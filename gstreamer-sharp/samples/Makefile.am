all:

TARGETS = PlayBinPlayer.exe

EXTRA_DIST = PlayBinPlayer.cs

DEBUGS = $(addsuffix .mdb, $(TARGETS))

assembly=$(ASSEMBLY_NAME).dll

CLEANFILES = $(TARGETS) $(DEBUGS) $(assembly)

references=-r:../sources/$(assembly) -pkg:gtk-sharp-2.0

all: $(TARGETS)

$(assembly): ../sources/$(assembly)
	cp ../sources/$(ASSEMBLY_NAME).dll $(srcdir)
	cp ../sources/$(ASSEMBLY_NAME).dll.config $(srcdir)
	cp ../sources/libgstreamersharpglue-0.10.so $(srcdir)

PlayBinPlayer.exe: $(assembly) $(srcdir)/PlayBinPlayer.cs
	$(CSC) /out:$@ $(references) $(srcdir)/PlayBinPlayer.cs

