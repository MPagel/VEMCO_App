TruePort Lite  - Release 1.0.8, Technical Notes. 



New in this Release
===================

An option has been added to allow greater performance between Trueport daemon 
ports and connected telnet clients.  This option is only effective when 
running Trueport in Lite mode. The option should not be used when the Trueport 
daemon will be connecting with a terminal server port (jetstream or iolan ).

The option for the tpadm utility is "-f". This will add an entry into the 
config.tp file. An example : "tpadm -a 10003 -p X0003 -f"

The option for the command lines in the config.tp file is "-nopace". These lines
are created automatically if the tpadm utility is used. If commands are entered 
manually then the "-nopace" option must be placed before the -tty option. 

See the Trueport User's Guide for further details on the utility and 
configuration file.

This option works on a per port basis. Therefore you can configure some of the 
Trueport devices without the option to connect to terminal server ports and at 
the same time configure other Trueport devices with the -f option to connect to
PCs running a Telnet client.

As well, this release changes the keepalive packets back to the original type. 
This is a normal TCP <PUSH> of 1 byte of data. The data is "00". 

NOTE for release 1.0.5
======================

The following work have been done for this release

1) Increaseing the total number of trueport ports to 2000. Note that since each 
trueport connection creates two processes, to accomodate such a number of trueport
ports, your NCR system is very likely need to be retuned.

2) New scripts added to facilitate creation and removal of port monitors (portmon and portrem).

3) The "cleanports" script has been updated to remove all services.

4) The script "enttymon" was added to enable all services in case some don't come
   up after a re-boot.


<Tuning your system>
If you not use large amount trueport ports, you may not require to tune your system
as the default setup of your system may have the capacity to handle them. If you
are not sure, check with the folloing two files:

a) The STREAMware TCP telnet and rlogin daemons require one pseudo terminal device for 
   each incoming connection.  The number of devices is defined in the units field of;
   /etc/conf/sdevice.d/ptm and
   /etc/conf/sdevice.d/ptem
   The units value in both of these files must match and be big enough to accommodate
   all your connetions.

b) Check the file /etc/conf/cf.d/stune.  Make sure that the system value NPROC 
   (number of process) is big enought for all existing system process plus an additional
   2 process for each TRUEPORT connection.

These two changes will insure that all the trueport ports will have the resouces 
when, in worst case, all ports are required for your application. Make sure the
kernel is rebuild (idbuild) and reboot (shutdown -i6 -g0 -y) under root directory
before the new configuration takes effect. A super-user privilege is required for 
making the above changes.As a precaution, make a copy of each file before change
in case something goes wrong.      


NOTE for release 1.0.2
=======================

On NCR 3 this software is only released for login tty's only. You must configure
a login using the facilities provided to correctly use this software. See "Bugs
Fixed 3.

1. Many bugs resolved - see below.

2. Added scripts - addports <number> - This adds <number> of tty ports starting
at X0 ( TCP port 10000 ) and creates a login process on each one.
- cleanports - Removes all tpd daemons, configurations and ttymon entries.

Introduction and Overview
=========================

This software has been built on NCR UNIX release 3.0.2.

TruePort Lite for NCR UNIX is a UNIX serial port emulation system for the 
Specialix X-Stream terminal server family. TruePort provides a standard
UNIX TTY interface to application software. This is achieved using the UNIX
pseudo TTY system used by software such as the telnet and rlogin programmes.
TruePort provides a more general interface via a fixed /dev/term TTY name,
this allows application software to send and receive data from ports on the
terminal server as if they were directly connected to the application server.

TruePort has supports some additional features that make it more flexible than
either rlogin or telnet :

* Support for an auxiliary port ( or printer port ) on each connection;
* Programmable keep-alives for improved connection reliability;
* Automatic reconnection after network disconnects.

Although TruePort Lite provides most of the features of a standard serial port,
it has no physical device control capabilities. You must pre-configure terminal
server ports for the required baud rates, parity etc.
Application IOCTL commands to TruePort ports are not propagated to the terminal
server.
	
Features
========

TruePort creates a standard UNIX TTY in the /dev/term directory.
Each TTY is connected to a unique TCP/IP port number on the terminal server.
This port number is used to uniquely identify the port and the connection to
the UNIX server computer.

Each connection supports an optional auxiliary port, this is typically used
for printing to a printer connected to a terminal.
Auxiliary devices have their own /dev/term TTY name and can be used by any
application.  When the auxiliary port is used by a printing application,
it is necessary to identify the terminal type to TruePort so that the 
appropriate printer ON/OFF codes can be added to the data stream. A terminal 
capabilities file printcap.tp is included with definitions for some commonly
used terminal types. Additional definitions can be added for other terminals
types.

The keep-alive feature improves robustness, TruePort will always attempt
to keep the application interface available. If the network connection fails,
TruePort will reconnect when the network is available. This feature allows
terminal server power cycles without a server reboot.

Installation and Configuration
==============================

Please consult the supplied user guide.

Please consult your JETSTREAM/LANSTREAM product documentation for terminal
server configuration details.

The following is an example for adding two lines using the 
JETSTREAM CLI commands:

	JS_8500# add host  ncrserver 192.101.34.99
	JS_8500#  set line 1 service silent raw ncrserver 10000
	JS_8500#  set line 2 service silent raw ncrserver 10001
	JS_8500#  save
	JS_8500# reboot

TruePort software uses the silent raw service on the terminal server,
in this mode the line is configured to connect to your host UNIX system on a
specified TCP/IP host port number. This is how the TruePort system matches
the network connection to the TTY name.

Adding Login Ports
==================

TTY ports are created using the TruePort admin tool "tpadm". The user guide 
describes the use of this utility to edit the TruePort configuration files.
The TruePort system is configured to start the required software at a system
reboot. However, tpadm has a fast start option "-s" which allows you to start
any single port immediately. This facility allows ports to be added without a
reboot.

For example, if the JETSTREAM terminal server had been configured as above,
then the following commands could be used to declare and start a new port
daemon on the UNIX system:

# /etc/trueport/tpadm -a 10000 -p X0 -t wyse60
	Declare a tty /dev/term/X0, printer port /dev/term/X0p ( type wyse60 ).

# /etc/trueport/tpadm -s 10000
	Start it running. Execution will be automatic on reboot.

Once ports have been created, logins can be added using ttymon or getty.
A port monitor called trumon is added by the install software.
You can use the sysadm tool to add port services ( logins ) to ports created.
When adding ports, please remember to specify the streams modules ptem 
and ldterm as a comma separated list of modules required for login, i.e. 
ptem,ldterm.

A shell script "tplogin" has also been included to add,remove,enable,disable
ttymon logins on a port by port basis.

eg.	# tplogin add X0 
	Add a login to TruePort /dev/term/X0

	# tplogin disable X0
	Stop logins on /dev/term/X0

	# tplogin enable X0
	Allow logins on /dev/term/X0

	# tplogin remove X0
	Remove the login for /dev/term/X0

Application software can print directly to TruePort main or printer ports
or printers can be configured using the lp print scheduler.

Transparent Print Throttle Feature
==================================

When using the auxiliary port as a transparent printer device ( printer 
connected to the terminal printer port ), the printer can seriously degrade 
terminal performance, especially if the printer baud rate and print speed is
lower than the terminal speed. In order to preserve reasonable terminal response
while printing, the auxiliary port is throttled to a fixed maximum bytes
per Second. The default throttle is 200 bytes/Second as this gives acceptable
performance on a terminal printer at 4800 baud. If your printer is faster you 
can increase this count via the "-tpbps #" option in the TruePort configuration
file, eg.

tpd -port 10000 -tty /dev/term/X0 -aux /dev/term/X0p -term vt100 -tpbps 500

As a rule of thumb we recommend that you calculate the tpbps as 25% of the
line speed ( in bytes/Second ) of the printer or terminal - which ever is the 
slower. So for example, if the terminal baud is 19200 ( 1920 Bytes/S ) and the
printer is 9600 baud ( 960 Bytes/S ), then the tpbps could be set for 240.

The maximum count supported is 800 bytes/Second. If you notice that terminal
response is poor when printing then try reducing tpbps below the default 200.

Bugs Fixed
==========

1. Resolved a bug where tpd keeps the slave tty open, signal delivery can then
kill tpd in error.

2. Added robustness code to reopne tty after serious error conditions.

3. Removed all AUTOPUSH code - due to a limitation of NCR UNIX, only 32 autopush
configurations are allowed. It is now up to the application (ttymon) to push any
required modules.

Known Issues and Bugs
=====================

1. TruePort Lite uses the UNIX pseudo TTY system. Unix pseudo TTYs are 
dynamically allocated to TruePort as more daemons are configured in config.tp. 
You may eventually configure more daemons than you have pseudo TTYs in your 
operating system. You should consult your operating system documentation for 
information about increasing the number of pseudo TTYs in this case.

2. When adding port services to the trumon port monitor, the administrator must 
specify ptem,ldterm for the list of streams modules to push when the port 
monitor is started.

3. If ttymon is running on a port and tpd is killed ( or exits ) and is
restarted by the administrator, then the ttymon (trumon) service must be
disabled and then re-enabled for that port before login will work again.

4. When each daemon runs it creates a trace file ( for error indication )
and a restore script. The restore script is needed to keep the system pseudo
TTY system operational if the TruePort daemon encounters an unrecoverable
error condition and exits prematurely or for example to recover from a server
power failure situation. The restore scripts are automatically executed
( if present ) during system start-up. Manual execution of the restore scripts
should not be necessary, however if you choose to run these manually, then you
should ensure that all tpd processes have been terminated
( using the kill command ) before doing this.

5. Never use the kill -9 command on tpd, as this prevents tpd from exiting
cleanly and restoring it's pseudo TTYs.

6. Because this software is based on the UNIX pseudo TTY system ( which uses
STREAMS clone devices ), the device major/minor numbers may change when a port
is closed and re-opened or when a device error occurs. This is normal behavior,
the correct /dev/term/<ttyname> will be correctly linked to the pseudo tty.

So that shell commands such as ps and whodo report the correct tty in use by an
application, it is necessary for TruePort to force the ps command to rebuild
it's tty database when a pseudo terminal device change occurs.
This should be transparent to the user, however if "whodo" complains that the
"/etc/ps_data" file is missing - it should be re-created by simply running the 
"ps" command ( by any user ).

