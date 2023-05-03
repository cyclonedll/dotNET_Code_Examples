
//https://zhuanlan.zhihu.com/p/325632066
//#include <mmintrin.h> //mmx
//#include <xmmintrin.h> //sse
//#include <emmintrin.h> //sse2
//#include <pmmintrin.h> //sse3

#include <xmmintrin.h>

#define CEXPORT_API extern "C" __declspec(dllexport)

CEXPORT_API	float Dot(const float* a, const float* b, unsigned int len)
{
	int i;
	float ret;
	__m128 sum = _mm_setzero_ps();
	for (i = 0; i < len; i += 8)
	{
		sum = _mm_add_ps(sum, _mm_mul_ps(_mm_loadu_ps(a + i), _mm_loadu_ps(b + i)));
		sum = _mm_add_ps(sum, _mm_mul_ps(_mm_loadu_ps(a + i + 4), _mm_loadu_ps(b + i + 4)));
	}
	sum = _mm_add_ps(sum, _mm_movehl_ps(sum, sum));
	sum = _mm_add_ss(sum, _mm_shuffle_ps(sum, sum, 0x55));
	_mm_store_ss(&ret, sum);
	return ret;
}
