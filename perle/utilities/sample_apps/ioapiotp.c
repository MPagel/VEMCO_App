/*
    This is a sample application that demonstrates how to use the Trueport I/O
    API.

    This application communicates with the Device Server (I/O models) through a
    Trueport mapped serial device (ie. on linux tx0001, tx0002 etc).
    
    The application includes code to read the status of I/Os and to control
    the I/Os.
    
    Disclaimer:  This sample application source code is provided "AS IS" with
                 no warranties of any kind. The entire risk arising out of 
                 the use or performance of the software and source code is 
                 with you.
*/

#include <stdio.h>
#include <errno.h>
#include <sys/types.h>
#include <endian.h>
#include <string.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <termios.h>
#include <poll.h>
#include <unistd.h>
#include <netinet/in.h>

#define MAX_CHANNELS            6

#pragma pack(1)
typedef struct
{
    u_int8_t cmd;
    u_int16_t startReg;
    u_int16_t numReg;
} ioApi_t;
#pragma pack()

#define CMD_READ_RW_BIT         0x01
#define CMD_READ_RW_WORD        0x03
#define CMD_READ_RO_WORD        0x04
#define CMD_WRITE_RW_BIT        0x15
#define CMD_WRITE_RW_WORD       0x16
#define CMD_INVALID             (-1)

enum IOAPI_ITEM
{
    IOAPI_ITEM_DI_SENSOR=0,
    IOAPI_ITEM_DI_ALARM_STATE,
    IOAPI_ITEM_DI_DSR,
    IOAPI_ITEM_DI_DSR_ALARM_STATE,
    IOAPI_ITEM_DI_DCD,
    IOAPI_ITEM_DI_DCD_ALARM_STATE,
    IOAPI_ITEM_DI_CTS,
    IOAPI_ITEM_DI_CTS_ALARM_STATE,
    IOAPI_ITEM_DO_SENSOR,
    IOAPI_ITEM_DO_DTR,
    IOAPI_ITEM_DO_RTS,
    IOAPI_ITEM_DI_SENSOR_LATCH,
    IOAPI_ITEM_DO_SENSOR_PULSE_ISW,
    IOAPI_ITEM_DO_SENSOR_PULSE_ASW, 
    IOAPI_ITEM_DO_SENSOR_PULSE_COUNT,
    IOAPI_ITEM_DI_DSR_LATCH,
    IOAPI_ITEM_DI_DCD_LATCH,
    IOAPI_ITEM_DI_CTS_LATCH,
    IOAPI_ITEM_AI_CLEAR_ALARM_LATCH,
    IOAPI_ITEM_AI_CLEAR_MAX,
    IOAPI_ITEM_AI_CLEAR_MIN,
    IOAPI_ITEM_AI_CUR_ENG,
    IOAPI_ITEM_AI_MIN_ENG,
    IOAPI_ITEM_AI_MAX_ENG,
    IOAPI_ITEM_AI_CUR_RAW,
    IOAPI_ITEM_AI_MIN_RAW,
    IOAPI_ITEM_AI_MAX_RAW,
    IOAPI_ITEM_AI_ALARM_LEVEL,
    IOAPI_ITEM_MAX
};

typedef struct
{
    int itemSize;
    int readCmd;
    int writeCmd;
    int (*readDecode)(unsigned char *data,int dataLen, void *value);
    int (*writeEncode)(void *data, void *res);
    int regno[MAX_CHANNELS];
} ioApiCmdTab_t;

int startReg1(int reg, int chanPort);
int startReg2(int reg, int chanPort);
int readDecodeBit(unsigned char *data, int dataLen, void *value);
int readDecodeWrd(unsigned char *data, int dataLen, void *value);
int readDecodeDwrd(unsigned char *data, int dataLen, void *value);
int writeEncodeBit(void *data, void *res);
int writeEncodeWrd(void *data, void *res);

ioApiCmdTab_t ioApiCmd[]=
{
    {1, CMD_READ_RW_BIT,  CMD_INVALID,       readDecodeBit,  NULL,            {6145, 6146, 6147, 6148, 6149, 6150}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {6209, 6210, 6211, 6212, 6213, 6214}},
    {1, CMD_READ_RW_BIT,  CMD_INVALID,       readDecodeBit,  NULL,            {4225, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {4289, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_INVALID,       readDecodeBit,  NULL,            {4353, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {4417, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_INVALID,       readDecodeBit,  NULL,            {4481, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {4545, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {6657, 6658, 6659, 6660, 6661, 6662}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {4673, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_BIT,  CMD_WRITE_RW_BIT,  readDecodeBit,  writeEncodeBit,  {4737, -1  , -1  , -1  , -1  , -1}},
                                             
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {6145, 6146, 6147, 6148, 6149, 6150}},
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {6209, 6210, 6211, 6212, 6213, 6214}},
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {6273, 6274, 6275, 6276, 6277, 6278}},
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {6337, 6338, 6339, 6340, 6341, 6342}},
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {4097, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {4609, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_READ_RW_WORD, CMD_WRITE_RW_WORD, readDecodeWrd,  writeEncodeWrd,  {5121, -1  , -1  , -1  , -1  , -1}},
    {1, CMD_INVALID,      CMD_WRITE_RW_WORD, NULL,           writeEncodeWrd,  {2049, 2050, 2051, 2052, -1  , -1}},
    {1, CMD_INVALID,      CMD_WRITE_RW_WORD, NULL,           writeEncodeWrd,  {2113, 2114, 2115, 2116, -1  , -1}},
    {1, CMD_INVALID,      CMD_WRITE_RW_WORD, NULL,           writeEncodeWrd,  {2177, 2178, 2179, 2180, -1  , -1}},
                                             
    {2, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeDwrd, NULL,            {2080, 2112, 2144, 2176, -1  , -1}},
    {2, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeDwrd, NULL,            {2082, 2114, 2146, 2178, -1  , -1}},
    {2, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeDwrd, NULL,            {2084, 2116, 2148, 2180, -1  , -1}},
    {1, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeWrd,  NULL,            {2086, 2118, 2150, 2182, -1  , -1}},
    {1, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeWrd,  NULL,            {2087, 2119, 2151, 2183, -1  , -1}},
    {1, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeWrd,  NULL,            {2088, 2120, 2152, 2184, -1  , -1}},
    {1, CMD_READ_RO_WORD, CMD_INVALID,       readDecodeWrd,  NULL,            {2089, 2121, 2153, 2185, -1  , -1}},
};

// fix the endianness of a 16 bit value if required
void fixEndian16(u_int16_t *value)
{
    *value=ntohs(*value);
}

// fix the endianness of a 32 bit value if required
void fixEndian32(u_int32_t *value)
{
    *value=ntohl(*value);
}

// get start register based on the item and the chan/port requested
int getStartReg(int item, int chanPort)
{
    if (chanPort < 1 || chanPort > MAX_CHANNELS)
        return(-1);

    return(ioApiCmd[item].regno[chanPort-1]);
}

// format (the number after the ":" is the number of bytes) 
// <function code:1><length:1><value:1>
int readDecodeBit(unsigned char *data, int dataLen, void *value)
{
    int *pRetValue=(int *)value;
    
    if (dataLen > 0 && (data[0] & 0x80))
    {
        return(-1);
    }
    if (dataLen != 3 || data[1] != 1)   // total length should be 3 and data length should be 1
    {
        return(-1);
    }
    if (data[2]&1)
        *pRetValue=1;
    else
        *pRetValue=0;
    return(0);
}

// format (the number after the ":" is the number of bytes) 
// <function code:1><length:1><value:2>
int readDecodeWrd(unsigned char *data, int dataLen, void *value)
{
    u_int16_t *pRetValue=(u_int16_t *)value;
    
    if (dataLen > 0 && (data[0] & 0x80))
    {
        return(-1);
    }
    if (dataLen != 4 || data[1] != 2)   // total length should be 4 and data length should be 2
    {
        return(-1);
    }
    *pRetValue=(*(u_int16_t *)&data[2]);
    fixEndian16(pRetValue);     // fix endianness if required
    return(0);
}

// format (the number after the ":" is the number of bytes) 
// <function code:1><length:1><value:4>
int readDecodeDwrd(unsigned char *data, int dataLen, void *value)
{
    u_int32_t *pRetValue=(u_int32_t *)value;
    
    if (dataLen > 0 && (data[0] & 0x80))
    {
        return(-1);
    }
    if (dataLen != 6 || data[1] != 4)   // total length should be 6 and data length should be 4
    {
        return(-1);
    }
    *pRetValue=(*(u_int32_t *)&data[2]);
    fixEndian32(pRetValue);     // fix endianness if required
    return(0);
}

int writeEncodeBit(void *data, void *res)
{
    u_int8_t *actResult=(u_int8_t *)res;
    int *actData=(int *)data;
    
    *actResult=(*actData)?1:0;
    return(1);  // length 1
}

int writeEncodeWrd(void *data, void *res)
{
    u_int16_t *actResult=(u_int16_t *)res;
    u_int16_t *actData=(u_int16_t *)data;

    *actResult=*actData;
    fixEndian16(actResult);     // fix endianness if required
    return(2);  // length 2
}

int writeDecodeResp(unsigned char *data,int dataLen)
{
    if (dataLen > 0 && (data[0] & 0x80))
    {
        return(-1);
    }
    if (dataLen != 5)
        return(-1);
    
    return(0);
}

void dumpBuffer(unsigned char *buffer, int buffersize)
{
    int i;
    
    printf("\n");
    for (i=0;i<buffersize;i++)
    {
        printf("%02x ",buffer[i]);
    }
    printf("\n");
}

void flushTty(int fd)
{
    unsigned char buffer[512];
    int retcode;
    struct pollfd pfd;
    
    pfd.fd=fd;
    pfd.events = POLLIN;
    pfd.revents = 0;
    while(1)
    {
        retcode=poll(&pfd, 1, 0);
        if (retcode == 1 && pfd.revents == POLLIN)
        {
            read(fd,&buffer[0],sizeof(buffer));
        }
        else
            break;
    }
}

int getData(int fd, enum IOAPI_ITEM item,int chanPort, void *data)
{
    ioApi_t req;
    unsigned char buffer[512];
    int readLen;
    int retcode;
    struct pollfd pfd;
    int value;

    if (item >= IOAPI_ITEM_MAX || ioApiCmd[item].readCmd == CMD_INVALID)
    {
        return(-1);
    }
    req.cmd=ioApiCmd[item].readCmd;
    value=getStartReg(item,chanPort);
    if (value < 0)
    {
        return(-1);
    }
    req.startReg=value;
    req.numReg=ioApiCmd[item].itemSize;
    fixEndian16((u_int16_t *)&req.startReg);        // fix endianness if required
    fixEndian16((u_int16_t *)&req.numReg);          // fix endianness if required
    retcode=write(fd,&req,sizeof(req));
    if (retcode < 0)
    {
        printf("error writing\n");
        return(-1);
    }
    pfd.fd=fd;
    pfd.events = POLLIN;
    pfd.revents = 0;
    while(1)
    {
        retcode=poll(&pfd, 1, -1);
        if (retcode == 1 && pfd.revents == POLLIN)
            break;
    }
    readLen=read(fd,&buffer[0],sizeof(buffer));
    if (readLen < 0)
        return(-1);
    
    retcode=ioApiCmd[item].readDecode(&buffer[0],readLen,data);
    return(retcode);
}

int setData(int fd, enum IOAPI_ITEM item, int chanPort, void *data)
{
    ioApi_t *req;
    unsigned char buffer[512];
    int readLen;
    int retcode;
    struct pollfd pfd;
    int reqLen;
    int value;

    if (item >= IOAPI_ITEM_MAX || ioApiCmd[item].writeCmd == CMD_INVALID)
    {
        return(-1);
    }
    req=(ioApi_t *)&buffer[0];
    req->cmd=ioApiCmd[item].writeCmd;
    value=getStartReg(item,chanPort);
    if (value < 0)
    {
        return(-1);
    }
    req->startReg=value;
    req->numReg=ioApiCmd[item].itemSize;
    fixEndian16((u_int16_t *)&req->startReg);   // fix endianness if required
    fixEndian16((u_int16_t *)&req->numReg);     // fix endianness if required
    reqLen=sizeof(req)+ioApiCmd[item].writeEncode(data, (void *)&buffer[sizeof(req)]);
    retcode=write(fd,&req,reqLen);
    if (retcode < 0)
    {
        printf("error writing\n");
        return(-1);
    }
    pfd.fd=fd;
    pfd.events = POLLIN;
    pfd.revents = 0;
    while(1)
    {
        retcode=poll(&pfd, 1, -1);
        if (retcode == 1 && pfd.revents == POLLIN)
            break;
    }
    readLen=read(fd,&buffer[0],sizeof(buffer));
    if (readLen < 0)
        return(-1);
    
    retcode=writeDecodeResp(&buffer[0],readLen);
    return(retcode);
}

int set_termios_raw(int fd, struct termios *prev_tios)
{
    struct termios tios;
    int retcode;
    
    retcode=tcgetattr(fd,&tios);
    if (retcode < 0)
    {
        return(-1);
    }
    if(prev_tios)
        memcpy(prev_tios, &tios, sizeof(struct termios));
    
    tios.c_cc[VTIME]=0;
    tios.c_cc[VMIN]=0;
    tios.c_iflag=IGNPAR;
    tios.c_oflag=0;
    tios.c_lflag=0;
    retcode=tcsetattr(fd,TCSANOW,&tios);
    if (retcode < 0)
    {
        return(-1);
    }
    return(0);
}

void demoSerial(int fd)
{
    u_int32_t value32;
    int i=0;
    int retcode;
    
    while(1)
    {
        printf("%d ",i);
        retcode=getData(fd,IOAPI_ITEM_DO_DTR,1,&value32);
        printf("DTR: ");
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%-6s ",value32?"1":"0");
        }
        retcode=getData(fd,IOAPI_ITEM_DO_RTS,1,&value32);
        printf("RTS: ");
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%-6s ",value32?"1":"0");
        }
        retcode=getData(fd,IOAPI_ITEM_DI_DSR,1,&value32);
        printf("DSR: ");
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%-6s ",value32?"1":"0");
        }
        retcode=getData(fd,IOAPI_ITEM_DI_DCD,1,&value32);
        printf("DCD ");
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%-6s ",value32?"1":"0");
        }
        retcode=getData(fd,IOAPI_ITEM_DI_CTS,1,&value32);
        printf("CTS ");
        if (retcode < 0)
        {
            printf("failed");
        }
        else
        {
            printf("%-6s ",value32?"1":"0");
        }
        printf("\n");
        sleep(1);
        i++;
    }
}

void demoAnalog(int fd, int start, int end)
{
    u_int16_t value16;
    float value32;
    int j;
    int retcode;
    
    for (j=start;j<=end;j++)
    {
        retcode=getData(fd,IOAPI_ITEM_AI_CUR_RAW,j,&value16);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%d ",(int)value16);
        }
        retcode=getData(fd,IOAPI_ITEM_AI_MIN_RAW,j,&value16);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%d ",(int)value16);
        }
        retcode=getData(fd,IOAPI_ITEM_AI_MAX_RAW,j,&value16);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%d ",(int)value16);
        }
        retcode=getData(fd,IOAPI_ITEM_AI_ALARM_LEVEL,j,&value16);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%d ",(int)value16);
        }
        retcode=getData(fd,IOAPI_ITEM_AI_CUR_ENG,j,&value32);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%f ",value32);
        }
        retcode=getData(fd,IOAPI_ITEM_AI_MIN_ENG,j,&value32);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%f ",value32);
        }
        retcode=getData(fd,IOAPI_ITEM_AI_MAX_ENG,j,&value32);
        if (retcode < 0)
        {
            printf("failed ");
        }
        else
        {
            printf("%f ",value32);
        }
        printf("\n");
    }
}

void demoT4orA4(int fd)
{
    int i=0;
    
    while(1)
    {
        printf("iteration %d\n",i);
        demoAnalog(fd, 1, 4);
        sleep(1);
        i++;
    }
}

void demoDigital(int fd, int start, int end)
{
    u_int32_t value32;
    int j;
    int retcode;
    
    for (j=start;j<=end;j++)
    {
        retcode=getData(fd,IOAPI_ITEM_DO_SENSOR,j,&value32);
        if (retcode < 0)
        {   // not a digital output/relay
            retcode=getData(fd,IOAPI_ITEM_DI_SENSOR,j,&value32);    // see if it's a digital input
            if (retcode < 0)
            {
                printf("Failed");
            }
            else
            {
                if (value32 == 0)
                {
                    printf("Inactive");
                }
                else
                {
                    printf("Active");
                }
            }
        }
        else
        {
            if (value32 == 0)
            {
                printf("Inactive");
            }
            else
            {
                printf("Active");
            }
        }
        printf("; ");
    }
    printf("\n");
}

void demoD4orD2R2(int fd)
{
    u_int16_t value16;
    int i,j;
    int retcode;
    
    for (j=1;j<=4;j++)
    {
        retcode=getData(fd,IOAPI_ITEM_DO_SENSOR_PULSE_ISW,j,&value16);
        if (retcode >= 0)
        {
            printf("chan%d inactive width %d\n",j,value16);
        }
        retcode=getData(fd,IOAPI_ITEM_DO_SENSOR_PULSE_ASW,j,&value16);
        if (retcode >= 0)
        {
            printf("chan%d active width %d\n",j,value16);
        }
        retcode=getData(fd,IOAPI_ITEM_DO_SENSOR_PULSE_COUNT,j,&value16);
        if (retcode >= 0)
        {
            printf("chan%d pulse count %d\n",j,value16);
        }
    }
    i=0;
    while(1)
    {
        printf("iteration %d\n",i);
        demoDigital(fd, 1, 4);
        sleep(1);
        i++;
    }
}

void demoA4R2orA4D2(int fd)
{
    int i=0;
    
    while(1)
    {
        printf("iteration %d\n",i);
        demoAnalog(fd, 1, 4);
        demoDigital(fd, 5, 6);
        sleep(1);
        i++;
    }
}

int main(int argc, char *argv[])
{
    int fd;
   
    if (argc < 3)
    {
        printf("usage: %s <tty device name> <demomode>\n",argv[0]);
        printf("    demo:   serial, t4, a4, d4, d2r2, a4r2 a4d2\n");
        return(0);
    }
    fd=open(argv[1],O_RDWR);
    if (fd < 0)
    {
        printf("can't open %s\n",argv[1]);
        return(1);
    }
    set_termios_raw(fd,NULL);
    flushTty(fd);
    if (strcmp(argv[2],"serial")==0)
        demoSerial(fd);
    else if (strcmp(argv[2],"t4")==0)
        demoT4orA4(fd);
    else if (strcmp(argv[2],"a4")==0)
        demoT4orA4(fd);
    else if (strcmp(argv[2],"d4")==0)
        demoD4orD2R2(fd);
    else if (strcmp(argv[2],"d2r2")==0)
        demoD4orD2R2(fd);
    else if (strcmp(argv[2],"a4r2")==0)
        demoA4R2orA4D2(fd);
    else if (strcmp(argv[2],"a4d2")==0)
        demoA4R2orA4D2(fd);
    else
        printf("invalid demomode\n");
    
    return(0);
}

