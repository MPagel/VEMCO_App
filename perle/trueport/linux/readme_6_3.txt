============================================================================
           Perle TruePort Daemon and  Driver for Linux
           Copyright (C) 2004-2009, Perle Systems Limited
=============================================================================

  Release           : 6.3.0
  Date              : February 2009
  O/S Compatibility : Linux Versions 2.2.19 or higher, 2.4.x and 2.6.x

=============================================================================

Introduction: 
=============

This is the readme belonging to the TruePort for Linux software. 


TruePort for Linux is a TTY serial port emulation system for the 
Perle device/terminal server families. TruePort provides a standard
TTY interface to application software which is achieved using the
TruePort serial device driver.

TruePort provides a more general interface via a fixed /dev TTY name,
this allows application software to send and receive data from ports on the
terminal server as if they were directly connected to the application server.


TruePort Full mode protocol will allow an application to take full control of the 
remote serial port and use all functions available e.g. setting baud rates, flow
control settings and raising and lowering modem pins etc.

TruePort Lite Mode will allow an application to just transmit and receive data 
to and from the port.  Any baud rates or flow control settings must be 
configured on the terminal server before the application is started.

TruePort supports the following Perle hardware in TruePort Full mode
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

The software consist of two parts. The part that allows a user-program
to control a fully-functional serial port is a driver in the form of a  new 
loadable kernel driver.

The other part is the daemon that handles the connection between the
kernel driver and a device/terminal server.

Installation using the .tgz file
--------------------------------

Unpack the file and go to the created directory. In the directory run 
the script:

   ./tar_install.sh

TruePort administrative files and utilities are installed in the /etc/trueport 
and /usr/bin directories.

To create and enable a range of ports we have provided a script "addports"
that starts the TruePort service. The "addports" command edits the TruePort 
configuration files and starts the required daemons and drivers.  

Once the "addports" command has configured the system, TruePort will be started 
automatically on each system reboot.

Once the ports have been enabled, you may use them as standard UNIX serial TTY's.

See the TruePort Linux User Guide for more information on the use of this 
script and other configuration utilities.


Kernel update:
==============
When a new kernel version is installed on the machine the TruePort rpm
package has to be reinstalled to get the correct kernel module for the
new kernel. The commands to do this are:

   cd /usr/share/doc/trueport/ptyx
   make
   make install

and 
   depmod -a
   /etc/init.d/trueport restart
or
   reboot the machine


or you can simply rebuild and reinstall the rpm package. (Keep in mind
that you'll have to build the package running on the kernel that will
eventually run it)


Known Issues:
=============
 * On systems with kernel 2.2.16 the compile of the psuedo-tty drivers will 
   produce warning about redefinition of variables. This only occurs when the 
   "module versioning support" is turned on. The drivers will still load and run 
   without any problems.
 * On systems with kernel 2.2.x the configuration option "openwaittime" will be 
   ignored and the TruePort driver will always return immediately with no errors 
   on a serial port open, irrelevant of the network connection status.
 * If your kernel is compiled with CONFIG_LEGACY_PTY=y then warning messages 
   will be generated in the message file when the ptyx module is loaded but the 
   driver will function properly. To eliminate these warning messages with out 
   recompiling the kernel you can add the boot parameter pty.legacy_count=0.
 * A change was made to the kernel TTY layer in 2.6.28 that pushed some of the 
   PTY specific logic from the TTY layer down to the PTY driver by introducing a
	new install method for any PTY drivers.  This new install method needs to 
   allocate the slave's TTY and initialize it but the TTY functions required to 
   do this were not exported. As a result Perle's ptyx module fails to load and
   gets unresolved symbol errors.  If you add the following 4 exports to the end
   of drivers/char/tty_io.c and recompile the kernel, then the ptyx module will
   load successfully.
     EXPORT_SYMBOL(alloc_tty_struct);
     EXPORT_SYMBOL(free_tty_struct);
     EXPORT_SYMBOL(initialize_tty_struct);
     EXPORT_SYMBOL(tty_init_termios);
   We are currently working to resolve this issue with the Linux kernel people.



Release History:
================

Version     Description
-------     -----------
6.3.0       * Added configuration to allow use of legacy UDP protocol for Full 
              mode
            * Resolve issues with TruePort Daemon being terminated in certain 
              circumstances when running Full mode protocol and the TruePort 
              connection is repeatedly going down and up.
            * Will use /etc/modprobe.d directory if /etc/modprobe.conf file does
              not exist when adding driver modules
6.2.2       * Resolve compile errors and warnings on kernels 2.6.27 and higher
            * Resolve RPM install error for Fedora Core 8
6.2.1       * Resolve issue with PPC64 and SPARC64 systems failing to process
              TruePort private IOCTLs properly.
            * added some paranoia checking in ptyx.c for out driver_data pointer
6.2.0       * Detection and support of running TruePort Full mode with no UDP 
              protocol
            * resolved some IPv6 configuration issues
6.1.1       * Resolve kernel panic during system boot up on certain 2.6 
              kernel distributions.
6.1.0       * Added support for missing "serial driver" functions.
6.0.1       * Resolved timing issue where TruePort service would sometimes fail
              to start on bootup
6.0.0       * Added Client initiated mode, Client I/O Access, and 
              Packet Forwarding features.
            * Added support to specify host IP address or DNS name for
              server mode
            * Added support for 64 bit Linux Kernels
5.1.0       * Maintenance release
5.0.4       * Resolved issue with driver not compiling under kernels 2.6.16 
             and higher.
5.0.3       * Switched code to open the tty first before listening on the 
              tcp port.
5.0.2       * Added code to ptyx driver to send a line to the 
              /var/log/message file when the driver is loaded.
5.0.1       * Added hangup option to allow trueportd to close the tty 
              device whenever the TCP connection to the terminal/device 
              server is lost. 
5.0.0       * Added support for SSL connection feature. 
            * The version number jumped from 2.3 to 5.0.0 so that all 
              versions of TruePort with SSL would be at the same level.
            * The SSL feature is not supported in kernel 2.2.x. The software
              can still be installed without this feature.
2.3         * Add support for the linux kernel 2.6.
            * Add support for linux kernel 2.2.16 and above.
2.2         * Driver became an independent loadable kernel module for 
              the kernel version 2.4.x.  No need to patch and rebuild the 
              kernel. 
2.0         * Consisted of the TruePort daemon and a patch to the pty.c
              driver module in the kernel.
