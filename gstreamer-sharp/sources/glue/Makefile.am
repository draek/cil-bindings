LIBRARY_NAME=libgstreamersharpglue-0.10
BUILD_DIR = ../
LIBRARY = $(BUILD_DIR)$(LIBRARY_NAME).so
CC = gcc
CFLAGS = -g -O2 -Wall -D_FORTIFY_SOURCE=2 -fPIC -DPIC $(GSTREAMER_CFLAGS)
LDFLAGS = -shared -Wl,-soname,$(LIBRARY_NAME).so $(GSTREAMER_LIBS)

glue_includes="gst/gstbin.h gst/gstpipeline.h gst/gstsegment.h"

sources = \
	buffer.c \
	clock.c \
	message.c \
	miniobject.c \
	bin.c

build_sources = generated.c $(sources)
build_objects = ${build_sources:.c=.o}

all: $(LIBRARY)

$(LIBRARY): $(build_objects)
	$(CC) -o $(LIBRARY) $(build_objects) $(LDFLAGS)

%.o: %.c
	$(CC) $(CFLAGS) -c $*.c -o $@

clean:
	rm -f $(OBJECTS)
	rm -f $(LIBRARY)

