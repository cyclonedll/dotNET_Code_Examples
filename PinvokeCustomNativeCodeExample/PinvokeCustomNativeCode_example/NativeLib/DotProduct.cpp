#include "framework.h"

CEXPORT_API float Dot(float* a, float* b, unsigned int len)
{
	float r = 0.0f;
	for (int i = 0; i < len; i++)
	{
		r += a[i] * b[i];
	}
	return r;
}