TRUEPORT FOR AIX 4.2 and AIX 4.3 VERSION 4.1.0 29/09/2000
=========================================================
WHATS NEW
=========

    This software has identical function to version 4.0.0, but features the 
    ability to start a second trueport daemon, allowing upto 1024 serial device 
    connections.

INTRODUCTION
============

    Software version 4.1.0.

    TruePort is a virtual serial port device driver for the Perle IOLNA DS, X-Stream 
    range of terminal servers and the IOLAN+. 

    Please read the following notes before attempting to install or use this 
    product.

    This product supports the TruePort LITE mode ONLY on Chase IOLAN and 
    TruePort FULL mode on Specialix JET/LANSTREAM Terminal Servers.

    LITE mode (IOLAN mode) provides a Fixed TTY interface to AIX based software 
    - HOWEVER - you must pre-configure the Terminal Server for the required 
    serial parameters - baud rate, parity, flow control etc.

    IMPORTANT NOTE - Please note configuration of IOLAN is different to the 
    Chase IOLAND software - please read the section on IOLAN configuration 
    carefully.

FEATURES
========

    TruePort creates standard AIX TTY's in the /dev directory. Each TTY is 
    connected to a unique Terminal server and TCP/IP port number on that 
    terminal server via a special control device created during installation. 
    This terminal server/port number combination is used to uniquely identify 
    the port and the connection to the AIX computer.

    A keep-alive feature improves robustness; TruePort will always attempt to 
    keep the application interface available. If the network connection fails, 
    TruePort will reconnect when the network is available. This feature allows 
    terminal server power cycles without a server reboot.


INSTALLATION AND CONFIGURATION
==============================

    There are 3 major steps required to configure this product; Terminal server 
    and device driver installation (on the host computer) enable logins if 
    required.



1) IOLAN Terminal Server Configuration
______________________________________

    NOTE - Configuration method has changed from version 3.0.0.

    This product uses the "Remote" access mode on the IOLAN serial ports. Please 
    note - this is different to the configuration for IOLAND. 
    Please consult your Chase IOLAN product documentation for terminal server 
    configuration details.

    Each IOLAN port is configured to use a remote TCP port in the range 10001-
    10016, the Access parameters in the IOLAN PORT SETUP MENU would be 
    configured as follows:

		Access			[Remote]
		Authentication		[None]
		Mode			[Raw	]
		Connection		[     ]
		Host			[     ]
		Remote Port		[0    ]
		Local Port		[10001]	
	
	(10001 for the first port 10002 for second and so on ...)

    TruePort software uses the "Remote" Access mode on the IOLAN, in this mode 
    the line is configured to listen on the network for a specified TCP/IP host 
    port number. This is how the TruePort system matches the network connection 
    to the TTY name.


2) Device driver Installation
_____________________________

    Copy the supplied installable files onto the filesystem of the host 
    computer, we recommend /tmp.

    Before installation, make sure that you are logged in as the root user.

    Copy the TruePort package file to a blank floppy disk using the command :

		# dd if=/tmp/tpaix410.img of=/dev/fd0  bs=64k

    Install the software with the command:

		# installp -acX all

    The files will be installed and the device drivers will be loaded. You do 
    not need to reboot.

    TruePort administrative files and utilities are installed in the 
    /etc/trueport directory.

    TruePort is configured using the smit configuration utility, type:

		# smit tp

    The following menu is displayed:


		List All TruePort and Printer Devices
		Manage Terminal Server
		Manage TTY Devices
		Manage Printer Devices
		Delete All TruePort Devices



    All TTY devices are added in blocks (one terminal server at a time) using 
    the "Manage Terminal Server" -> "Add Terminal Server option".

    You are required to supply the name or IP address of the terminal server to 
    add and the number of ports required. All other entries should be left as 
    default. A block of TTYs corresponding to the number of ports selected will 
    be created and enabled for login.

3) Adding Logins
_________________

    Logins are automatically enabled when you add a terminal server.

    You may enable/disable login an individual TTY ports using the "Manage TTY 
    Devices option", and selecting the required TTY from the list generated in 
    the "Change / Show Characteristics of a TTY menu".

    Using the "List all TruePort TTY and Printer devices" option you can list 
    all of the TTY's created and their control devices.

    Some AIX device system administration experience is highly desirable before 
    attempting to add, remove or change individual devices.

FURTHER CONSIDERATIONS
======================

TTY Name
========

    The AIX serial TTY administration system is built into AIX, the user has no 
    control over the tty names generated. All the ports will be numbered from 
    the next available device number. In general if there are no other defined 
    serial ports in the system, the TTY's will be created starting from tty0. A 
    list of available TTY's can be generated in the smit tty menu.

Single/Dual Process(s)
======================

    While this version of TruePort supports upto 1024 TruePort ports between 2 
    daemons, the initial installation will only handle 512 ports with 1 daemon. 
    To enable the second daemon (and second set of 512 ports), the following 
    command should be entered

    		    # mkitab -i tpd "tpd2:2:respawn:/etc/trueport/tpd -2nd
    		    	     -config /etc/trueport/config.tp"

	Note: This line has been wrapped and the command must be entered as one
	      line.

    Trueport AIX version 4.1.0 runs the network connection for upto 512 TruePort 
    ports in a single process. If more than a total of 512 TruePort ports are 
    required, a second process must be started. The TruePort daemon(s) can 
    therefore become very large when the maximum number of ports is configured. 
    Consider increasing the system swap space partition if you don't have enough 
    physical memory to keep the process memory resident.

Port Restarter
==============

    The script /etc/trueport/restartport can be used to restart hung ports.

		Usage: ./restartport tty#

    This is needed because all ports are handled by one process, it signals the 
    daemon to stop and start just this one port.

Port Status
===========

    The file /etc/trueport/tpd.log contains status messages from the daemon. You 
    can get the TruePort daemon to re-read its configuration file by sending it 
    a SIGHUP.
		
		# cd /etc/trueport
		# kill -HUP `cat tpd.pid`

    This will cause the latest status of each port to be listed at the end of 
    the file.

    Any error messages will be listed in tpd.log.

    Note: If using 2 daemons and more than 512 Trueport ports, the files for
          the second set of ports will be 'tpd.2nd.pid' and 'tpd.2nd.log'
