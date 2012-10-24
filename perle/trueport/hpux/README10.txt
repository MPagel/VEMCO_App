        TruePort - Release 1.1.0, Release Notes. June 30, 2004.
                      OPERATING SYSTEM HP-UX 10.x

New in this Release
===================

The number of Trueport device nodes supported in Release 1.1.0 has been 
increased to 4000.

The "addports" script and the "tpadm" utility has been modified to increase the
performance when adding a large number of device nodes.

A version of Trueport is now available for HP-UX 10 systems.


Introduction and Overview
=========================

TruePort is similar to the MTSRD system available on HP-UX 10.2 and other 
operating systems. TruePort software completely replaces MTSRD.

TruePort for HP-UX is a UNIX serial port emulation system for the 
Specialix JETSTREAM terminal server family. TruePort provides a standard
UNIX TTY interface to application software. This is achieved using TruePort 
serial device drivers.

TruePort provides a more general interface via a fixed /dev TTY name,
this allows application software to send and receive data from ports on the
terminal server as if they were directly connected to the application server.

TruePort supports some additional features that make it more flexible than
either rlogin or telnet:

* Support for an auxiliary port (or printer port) on each connection;
* Programmable keepalives for improved connection reliability;
* Automatic reconnection after network disconnects.


Features Overview
=================

TruePort creates a standard UNIX TTY in the /dev directory.
Each TTY is connected to a unique TCP/IP port number on the terminal server.
This port number is used to uniquely identify the port and the connection to
the computer.

The keep-alive feature improves robustness, TruePort will always attempt
to keep the application interface available. If the network connection fails,
TruePort will reconnect when the network is available. This feature allows
terminal server power cycles without a server reboot.


Software Installation 
=====================

NOTE - Please remove any previous versions (or Beta releases) of TruePort 
software OR MTSRD for HP-UX before attempting to install this product.

Installation uses the "swinstall" utility.

The Installable package is contained in the files: 

    tphp10v<nnn>.depot.Z  for HPUX-10.
	
	where <nnn> is the version number of the release.

This is a compressed software depot. Proceed as follows to install the software:

- Login as a root user and copy the installation package to /tmp.
- Decompress the depot.
	
    # uncompress tphpxxvnnn.depot.Z

- Register the package as a software installation depot.

    # swreg -l depot /tmp/tphpxxvnnn.depot

- Start the installer.

    # swinstall

- When the SD Install window appears, click the "Source Depot Path" button and
  select "/tmp/tphp<ver>.depot", Click OK to return to the main window.
- Select "TruePort" in the "SD Install window, select "Actions->Mark for
  Install" followed by "Actions->Install"
- Follow the on screen instructions, you can view the product description and
  README when the files have been installed on the computer.
- When the installation is complete you will be prompted to click OK and the
  system will be rebooted.

When the system has rebooted you can check that the software has been correctly 
installed with the lsdev command:

  	# lsdev

A list of installed devices, "tpm" and "tps" should be listed.

Once software installation is complete, you are required to configure your 
terminal servers to use the TruePort (MTSRD) service and to enable ports on your 
server computer.


Terminal Server Configuration
=============================

The JETSTREAM/LANSTREAM product documentation has detailed information on 
configuration options when using TruePort (MTSRD).

The following is an example for adding two lines using the 
JETSTREAM 6000 CLI commands:

	MTS# add host HPSERV 192.101.34.99
	MTS# set line 1 type silent raw HPSERV 10000
	MTS# set line 2 type silent raw HPSERV 10001
	MTS# reboot

TruePort software uses the "silent raw" service on the terminal server, in this 
mode the line is configured to connect to your host UNIX system on a specified 
TCP/IP host port number. This is how the TruePort system matches the network 
connection to the TTY name.

There are some restrictions on the TCP/IP port numbers you should use, see the 
following section (IMPORTANT NOTE).


Enabling Serial Ports
=====================

All TruePort ports must be enabled before they can be used. No ports are 
available by default after software installation.

TruePort daemons can be launched from /etc/inittab at system boot time or by 
using the TruePort administrative tools described in this section. Users 
migrating from MTSRD may wish to ignore this section and read the "Notes for 
MTSRD users".

TruePort for HP-UX supports up to 4000 TruePort serial device nodes.

Trueport administrative files and utilities are located in the directory 
"/etc/trueport".

TTY ports are enabled using the TruePort admin tool "tpadm". The user guide 
describes the use of this utility to edit the TruePort configuration files.

To simplify configuration of the default Standard TruePort mode, a 
quick start script (addports) is installed in the /etc/trueport directory.

The TruePort serial device naming convention is as follows:

	/dev/ttys0 through /dev/ttys3999 for the non-modem open devices.
	/dev/ttyS0 through /dev/ttyS3999 for the modem open devices.
	/dev/ttyt0 through /dev/ttyt3999 for the pseudo modem open (Reynolds mode)
    	devices.

Addports is a quick installation script that allows the user to quickly add a 
range of serial ports, the syntax is:

		# cd /etc/trueport
		# sh addports <start port> <end port>

	Where <start port> is the number of the first port to use and <end port> is
	the number of the last port to use.

For example the command:

  	# sh addports 0 63

would enable 64 TruePort devices (if 64 terminal server ports have been
correctly configured).


IMPORTANT NOTE
--------------
	Although you can use any TCP/IP port number you like and any /dev/ttyname you
	like, the "addports" script described above assumes that all ports start at
	TCP/IP port number 10000 (/dev/tty[sSt]0 is connected to 10000 on the terminal
	server). If you do not adopt this convention, then you will not be able to use
	"addports".

The TruePort system is configured to start the required drivers at a system
reboot. These ports will be automatically started each time the system is 
rebooted. The ports enabled can be used as standard serial devices for login 
ports or for other serial devices.

This release does not add login facilities to the ports created. The system 
administrator should do this using system administration facilities.

To use a TruePort port as a login port, you should add an appropriate getty 
entry to the /etc/inittab file.


Notes for MTSRD Users
=====================

Users of MTSRD on this or other platforms will find TruePort similar to MTSRD.

TruePort uses the same network protocol as MTSRD and is completely compatible 
with Specialix terminal servers configured for MTSRD.

The following is a list of the differences between MTSRD (HP-UX) and TruePort :

- Like MTSRD, TruePort uses a daemon process to handle data flow between the
	terminal server port and the serial device drivers. However, where MTSRD
	starts it's daemon processes from /etc/inittab, TruePort has it's own
	administrative start-up file in /etc/trueport. This improves start-up
	performance when large numbers of ports need to be started.

- TruePort contains support for robust connections, if a terminal server with
	an active serial connection is rebooted (or replaced with an identically
	configured unit), the session will be resumed after a short user defined
	interval. The keepalive feature is used to perform this function. The default
	keepalive is 30 seconds.

- TruePort currently does not add any getty entries to /etc/inittab for login
	ports, this task must be performed by the system administrator.

- TruePort supports a wider range of terminal servers than MTSRD, it DOES NOT
	check the version numbers or firmware version of connection terminal servers.
	TruePort never sends reboot commands to terminal servers.



Known Issues and Bugs
=====================

1.	This release includes code to work around a specific HP-UX STREAMS open
	issue with multiple open tty devices. It is possible that HP-UX will
	allocate an invalid STREAM under certain conditions if 2 processes try to
	pen the same device simultaneously. In order to prevent the resulting
	invalid condition occurring, the TruePort drivers have been modified to
	return an error condition to the second of the two process ( errno set to
	EAGAIN ). The second process should retry the open. The second open will
	succeed after the first open has completed. HP are aware of this issue and
	may release a patch at a future date.
	

FILE REFERENCE
==============

/etc/trueport -	Installation directory.
	config.tp   -	Trueport configuration file.
	addports    -	Quick start configuration script.
	cleanports	-	Cleanup script.
	tpd         -	TruePort daemon program.
	tpadm       -	Configuration utility (used by addports).

/dev/         -	Devices directory.
	ttysxxxx		-	Non-modem serial ports.
	ttySxxxx		-	Modem serial ports.
	ttytxxxx		-	Pseudo modem (Reynolds mode) ports.
	tpmxxxx     -	TruePort control device nodes.

/sbin/				      -	System directory.
	init.d/trueport		-	TruePort start-up script.


TruePort running in Standard mode is actually a pair of device drivers.
The TTY (or slave) device driver is called "tps".
The control (or master) device driver is called "tpm".


History
=======

Release 1.0.1
-------------

Release 1.0.1 contains code to work around a HP-UX 11 STREAMS open issue as 
described in the Known Issues and Bugs item 2 above.
