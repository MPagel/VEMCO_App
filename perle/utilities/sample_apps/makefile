EXEC = ioudpbcast ioapiotp

all: $(EXEC)

ioudpbcast: ioudpbcast.c
	$(CC) -W -Wall $(CFLAGS) $(LD-CCFLAGS) $(LIBS) -o ioudpbcast ioudpbcast.c
	
ioapiotp: ioapiotp.c
	$(CC) -W -Wall $(CFLAGS) $(LD-CCFLAGS) $(LIBS) -o ioapiotp ioapiotp.c

clean:
	rm $(EXEC)
