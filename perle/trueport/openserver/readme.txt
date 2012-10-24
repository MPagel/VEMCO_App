===============================================================================
         Perle TruePort Daemon and  Driver for SCO OpenServer 5.0.X
           Copyright (C) 2004-2008, Perle Systems Limited
===============================================================================

  Release           : 6.2.0
  Date              : November 2008 
  O/S Compatibility : SCO OpenServer 5.0.X

===============================================================================

Introduction: 
=============

This is the readme belonging to the Trueport for OpenServer 5 software. 

TruePort creates standard UNIX TTY's in the /dev/term/ directory. Each TTY is 
connected to a unique TCP/IP port number on the device server. This port 
number is used to uniquely identify the port and the connection to the UNIX 
server computer.


TruePort for OpenServer 5 is a TTY serial port emulation system for the Perle 
device/terminal server families. TruePort provides a standard TTY interface to 
application software which is achieved using the TruePort serial device driver.


TruePort Full mode protocol will allow an application to take full control of the 
remote serial port and use all functions available e.g. setting baud rates, flow
control settings and raising and lowering modem pins etc.

TruePort Lite Mode will allow an application to just transmit and receive data 
to and from the port.  Any baud rates or flow control settings must be 
configured on the terminal server before the application is started.

TruePort supports the following Perle hardware in TruePort Full Mode
protocol:
    - All IOLAN Family Models, including DS, TS, SDS, STS, SCS
    - JetStream 8500, JetStream 4000
    - LanStream 2000
    - LinkStream 2000.

It also supports IOLAN+ and other 3rd party multi-port terminal servers in Lite
(raw) mode.

NOTE: 	
For TruePort Full Mode the JetStream 8500, JetStream 4000 and LanStream 2000 
require firmware version 2.3.3 or later and the LinkStream 2000 requires 
version 6.04 or later.


Installation and Configuration:
===============================

NOTE - Please remove any previous versions of TruePort software for 
SCO Openserver before attempting to install this product.

Installation uses the pkgadd utility 
(NOTE - this package is NOT a scoadmin/custom installable image).

Once the installation package has been copied onto your computer
(/tmp/tpos5-<version>.pkg), the package can be installed as follows:

Login as root user.

# cd /tmp
# uncompress tpos5-<version>.pkg.Z
# pkgadd -d /tmp/tpos5-<version>.pkg

This will install the device drivers, rebuild the UNIX kernel and create the
Required device names.

TruePort administrative files and utilities are installed in 
the /etc/trueport directory.


The standard installation creates device nodes /dev/tty[sS]0 through 
/dev/tty[sS]127.  To enable a range of ports, we have provided a script 
/etc/trueport/addports that starts the TruePort service. The "addports" command 
edits the TruePort configuration files and starts the required daemons and 
drivers.  

Once the "addports" command has configured the system, TruePort will be started 
automatically on each system reboot.

Once the ports have been enabled, you may use them as standard UNIX serial TTY's.

See the TruePort OpenServer 5 User Guide for more information on the use of this 
script and other configuration utilities.



Known Issues and Bugs
=====================

1. When each daemon runs it may create a trace file if the trace option
is specified. These files can become large so the trace option should be used
only as needed.

2. Never use the kill -9 (SIGKILL) command on a tpd process, as this prevents
tpd from exiting cleanly and restoring it's pseudo TTYs. There is a cleanup
system that operates on a system reboot that will restore TTY's lost in this 
way.

3. Never use existing device names as TruePort Lite device names as they may be
replaced by a TruePort TTY.



Release History
===============

    Version     Description
    -------     -----------
    6.2.0     * Detection and support of running TruePort Full mode with no UDP 
                protocol
              * Resolve TCSBRK drain failure when openwaittime set to -1 
    6.1.0     * Resolved serial driver incompatability issues.
              * Added new configuration fields
    6.0.0     * Added Client initiated mode which will initiate the TCP
                connection from the TruePort host when the application opens
                the TTY device.
              * Added Client I/O Access which allows Modbus Serial ASCII/RTU 
                applications or custom applications using the Perle I/O API 
                to access I/O register from Perle IOLAN DS devices.
              * Added Packet Forwarding feature which allows you to conntrol 
                how UnixWare application data is packetized before it is sent 
                over the network.
              * Allow specifying of IP address or DNS name for server mode
              * Added support for SSL connection feature and hangup feature.
    1.0.3     * Changed name of Standard mode to Full mode in documentation.
    1.0.2     * Increased supported TruePort connections from 64 to 256.
    1.0.1     * Added Standard mode as default mode. 1.0.0 only 
                supported Lite mode.
    1.0.0     * Initial Release
