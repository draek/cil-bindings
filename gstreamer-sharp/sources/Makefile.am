SUBDIRS = . glue

pkgconfigdir = $(libdir)/pkgconfig
pkgconfig_DATA = $(ASSEMBLY_PC).pc

RAW_API=$(ASSEMBLY_NAME)-api.raw
API=$(ASSEMBLY_NAME)-api.xml
METADATA=$(ASSEMBLY_NAME).metadata

glue_includes="gst/gstbin.h gst/gstpipeline.h gst/gstsegment.h"

sources = \
	DynamicSignal.cs \
	BindingHelper.cs \
	Application.cs \
	Version.cs \
	CommonTags.cs \
	plugins-base/PlayBin.cs \
	plugins-base/DecodeBin.cs \
	plugins-base/TypeFindElement.cs

build_sources = AssemblyInfo.cs $(sources)

customs = \
	Bin.custom \
	Buffer.custom \
	Bus.custom \
	Caps.custom \
	Clock.custom \
	Debug.custom \
	Element.custom \
	Message.custom \
	Pad.custom \
	Object.custom

CLEANFILES = $(ASSEMBLY_NAME).dll generated-stamp generated/*.cs $(API)

DISTCLEANFILES= AssemblyInfo.cs $(ASSEMBLY_PC).pc $(ASSEMBLY_NAME).dll.config

noinst_DATA = $(ASSEMBLY_NAME).dll

EXTRA_DIST = $(RAW_API) $(sources) $(customs) $(METADATA) \
	AssemblyInfo.cs.in \
	$(ASSEMBLY_NAME).snk \
	$(ASSEMBLY_NAME).dll.config.in \
	$(ASSEMBLY_NAME).pc.in \
	$(ASSEMBLY_NAME)-sources.xml

all: generated-stamp $(ASSEMBLY)

get-sources:
	cp -rf /usr/include/gstreamer-0.10 .
	@for file in gstreamer-0.10/gst/*.h; do                  \
		sed -i -e 's,^GSTREAMER_API ,/*GSTREAMER_API*/\r,' $$file; \
	done
#	@for file in gstreamer-0.10/gst/base/*.h; do                  \
#		sed -i -e 's,^GSTREAMER_API ,/*GSTREAMER_API*/\r,' $$file; \
#	done
#	@for file in gstreamer-0.10/gst/audio/*.h; do                  \
#		sed -i -e 's,^GSTREAMER_API ,/*GSTREAMER_API*/\r,' $$file; \
#	done
#	@for file in gstreamer-0.10/gst/video/*.h; do                  \
#		sed -i -e 's,^GSTREAMER_API ,/*GSTREAMER_API*/\r,' $$file; \
#	done

api: get-sources
	PERLLIB=$(GLIBSHARP_PREFIX)/share/perl5 \
	PATH=$(GLIBSHARP_PREFIX)/bin:$$PATH \
	$(GAPI_PARSER) $(ASSEMBLY_NAME)-sources.xml

$(API): $(srcdir)/$(RAW_API) $(srcdir)/$(METADATA)
	cp $(srcdir)/$(RAW_API) $(API)
	chmod u+w $(API)
	$(GAPI_FIXUP) --api=$(srcdir)/$(API) --metadata=$(srcdir)/$(METADATA)

generated-stamp: $(API)
	 $(GAPI_CODEGEN) --generate $(srcdir)/$(API) $(GLIB_SHARP_CFLAGS) \
	 	--outdir=generated --customdir=$(srcdir) \
		--glue-filename=glue/generated.c --gluelib-name=libgstreamersharpglue-0.10 \
		--glue-includes=$(glue_includes) \
	 	--assembly-name=$(ASSEMBLY_NAME) && touch generated-stamp

$(ASSEMBLY_NAME).dll: $(build_sources) generated-stamp
	$(CSC) -nowarn:169 -unsafe -target:library $(GLIB_SHARP_LIBS) \
		$(build_sources) generated/*.cs -out:$(ASSEMBLY_NAME).dll

install-data-local:
	echo "$(GACUTIL) /i $(ASSEMBLY_NAME).dll /f $(GACUTIL_FLAGS)";  \
        $(GACUTIL) /i $(ASSEMBLY_NAME).dll /f $(GACUTIL_FLAGS) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS) || exit 1;

