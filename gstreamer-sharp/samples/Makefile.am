all:

TARGETS = MetaData.exe PlayBinPlayer.exe TypeFind.exe GtkVideoPlayer.exe

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
	$(CSC) /debug /out:$@ $(references) $(srcdir)/PlayBinPlayer.cs

MetaData.exe: $(assembly) $(srcdir)/MetaData.cs
	$(CSC) /debug /out:$@ $(references) $(srcdir)/MetaData.cs

TypeFind.exe: $(assembly) $(srcdir)/TypeFind.cs
	$(CSC) /debug /out:$@ $(references) $(srcdir)/TypeFind.cs

GtkVideoPlayer.exe: $(assembly) $(srcdir)/GtkVideoPlayer.cs
	$(CSC) /debug /out:$@ $(references) $(srcdir)/GtkVideoPlayer.cs

