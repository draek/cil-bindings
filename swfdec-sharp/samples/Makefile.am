TARGETS = GtkTest.exe

EXTRA_DIST = GtkTest.cs

DEBUGS = $(addsuffix .mdb, $(TARGETS))

assembly=$(ASSEMBLY_NAME).dll

CLEANFILES = $(TARGETS) $(DEBUGS) $(assembly)

references=-r:../sources/$(assembly) -pkg:gtk-sharp-2.0

all: $(TARGETS)

$(assembly): ../sources/$(assembly)
	cp ../sources/$(ASSEMBLY_NAME).dll $(srcdir)

GtkTest.exe: $(assembly) $(srcdir)/GtkTest.cs
	$(CSC)  /out:$@ $(references) $(srcdir)/GtkTest.cs

