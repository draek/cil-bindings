#include <glib-object.h>

guint gstsharp_g_closure_get_size (void);

guint gstsharp_g_closure_get_size (void)
{
	return (guint)sizeof (GClosure);
}

