=============================================================================
       Perle TruePort Service and Multi-port Serial Driver for Windows
               Copyright (C) 2005-2011, Perle Systems Limited.
=============================================================================

  Release           : 6.4.0
  Date              : March 2011
  O/S Compatibility : Windows 2000 32-bit
                      Windows XP, 32-bit and 64-bit
                      Windows Server 2003, 32-bit and 64-bit
                      Windows Vista, 32-bit and 64-bit
                      Windows Server 2008, 32-bit and 64-bit
                      Windows 7, 32-bit and 64-bit
                      Windows Server 2008 Release 2
=============================================================================

The TruePort Multi-port Serial Driver for Microsoft Windows 2000/XP/
Server 2003/Vista allows remote ports on network connected Terminal Servers to
be used like local system serial ports within Windows.

It supports the following Perle hardware using TruePort Full mode protocol:
    - IOLAN DS family
    - JetStream 8500, JetStream 4000
    - LanStream 2000
    - LinkStream 2000.

It also supports IOLAN+ and other 3rd party multi-port terminal servers in Lite
(raw) mode.


TruePort Full mode protocol will allow an application to take full control of
the remote serial port and use all functions available. For example setting
baud rates, flow control settings and raising, and lowering modem pins etc.

TruePort Lite Mode will allow an application to just transmit and receive data
to and from the port.  Any baud rates or flow control settings must be
configured on the terminal server before the application is started.


NOTE:
For TruePort Full Mode the JetStream 8500, JetStream 4000 and LanStream 2000
require firmware version 2.3.3 or later and the LinkStream 2000 requires
version 6.04 or later.


Driver Upgrade/Installation Procedure:
--------------------------------------
TruePort is supported on three platforms:
    x86 - 32-bit            trueport-setup-x86.exe
    amd64/x64 - 64-bit      trueport-setup-x64.exe
    itanium - 64-bit        trueport-setup-ia64.exe

Select the installation executable that is appropriate for your operation
system.

To install/upgrade TruePort, run the appropriate TruePort Setup file.

----------------------------
Windows Firewall Information
----------------------------
If you have Window's firewall enabled on your network card you may need to add
the TruePort.exe application to the firewall program exception list to run
correctly.

--------------------------
Windows Telnet Information
--------------------------
The Perle TruePort Adapter properties page provides a "Telnet Config" button to
allow easy telnet access to the configured Perle Device Server.  Some versions
of Windows do  not install the Microsoft Telnet client. You can install it
yourself by following these steps:
  1. Open the Programs and Features Control Panel applet (Start, Control Panel,
     Programs and Features).
  2. Select "Turn Windows features on or off."
  3. Select the Telnet Client option and click OK.
  4. A dialog box appears, confirming the installation of new features. After
     installation is complete, close the main Programs and Features Control
     Panel applet. The telnet function within the Trueport Adapter program will
     now be available.


-------------
Known Issues:
-------------
 * If you are upgrading from 6.2.x to 6.3.x or higher then the Full mode protocol
   will switch to using the legacy UDP protocol.  You will need to modify the
   TruePort configuration to disable legacy UDP protocol.

Release History:
----------------

Version     Description
--------------------------------------------------------------------------------
6.4.0       * Added configuration option to drain data before COM port settings
              are set on the device server serial port
            * Added an Apply button to the the Perle TruePort Adapter Settings
              dialog so that your configuration changes can be saved at any time.
            * Resolve issue were device server and TruePort Full mode window 
              count would get out of sync when several read flush commands are
              sent to the device server.
            * Modified Full Mode configuration logic to have TruePort continue 
              forever to establish the Full mode protocol.
            * Added configuration option to enable enumeration on a COM port.
              Default is off.
6.3.6       * added serenum to port driver to allow detection of modems. Added
              code to send flag to IOLAN to not translate data to NULL if the 
              data was recieved with an Parity or Frame error.
6.3.5       * Fixed issue where TruePort could not use an SSL certificate with 
              an encrypted private key.
6.3.4       * Fixed issue with "Always return successful" option during a write
              to a TruePort Com port. Will now return successful even if tcp 
			  connection is down.
6.3.3       * Resolve issue with not listening for duplicate TCP port numbers 
              configured across both IPv4 and IPv6 TruePort adapters.
            * Resolve field handling issues on the Add Ports dialog.
6.3.2       * Added feature to accept server-initiated TCP connections from any 
              IP address if the user configures either a zero IPv4 address 
              (0.0.0.0) or a zero IPv6 address (::)
6.3.1       * Resolve timing issue where applications could hang on writes when
              Advanced Settings - Simulate COM port transmit delays is enabled
6.3.0       * Added configuration to allow use of legacy UDP protocol for Full 
              mode.
            * Fixed issue where client initiated SSL sessions would continuously 
              retry failed SSL connections.
            * Fixed several issues where TruePort service would crash when 
              installing new software or when saving TruePort configuration 
              changes.
            * Fixed blue screen crash when application is closed with open ports
              in certain situations.
            * Fixed issue where sessions with duplicate TCP ports on different 
              IP addresses would fail to connect.
            * Fixed issue where in certain situations application would stop
              receiving read data.
6.2.3       * Fixed issue with port Connection Profile not being saved when 
              modified.
            * Fixed TruePort properties page crash when removing the last
              port or all ports on a TruePort adapter. Also fixes issue with 
              wrong COM port being removed.
6.2.2       * Fixed RS232 DTR and RTS signal timing issue
            * Fixed issue where signal changes were being reported to the
              the application when they had not changed
6.2.1       * Fix for blanking of SSL peer certificate validation criteria
              fields in properties page.
            * Fixed problems with Country, Locality and Organization fields for
              SSL peer certificate validation criteria.
6.2.0       * Support for Microsoft Windows Server 2008.
            * Added support of IPv6 addressing
            * Detection and support of running TruePort Full mode with no UDP
              protocol
6.1.1       * Fixed memory leak when open of serial port failed
6.1.0       * Fixed serial driver incompatability issues
            * Added new configuration fields
            * The number of COM ports supported has been increased from 256 to
              4096 ports.
            * Added support for Windows Vista in both the 32-bit and 64-bit
              versions.
6.0.1       * Fixed timing issue where TruePort service would sometimes fail
              to start automatically on bootup
            * Duplicate port numbers causes "TruePort Device management Tools"
              to fail and generate a report on ia64 systems.
6.0.0       * Added support for Client Initiated, IOLAN I/O Access and Packet
              Forwarding features.
            * Added support for configuration for individual ports
            * Added  installer and TruePort Device Management Tool
            * Created driver package to run on ia64 (Itanium) version of
              Windows XP/2003
            * Created driver package to run on x64 version of Windows XP/2003
5.0.2       * Fixed issue with mark/space parity not working in Full mode
5.0.1       * Fixed issue which occurred when the property page for the
              device server was modified while a connection to Device server
              was coming up.
            * Added seperate message queue for UDP traffic for full mode. This
              prevents lockup when there is a lot of RS-232 signal changes when
              the LAN is busy.

5.0.0       * Added support for SSL connection feature OpenSSL Toolkit 0.9.7D.
            * The version number jumped from 1.0.4 to 5.0.0 so that all
              versions of TruePort with SSL would be at the same level.
            * Added serial Read/Write delay configuration.
            * Fixed issues with TruePort Full mode being lost after TCP/IP
              disconnect.

1.0.4       * Fixed issue with modem signals not being sent properly to
              applications.

1.0.3       * Added support for IOLAN DS Family of products.

1.0.2       * Properly handle controlling RTS signal in TruePort Full mode.
            * Proper handling of Break.

1.0.1       * Fixed issue with Unable to re-establish connections with the
              Jetstream after a reboot of the Jetstream.

1.0.0       * First approved release.
--------------------------------------------------------------------------------


COPYRIGHT NOTICES:
------------------

  The OpenSSL Project

  Copyright (c) 1998-2004 The OpenSSL Project.  All rights reserved.

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions
  are met:

  1. Redistributions of source code must retain the above copyright
     notice, this list of conditions and the following disclaimer.

  2. Redistributions in binary form must reproduce the above copyright
     notice, this list of conditions and the following disclaimer in
     the documentation and/or other materials provided with the
     distribution.

  3. All advertising materials mentioning features or use of this
     software must display the following acknowledgment:
     "This product includes software developed by the OpenSSL Project
     for use in the OpenSSL Toolkit. (http://www.openssl.org/)"

  4. The names "OpenSSL Toolkit" and "OpenSSL Project" must not be used to
     endorse or promote products derived from this software without
     prior written permission. For written permission, please contact
     openssl-core@openssl.org.

  5. Products derived from this software may not be called "OpenSSL"
     nor may "OpenSSL" appear in their names without prior written
     permission of the OpenSSL Project.

  6. Redistributions of any form whatsoever must retain the following
     acknowledgment:
     "This product includes software developed by the OpenSSL Project
     for use in the OpenSSL Toolkit (http://www.openssl.org/)"

  THIS SOFTWARE IS PROVIDED BY THE OpenSSL PROJECT ``AS IS'' AND ANY
  EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
  PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE OpenSSL PROJECT OR
  ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
  HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
  STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
  OF THE POSSIBILITY OF SUCH DAMAGE.


  Eric Young

  This product includes cryptographic software written by Eric Young
  (eay@cryptsoft.com).  This product includes software written by Tim
   Hudson (tjh@cryptsoft.com).


  Copyright (C) 1995-1998 Eric Young (eay@cryptsoft.com)
  All rights reserved.

  This package is an SSL implementation written
  by Eric Young (eay@cryptsoft.com).
  The implementation was written so as to conform with Netscapes SSL.

  This library is free for commercial and non-commercial use as long as
  the following conditions are aheared to.  The following conditions
  apply to all code found in this distribution, be it the RC4, RSA,
  lhash, DES, etc., code; not just the SSL code.  The SSL documentation
  included with this distribution is covered by the same copyright terms
  except that the holder is Tim Hudson (tjh@cryptsoft.com).

  Copyright remains Eric Young's, and as such any Copyright notices in
  the code are not to be removed.
  If this package is used in a product, Eric Young should be given attribution
  as the author of the parts of the library used.
  This can be in the form of a textual message at program startup or
  in documentation (online or textual) provided with the package.

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions
  are met:
  1. Redistributions of source code must retain the copyright
     notice, this list of conditions and the following disclaimer.
  2. Redistributions in binary form must reproduce the above copyright
     notice, this list of conditions and the following disclaimer in the
     documentation and/or other materials provided with the distribution.
  3. All advertising materials mentioning features or use of this software
     must display the following acknowledgement:
     "This product includes cryptographic software written by
      Eric Young (eay@cryptsoft.com)"
     The word 'cryptographic' can be left out if the rouines from the library
     being used are not cryptographic related :-).
  4. If you include any Windows specific code (or a derivative thereof) from
     the apps directory (application code) you must include an acknowledgement:
     "This product includes software written by Tim Hudson (tjh@cryptsoft.com)"

  THIS SOFTWARE IS PROVIDED BY ERIC YOUNG ``AS IS'' AND
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
  ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
  FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
  DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
  OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
  HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
  OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
  SUCH DAMAGE.


******************************************
******          END README          ******
******************************************
