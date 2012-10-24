

			Installation and Usage Information:  DECODER Utility

1.0	Supported Platforms

	Platform					Location
	========					========

	MS Windows (DOS box)		\Utilities\NFS decoder\windos\*.*
	MS-DOS				\utilities\NFS decoder\windos\*.*
	Linux x86			\utilities\NFS decoder\linux\*.*
	Solaris x86			\utilities\NFS decoder\solarisx86\*.*
	Solaris Sparcs (32/64)		\utilities\NFS decoder\solarissparc\*.*



2.0	Installation and Usage

2.1	MS Windows and DOS

	Copy all files under the \utilities\NFS decoder\windos directory to your destination
	directory on your PC. Type "decoder" with no parameters for usage
	information.

	Typical usage is:

				decoder <infile.enc>  <outfile.dec>

	Note: infile and outfile names are limited to the DOS 8.3 format 
	(or eight characters).  Any long filenames must be renamed before running
	the utility.


2.2	Linux x86

	Copy all files under the \utilities\NFS decoder/linux86 directory to your destination
	directory on your Linux PC.  Type "./decoder.exl" with no parameters for
	usage information.
	
	Typical usage is:

				./decoder.exl  <infile.enc>  <outfile.dec>


2.3	Solaris x86

	Copy all files under the \utilities\NFS decoder\solarisx86 directory to your destination
	directory on your Solaris x86 PC.  Type "./decoder.sol" with no parameters
	for usage information.

	Typical usage is:

				./decoder.sol  <infile.enc>  <outfile.dec>


2.3	Solaris Sparc 32/64

	Copy all files under the \utilities\NFS decoder/solsparc directory to your destination
	directory on your solaris Sparcs workstation.  Type ./decoder.spc  with no
	parameters for usage information.

	Typical usage is:

				./decoder.spc  <infile.enc>  <outfile.dec>

	Note:	Linux x86 and Solaris versions of the decoder utility do not have 
			the 8.3 filename limitations like on the MS Windows/Dos version.

