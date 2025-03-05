#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif





struct String_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

struct Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct CalendarTriggerData_t95CDF224E7B6165CE42899A54B3BADAE1B4BBB23 
{
	int32_t ___year;
	int32_t ___month;
	int32_t ___day;
	int32_t ___hour;
	int32_t ___minute;
	int32_t ___second;
	uint8_t ___repeats;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct LocationTriggerData_t6C709C3123CDD15B8FA218B532776BAB4B0172FC 
{
	double ___latitude;
	double ___longitude;
	float ___radius;
	uint8_t ___notifyOnEntry;
	uint8_t ___notifyOnExit;
	uint8_t ___repeats;
};
struct TimeTriggerData_t110F07D01BDEC0F8D7C1E625A581638C9AEE6823 
{
	int32_t ___interval;
	uint8_t ___repeats;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct iOSAuthorizationRequestData_t216987B5D9A6729184F783B5F68AE9124B9321AC 
{
	int32_t ___granted;
	String_t* ___error;
	String_t* ___deviceToken;
};
struct iOSAuthorizationRequestData_t216987B5D9A6729184F783B5F68AE9124B9321AC_marshaled_pinvoke
{
	int32_t ___granted;
	char* ___error;
	char* ___deviceToken;
};
struct iOSAuthorizationRequestData_t216987B5D9A6729184F783B5F68AE9124B9321AC_marshaled_com
{
	int32_t ___granted;
	Il2CppChar* ___error;
	Il2CppChar* ___deviceToken;
};
struct TriggerData_t5B00176E3EB034DB9078E419981580696EB8D39E 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			TimeTriggerData_t110F07D01BDEC0F8D7C1E625A581638C9AEE6823 ___timeInterval;
		};
		#pragma pack(pop, tp)
		struct
		{
			TimeTriggerData_t110F07D01BDEC0F8D7C1E625A581638C9AEE6823 ___timeInterval_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			CalendarTriggerData_t95CDF224E7B6165CE42899A54B3BADAE1B4BBB23 ___calendar;
		};
		#pragma pack(pop, tp)
		struct
		{
			CalendarTriggerData_t95CDF224E7B6165CE42899A54B3BADAE1B4BBB23 ___calendar_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			LocationTriggerData_t6C709C3123CDD15B8FA218B532776BAB4B0172FC ___location;
		};
		#pragma pack(pop, tp)
		struct
		{
			LocationTriggerData_t6C709C3123CDD15B8FA218B532776BAB4B0172FC ___location_forAlignmentOnly;
		};
	};
};
struct iOSNotificationData_t57D24EBD788D6C71F203ACE14688358AFA08BDED 
{
	String_t* ___identifier;
	String_t* ___title;
	String_t* ___body;
	int32_t ___badge;
	String_t* ___subtitle;
	String_t* ___categoryIdentifier;
	String_t* ___threadIdentifier;
	int32_t ___soundType;
	float ___soundVolume;
	String_t* ___soundName;
	int32_t ___interruptionLevel;
	double ___relevanceScore;
	intptr_t ___userInfo;
	intptr_t ___attachments;
	int32_t ___triggerType;
	TriggerData_t5B00176E3EB034DB9078E419981580696EB8D39E ___trigger;
};
struct iOSNotificationData_t57D24EBD788D6C71F203ACE14688358AFA08BDED_marshaled_pinvoke
{
	char* ___identifier;
	char* ___title;
	char* ___body;
	int32_t ___badge;
	char* ___subtitle;
	char* ___categoryIdentifier;
	char* ___threadIdentifier;
	int32_t ___soundType;
	float ___soundVolume;
	char* ___soundName;
	int32_t ___interruptionLevel;
	double ___relevanceScore;
	intptr_t ___userInfo;
	intptr_t ___attachments;
	int32_t ___triggerType;
	TriggerData_t5B00176E3EB034DB9078E419981580696EB8D39E ___trigger;
};
struct iOSNotificationData_t57D24EBD788D6C71F203ACE14688358AFA08BDED_marshaled_com
{
	Il2CppChar* ___identifier;
	Il2CppChar* ___title;
	Il2CppChar* ___body;
	int32_t ___badge;
	Il2CppChar* ___subtitle;
	Il2CppChar* ___categoryIdentifier;
	Il2CppChar* ___threadIdentifier;
	int32_t ___soundType;
	float ___soundVolume;
	Il2CppChar* ___soundName;
	int32_t ___interruptionLevel;
	double ___relevanceScore;
	intptr_t ___userInfo;
	intptr_t ___attachments;
	int32_t ___triggerType;
	TriggerData_t5B00176E3EB034DB9078E419981580696EB8D39E ___trigger;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif

extern "C" void DEFAULT_CALL ReversePInvokeWrapper_CultureInfo_OnCultureInfoChangedInAppX_m407BCFC1029A4485B7B063BC2F3601968C3BE577(Il2CppChar* ___0_language);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_OSSpecificSynchronizationContext_InvocationEntry_mF93C3CF6DBEC86E377576D840CAF517CB6E4D7E3(intptr_t ___0_arg);
extern "C" int32_t CDECL ReversePInvokeWrapper_RewindableAllocator_Try_mA4AF5A5088097CB6343C3CC97058959976372C35(intptr_t ___0_state, Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3* ___1_block);
extern "C" int32_t CDECL ReversePInvokeWrapper_RewindableAllocator_TryU24BurstManaged_mBB6DAE6A8CDB2E3626C38F3B65186AAF6ACBF6E8(intptr_t ___0_state, Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3* ___1_block);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_iOSNotificationsWrapper_AuthorizationRequestReceived_m4A6C75E5BFEA2C3E529F2F8CEEA8A813F428B3CD(intptr_t ___0_request, iOSAuthorizationRequestData_t216987B5D9A6729184F783B5F68AE9124B9321AC_marshaled_pinvoke ___1_data);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_iOSNotificationsWrapper_NotificationReceived_mB7F4859AF321D5AF115D70F4BD8AB131543D7524(iOSNotificationData_t57D24EBD788D6C71F203ACE14688358AFA08BDED_marshaled_pinvoke ___0_data);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_iOSNotificationsWrapper_ReceiveNSDictionaryKeyValue_m57E371B7275602846E05E0770BFA78C6641BC856(intptr_t ___0_dict, char* ___1_key, char* ___2_value);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_iOSNotificationsWrapper_ReceiveUNNotificationAttachment_mF8194BDA3434E46D10AF292C61D4F3E00B654652(intptr_t ___0_array, char* ___1_id, char* ___2_url);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_iOSNotificationsWrapper_RemoteNotificationReceived_mBF44180FD060AC5811C9979FE6E771650AA35212(iOSNotificationData_t57D24EBD788D6C71F203ACE14688358AFA08BDED_marshaled_pinvoke ___0_data);
extern "C" int32_t CDECL ReversePInvokeWrapper_SlabAllocator_Try_mCD7DED588811A6E3F78E4A14CBFE2852D8E39DEB(intptr_t ___0_allocatorState, Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3* ___1_block);
extern "C" int32_t CDECL ReversePInvokeWrapper_SlabAllocator_TryU24BurstManaged_mC48F05E806431B6537727E4D6A10550207FBB1EA(intptr_t ___0_allocatorState, Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3* ___1_block);
extern "C" int32_t CDECL ReversePInvokeWrapper_StackAllocator_Try_m093FA501B1B427E32DD9F654380B3EA56A5A4234(intptr_t ___0_allocatorState, Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3* ___1_block);
extern "C" int32_t CDECL ReversePInvokeWrapper_StackAllocator_TryU24BurstManaged_mB88D607AA12E4D9181BF1FFE81A1AC3117FDB5E2(intptr_t ___0_allocatorState, Block_tCCF620817FE305B5BF7B0FB7705B4571F976C4E3* ___1_block);
extern "C" int32_t CDECL ReversePInvokeWrapper_BurstCompilerHelper_IsBurstEnabled_m8F3C6D0129D14359B51860FBA51933C4FE92F131();


IL2CPP_EXTERN_C const Il2CppMethodPointer g_ReversePInvokeWrapperPointers[];
const Il2CppMethodPointer g_ReversePInvokeWrapperPointers[14] = 
{
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_CultureInfo_OnCultureInfoChangedInAppX_m407BCFC1029A4485B7B063BC2F3601968C3BE577),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_OSSpecificSynchronizationContext_InvocationEntry_mF93C3CF6DBEC86E377576D840CAF517CB6E4D7E3),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_RewindableAllocator_Try_mA4AF5A5088097CB6343C3CC97058959976372C35),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_RewindableAllocator_TryU24BurstManaged_mBB6DAE6A8CDB2E3626C38F3B65186AAF6ACBF6E8),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_iOSNotificationsWrapper_AuthorizationRequestReceived_m4A6C75E5BFEA2C3E529F2F8CEEA8A813F428B3CD),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_iOSNotificationsWrapper_NotificationReceived_mB7F4859AF321D5AF115D70F4BD8AB131543D7524),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_iOSNotificationsWrapper_ReceiveNSDictionaryKeyValue_m57E371B7275602846E05E0770BFA78C6641BC856),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_iOSNotificationsWrapper_ReceiveUNNotificationAttachment_mF8194BDA3434E46D10AF292C61D4F3E00B654652),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_iOSNotificationsWrapper_RemoteNotificationReceived_mBF44180FD060AC5811C9979FE6E771650AA35212),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_SlabAllocator_Try_mCD7DED588811A6E3F78E4A14CBFE2852D8E39DEB),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_SlabAllocator_TryU24BurstManaged_mC48F05E806431B6537727E4D6A10550207FBB1EA),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_StackAllocator_Try_m093FA501B1B427E32DD9F654380B3EA56A5A4234),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_StackAllocator_TryU24BurstManaged_mB88D607AA12E4D9181BF1FFE81A1AC3117FDB5E2),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_BurstCompilerHelper_IsBurstEnabled_m8F3C6D0129D14359B51860FBA51933C4FE92F131),
};
