/*
    This is a sample application that demonstrates how to decode the UDP
    data that is sent by the UDP broadcast feature on the I/O models
    
    The application simply decodes the data and prints the information in a
    human readable format.
    
    Disclaimer:  This sample application source code is provided "AS IS" with
                 no warranties of any kind. The entire risk arising out of 
                 the use or performance of the software and source code is 
                 with you.
*/

#include <stdio.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>
#include <netinet/in.h>
#include <endian.h>
#include <string.h>

#define UDP_VERSION         0
#define MAX_PACKET_SIZE     1500
#define MAX_ANALOG          4
#define MAX_DIGITAL         4
#define MAX_SERIAL_PORTS    1
#define MAX_SERIAL_SIGNAL   5

#define SERIAL_SIGNAL_DSR   0
#define SERIAL_SIGNAL_DCD   1
#define SERIAL_SIGNAL_CTS   2
#define SERIAL_SIGNAL_DTR   3
#define SERIAL_SIGNAL_RTS   4

typedef struct
{
    unsigned char valid;
    unsigned short curRawValue;
    unsigned short minRawValue;
    unsigned short maxRawValue;
    float curEngValue;
    float minEngValue;
    float maxEngValue;
} udpAnalog_t;

typedef struct
{
    unsigned char valid;
    int state;
} udpDigital_t;

typedef struct
{
    unsigned char valid;
    int state;
} udpSerial_t;

typedef struct
{
    udpAnalog_t analog[MAX_ANALOG];
    udpDigital_t digital[MAX_DIGITAL];
    udpSerial_t serial[MAX_SERIAL_PORTS][MAX_SERIAL_SIGNAL];
} udpInfo_t;

int udpPort=10001;
udpInfo_t udpInfo;

int openUdpSocket(void)
{
    int fd;
    int error;
    int val;
    struct sockaddr_in sock;
    
    fd=socket(PF_INET,SOCK_DGRAM,0);
    if (fd < 0)
    {
        printf("Socket error %d\n",errno);
        return(-1);
    }
    val = 1;
    error=setsockopt(fd, SOL_SOCKET, SO_REUSEADDR, (void*) &val, sizeof(val));
    if (error < 0)
    {
        printf("setsockopt SO_REUSEADDR error %d\n",errno);
        return(-1);
    }
    sock.sin_family=AF_INET;
    sock.sin_addr.s_addr=INADDR_ANY;
    sock.sin_port=htons(udpPort);
    error=bind(fd,(struct sockaddr *)&sock,sizeof(sock));
    if (error < 0)
    {
        printf("bind error %d\n",errno);
        return(-1);
    }
    return(fd);
}

void fixEndian16(u_int16_t *value)
{
    *value=ntohs(*value);
}

void fixEndian32(u_int32_t *value)
{
    *value=ntohl(*value);
}

// returns -1 error
//         0  otherwise
int getUint8(u_int8_t *buf,int *index,int bufSize, u_int8_t *retData)
{
    if (*index > (int)(bufSize-sizeof(u_int8_t)))
    {
        return(-1);
    }
    else
    {
        *retData=*(buf+*index);
        (*index)++;
    }
    return(0);
}

// returns -1 error
//         0  otherwise
int getUint16(u_int8_t *buf,int *index,int bufSize, u_int16_t *retData)
{
    if (*index > (int)(bufSize-sizeof(u_int16_t)))
    {
        return(-1);
    }
    else
    {
        *retData=*(u_int16_t*)(buf+*index);
        (*index)+=sizeof(u_int16_t);
        fixEndian16(retData);
   }
   return(0);
}

// returns -1 error
//         0  otherwise
int getUint32(u_int8_t *buf,int *index,int bufSize, u_int32_t *retData)
{
    if (*index > (int)(bufSize-sizeof(u_int32_t)))
    {
        return(-1);
    }
    else
    {
        *retData=*(u_int32_t *)(buf+*index);
        (*index)+=sizeof(u_int32_t);
        fixEndian32(retData);
    }
    return(0);
}

int procUdpFrame(u_int8_t *buf,int bufSize)
{
    u_int8_t version;
    int index;
    u_int16_t totalLength;
    int error;
    u_int16_t sectionLength;

    memset(&udpInfo,0,sizeof(udpInfo));
    index=0;
    error=getUint8((u_int8_t *)buf,&index, bufSize, &version);
    if (error < 0 || version!= UDP_VERSION)
    {
        printf("Unsupported UDP format\n");
        return(-1);
    }
    error=getUint16((u_int8_t *)buf,&index, bufSize, &totalLength);
    if (error < 0)
    {
        printf("No total length\n");
        return(-1);
    }

// process analog data
    error=getUint16((u_int8_t *)buf,&index, bufSize, &sectionLength);
    if (error < 0)
    {
        printf("No analog length\n");
        return(-1);
    }
    if (sectionLength > 0)
    {
        u_int8_t enabled;
        u_int8_t bit=1;
        int i;

        if (((sectionLength-1) % 18) != 0 || ((sectionLength-1) / 18) > MAX_ANALOG)
        {
            printf("Invalid analog length\n");
            return(-1);
        }
        error=getUint8((u_int8_t *)buf,&index, bufSize, &enabled);
        if (error < 0)
        {
            printf("Missing Analog info specifier\n");
            return(-1);
        }
        for (i=0;i<MAX_ANALOG;i++,bit=bit<<1)
        {
            if (enabled & bit)
            {
                udpInfo.analog[i].valid=1;
                error=getUint16((u_int8_t *)buf,&index, bufSize, &udpInfo.analog[i].curRawValue);
                if (error < 0)
                {
                    printf("Missing analog curRawValue\n");
                    return(-1);
                }
                error=getUint16((u_int8_t *)buf,&index, bufSize, &udpInfo.analog[i].minRawValue);
                if (error < 0)
                {
                    printf("Missing analog minRawValue\n");
                    return(-1);
                }
                error=getUint16((u_int8_t *)buf,&index, bufSize, &udpInfo.analog[i].maxRawValue);
                if (error < 0)
                {
                    printf("Missing analog maxRawValue\n");
                    return(-1);
                }
                error=getUint32((u_int8_t *)buf,&index, bufSize, (u_int32_t *)&udpInfo.analog[i].curEngValue);
                if (error < 0)
                {
                    printf("Missing analog curEngValue\n");
                    return(-1);
                }
                error=getUint32((u_int8_t *)buf,&index, bufSize, (u_int32_t *)&udpInfo.analog[i].minEngValue);
                if (error < 0)
                {
                    printf("Missing analog minEngValue\n");
                    return(-1);
                }
                error=getUint32((u_int8_t *)buf,&index, bufSize, (u_int32_t *)&udpInfo.analog[i].maxEngValue);
                if (error < 0)
                {
                    printf("Missing analog maxEngValue\n");
                    return(-1);
                }
            }
        }
    }

// process digital data
    error=getUint16((u_int8_t *)buf,&index, bufSize, &sectionLength);
    if (error < 0)
    {
        printf("No digital length\n");
        return(-1);
    }
    if (sectionLength > 0)
    {
        u_int8_t enabled;
        u_int8_t state;
        u_int8_t bit=1;
        int i;

        if (sectionLength != 2)
        {
            printf("Invalid digital length\n");
            return(-1);
        }
        error=getUint8((u_int8_t *)buf,&index, bufSize, &enabled);
        if (error < 0)
        {
            printf("Missing digital info specifier\n");
            return(-1);
        }
        error=getUint8((u_int8_t *)buf,&index, bufSize, &state);
        if (error < 0)
        {
            printf("Missing digital state data\n");
            return(-1);
        }
        for (i=0;i<MAX_DIGITAL;i++,bit=bit<<1)
        {
            if (enabled & bit)
            {
                udpInfo.digital[i].valid=1;
                if (state & bit)
                {
                    udpInfo.digital[i].state=1;
                }
            }
        }
    }

// process serial data
    error=getUint16((u_int8_t *)buf,&index, bufSize, &sectionLength);
    if (error < 0)
    {
        printf("No serial length\n");
        return(-1);
    }
    if (sectionLength > 0)
    {
        u_int8_t enabled;
        u_int8_t state;
        u_int8_t bit=1;
        int i;

        if (sectionLength != 2)
        {
            printf("Invalid serial length %d\n",sectionLength);
            return(-1);
        }
        error=getUint8((u_int8_t *)buf,&index, bufSize, &enabled);
        if (error < 0)
        {
            printf("Missing serial info specifier\n");
            return(-1);
        }
        error=getUint8((u_int8_t *)buf,&index, bufSize, &state);
        if (error < 0)
        {
            printf("Missing serial state data\n");
            return(-1);
        }
        for (i=0;i<MAX_SERIAL_SIGNAL;i++,bit=bit<<1)
        {
            if (enabled & bit)
            {
                udpInfo.serial[0][i].valid=1;
                if (state & bit)
                {
                    udpInfo.serial[0][i].state=1;
                }
            }
        }
    }
    return(0);
}

char *serialSignalName[]={"DSR","DCD","CTS","DTR","RTS"};

void dispUdpData(void)
{
    static int frameCount=0;
    int i,j;
    
    printf("Frame %d\n",++frameCount);
    for (i=0;i<MAX_ANALOG;i++)
    {
        if (udpInfo.analog[i].valid)
        {
            printf("    Analog%d  curRaw(%d) minRaw(%d) maxRaw(%d) curEng(%f) minEng(%f) maxEng(%f)\n",
                i+1,
                (int)udpInfo.analog[i].curRawValue,
                (int)udpInfo.analog[i].minRawValue,
                (int)udpInfo.analog[i].maxRawValue,
                udpInfo.analog[i].curEngValue,
                udpInfo.analog[i].minEngValue,
                udpInfo.analog[i].maxEngValue);
        }
    }
    for (i=0;i<MAX_DIGITAL;i++)
    {
        if (udpInfo.digital[i].valid)
        {
            printf("    Digital%d %s\n",
                i+1,
                udpInfo.digital[i].state?"Active":"Inactive");
        }
    }
    for (i=0;i<MAX_SERIAL_PORTS;i++)
    {
        for (j=0;j<MAX_SERIAL_SIGNAL;j++)
        {
            if (udpInfo.serial[i][j].valid)
            {
                printf("    Serial%d %s:%s\n",
                    i+1,
                    serialSignalName[j],
                    udpInfo.serial[i][j].state?"Active":"Inactive");
            }
        }
    }
    printf("\n");
}

void processUdpData(int fd)
{
    unsigned char recvBuffer[1500];
    int curRecvSize;
    
    while(1)
    {
        curRecvSize=recv(fd,&recvBuffer[0],MAX_PACKET_SIZE,0);
        if (curRecvSize==0)
        {
            printf("socket error: read 0 bytes .. socket probably closed\n");
            return;
        }
        if (procUdpFrame(&recvBuffer[0],curRecvSize) < 0)
        {
            return;
        }
        dispUdpData();
    }
}

int main(int argc, char *argv[])
{
    int fd;
    
    if (argc > 1)
    {
        if (strcmp(argv[1],"?")==0)
        {
            printf("usage: %s [udpPort]\n",argv[0]);
            printf("    udpPort    udp port to listen on\n\n");
            return(0);
        }
        sscanf(argv[1],"%d",&udpPort);
    }
    fd=openUdpSocket();
    if (fd < 0)
    {
        return(-1);
    }
    processUdpData(fd);
    close(fd);
    return(0);
}
