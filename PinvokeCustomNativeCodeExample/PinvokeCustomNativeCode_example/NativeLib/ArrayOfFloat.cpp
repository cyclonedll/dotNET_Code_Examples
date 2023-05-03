//#include "framework.h"

#ifdef __cplusplus
extern "C"
{

	_declspec(dllexport) float* AOF_Create(int count)
	{
		return new float[count];
	}

	_declspec(dllexport) void AOF_Delete(float* aof)
	{
		delete aof;
	}

}
#endif // __cpluscplus

