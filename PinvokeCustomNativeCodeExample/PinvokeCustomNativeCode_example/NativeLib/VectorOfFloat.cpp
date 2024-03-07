//
//#include <vector>
//#include "framework.h"
//
////https://www.runoob.com/w3cnote/cpp-vector-container-analysis.html
//
//using namespace std;
//
////另一种写法
//#ifdef __cplusplus
//extern "C"
//{
//
//	//一个 _ 两个 __ 都行
//	_declspec(dllexport) std::vector<float>* VOF_Create()
//	{
//		return new std::vector<float>();
//	}
//
//
//	__declspec(dllexport) std::vector<float>* VOF_Create_Count(int count)
//	{
//		return new std::vector<float>(count);
//	}
//
//}
//#endif
//
//
//
//
//
//CEXPORT_API void VOF_Add(std::vector<float>* vof, float value)
//{
//	vof->push_back(value);
//}
//
//
//CEXPORT_API void VOF_Remove(vector<float>* vof, int index)
//{
//	vof->erase(vof->begin() + index);
//}
//
//CEXPORT_API void VOF_RemoveRange(vector<float>* vof, int begin, int length)
//{
//	//http://www.cplusplus.com/reference/vector/vector/erase/
//	vof->erase(vof->begin() + begin, vof->begin() + begin + length);
//}
//
//CEXPORT_API float VOF_GetElement(vector<float>* vof, int index)
//{
//	return vof->at(index);
//}
//
//CEXPORT_API void VOF_SetElement(vector<float>* vof, int index, float value)
//{
//	vof->at(index) = value;
//}
//
//CEXPORT_API void VOF_Zeros(vector<float>* vof)
//{
//	for (vector<float>::iterator iter = vof->begin(); iter != vof->end(); iter++)
//	{
//		(*iter) = 0.0f;
//	}
//}
//
//CEXPORT_API void VOF_TrimExcess(vector<float>* vof)
//{
//	vof->shrink_to_fit();
//}
//
//
//
//CEXPORT_API size_t VOF_GetSize(vector<float>* vof)
//{
//	return vof->size();
//}
//
//CEXPORT_API size_t VOF_GetCapacity(vector<float>* vof)
//{
//	return vof->capacity();
//}
//
//
//CEXPORT_API bool VOF_IsEmpty(vector<float>* vof)
//{
//	return vof->empty();
//}
//
//
//CEXPORT_API float* VOF_GetDataPointer(vector<float>* vof)
//{
//	return vof->data();
//}
//
//CEXPORT_API void VOF_Delete(vector<float>* vof)
//{
//	delete vof;
//	vof = nullptr;
//}
//
//
//CEXPORT_API float VOF_GetMax(vector<float>* vof)
//{
//	float result = vof->at(0);
//	for (size_t i = 0; i < vof->size(); i++)
//	{
//		if (vof->at(i) > result)
//		{
//			result = vof->at(i);
//		}
//	}
//	return result;
//}
//
