using System.ComponentModel;

namespace MkZ.Windows.Win32API
{
	public partial struct HRESULT
	{
		[Description("Success")]
		public const int S_OK = 0;

		[Description("False")]
		public const int S_FALSE = 1;

		[Description("")]
		public const int COR_E_OBJECTDISPOSED = unchecked((int)0x80131622);

		[Description("")]
		public const int DESTS_E_NO_MATCHING_ASSOC_HANDLER = unchecked((int)0x80040f03);

		[Description("")]
		public const int SCRIPT_E_REPORTED = unchecked((int)0x80020101);

		[Description("")]
		public const int WC_E_GREATERTHAN = unchecked((int)0xc00cee23);

		[Description("")]
		public const int WC_E_SYNTAX = unchecked((int)0xc00cee2d);

		[Description("The underlying file was converted to compound file format.")]
		public const int STG_S_CONVERTED = 0x00030200;

		[Description("The storage operation should block until more data is available.")]
		public const int STG_S_BLOCK = 0x00030201;

		[Description("The storage operation should retry immediately.")]
		public const int STG_S_RETRYNOW = 0x00030202;

		[Description("The notified event sink will not influence the storage operation.")]
		public const int STG_S_MONITORING = 0x00030203;

		[Description("Multiple opens prevent consolidated (commit succeeded).")]
		public const int STG_S_MULTIPLEOPENS = 0x00030204;

		[Description("Consolidation of the storage file failed (commit succeeded).")]
		public const int STG_S_CONSOLIDATIONFAILED = 0x00030205;

		[Description("Consolidation of the storage file is inappropriate (commit succeeded).")]
		public const int STG_S_CANNOTCONSOLIDATE = 0x00030206;

		[Description("Use the registry database to provide the requested information.")]
		public const int OLE_S_USEREG = 0x00040000;

		[Description("Success, but static.")]
		public const int OLE_S_STATIC = 0x00040001;

		[Description("Macintosh clipboard format.")]
		public const int OLE_S_MAC_CLIPFORMAT = 0x00040002;

		[Description("Successful drop took place.")]
		public const int DRAGDROP_S_DROP = 0x00040100;

		[Description("Drag-drop operation canceled.")]
		public const int DRAGDROP_S_CANCEL = 0x00040101;

		[Description("Use the default cursor.")]
		public const int DRAGDROP_S_USEDEFAULTCURSORS = 0x00040102;

		[Description("Data has same FORMATETC.")]
		public const int DATA_S_SAMEFORMATETC = 0x00040130;

		[Description("View is already frozen.")]
		public const int VIEW_S_ALREADY_FROZEN = 0x00040140;

		[Description("FORMATETC not supported.")]
		public const int CACHE_S_FORMATETC_NOTSUPPORTED = 0x00040170;

		[Description("Same cache.")]
		public const int CACHE_S_SAMECACHE = 0x00040171;

		[Description("Some caches are not updated.")]
		public const int CACHE_S_SOMECACHES_NOTUPDATED = 0x00040172;

		[Description("Invalid verb for OLE object.")]
		public const int OLEOBJ_S_INVALIDVERB = 0x00040180;

		[Description("Verb number is valid but verb cannot be done now.")]
		public const int OLEOBJ_S_CANNOT_DOVERB_NOW = 0x00040181;

		[Description("Invalid window handle passed.")]
		public const int OLEOBJ_S_INVALIDHWND = 0x00040182;

		[Description("Message is too long; some of it had to be truncated before displaying.")]
		public const int INPLACE_S_TRUNCATED = 0x000401A0;

		[Description("Unable to convert OLESTREAM to IStorage.")]
		public const int CONVERT10_S_NO_PRESENTATION = 0x000401C0;

		[Description("Moniker reduced to itself.")]
		public const int MK_S_REDUCED_TO_SELF = 0x000401E2;

		[Description("Common prefix is this moniker.")]
		public const int MK_S_ME = 0x000401E4;

		[Description("Common prefix is input moniker.")]
		public const int MK_S_HIM = 0x000401E5;

		[Description("Common prefix is both monikers.")]
		public const int MK_S_US = 0x000401E6;

		[Description("Moniker is already registered in running object table.")]
		public const int MK_S_MONIKERALREADYREGISTERED = 0x000401E7;

		[Description("An event was able to invoke some, but not all, of the subscribers.")]
		public const int EVENT_S_SOME_SUBSCRIBERS_FAILED = 0x00040200;

		[Description("An event was delivered, but there were no subscribers.")]
		public const int EVENT_S_NOSUBSCRIBERS = 0x00040202;

		[Description("The task is ready to run at its next scheduled time.")]
		public const int SCHED_S_TASK_READY = 0x00041300;

		[Description("The task is currently running.")]
		public const int SCHED_S_TASK_RUNNING = 0x00041301;

		[Description("The task will not run at the scheduled times because it has been disabled.")]
		public const int SCHED_S_TASK_DISABLED = 0x00041302;

		[Description("The task has not yet run.")]
		public const int SCHED_S_TASK_HAS_NOT_RUN = 0x00041303;

		[Description("There are no more runs scheduled for this task.")]
		public const int SCHED_S_TASK_NO_MORE_RUNS = 0x00041304;

		[Description("One or more of the properties that are needed to run this task on a schedule have not been set.")]
		public const int SCHED_S_TASK_NOT_SCHEDULED = 0x00041305;

		[Description("The last run of the task was terminated by the user.")]
		public const int SCHED_S_TASK_TERMINATED = 0x00041306;

		[Description("Either the task has no triggers, or the existing triggers are disabled or not set.")]
		public const int SCHED_S_TASK_NO_VALID_TRIGGERS = 0x00041307;

		[Description("Event triggers do not have set run times.")]
		public const int SCHED_S_EVENT_TRIGGER = 0x00041308;

		[Description("The task is registered, but not all specified triggers will start the task.")]
		public const int SCHED_S_SOME_TRIGGERS_FAILED = 0x0004131B;

		[Description("The task is registered, but it might fail to start. Batch logon privilege needs to be enabled for the task principal.")]
		public const int SCHED_S_BATCH_LOGON_PROBLEM = 0x0004131C;

		[Description("An asynchronous operation was specified. The operation has begun, but its outcome is not known yet.")]
		public const int XACT_S_ASYNC = 0x0004D000;

		[Description("The method call succeeded because the transaction was read-only.")]
		public const int XACT_S_READONLY = 0x0004D002;

		[Description("The transaction was successfully aborted. However, this is a coordinated transaction, and a number of enlisted resources were aborted outright because they could not support abort-retaining semantics.")]
		public const int XACT_S_SOMENORETAIN = 0x0004D003;

		[Description("No changes were made during this call, but the sink wants another chance to look if any other sinks make further changes.")]
		public const int XACT_S_OKINFORM = 0x0004D004;

		[Description("The sink is content and wants the transaction to proceed. Changes were made to one or more resources during this call.")]
		public const int XACT_S_MADECHANGESCONTENT = 0x0004D005;

		[Description("The sink is for the moment and wants the transaction to proceed, but if other changes are made following this return by other event sinks, this sink wants another chance to look.")]
		public const int XACT_S_MADECHANGESINFORM = 0x0004D006;

		[Description("The transaction was successfully aborted. However, the abort was nonretaining.")]
		public const int XACT_S_ALLNORETAIN = 0x0004D007;

		[Description("An abort operation was already in progress.")]
		public const int XACT_S_ABORTING = 0x0004D008;

		[Description("The resource manager has performed a single-phase commit of the transaction.")]
		public const int XACT_S_SINGLEPHASE = 0x0004D009;

		[Description("The local transaction has not aborted.")]
		public const int XACT_S_LOCALLY_OK = 0x0004D00A;

		[Description("The resource manager has requested to be the coordinator (last resource manager) for the transaction.")]
		public const int XACT_S_LASTRESOURCEMANAGER = 0x0004D010;

		[Description("Not all the requested interfaces were available.")]
		public const int CO_S_NOTALLINTERFACES = 0x00080012;

		[Description("The specified machine name was not found in the cache.")]
		public const int CO_S_MACHINENAMENOTFOUND = 0x00080013;

		[Description("The function completed successfully, but it must be called again to complete the context.")]
		public const int SEC_I_CONTINUE_NEEDED = 0x00090312;

		[Description("The function completed successfully, but CompleteToken must be called.")]
		public const int SEC_I_COMPLETE_NEEDED = 0x00090313;

		[Description("The function completed successfully, but both CompleteToken and this function must be called to complete the context.")]
		public const int SEC_I_COMPLETE_AND_CONTINUE = 0x00090314;

		[Description("The logon was completed, but no network authority was available. The logon was made using locally known information.")]
		public const int SEC_I_LOCAL_LOGON = 0x00090315;

		[Description("The context has expired and can no longer be used.")]
		public const int SEC_I_CONTEXT_EXPIRED = 0x00090317;

		[Description("The credentials supplied were not complete and could not be verified. Additional information can be returned from the context.")]
		public const int SEC_I_INCOMPLETE_CREDENTIALS = 0x00090320;

		[Description("The context data must be renegotiated with the peer.")]
		public const int SEC_I_RENEGOTIATE = 0x00090321;

		[Description("There is no LSA mode context associated with this context.")]
		public const int SEC_I_NO_LSA_CONTEXT = 0x00090323;

		[Description("A signature operation must be performed before the user can authenticate.")]
		public const int SEC_I_SIGNATURE_NEEDED = 0x0009035C;

		[Description("The protected data needs to be reprotected.")]
		public const int CRYPT_I_NEW_PROTECTION_REQUIRED = 0x00091012;

		[Description("The requested operation is pending completion.")]
		public const int NS_S_CALLPENDING = 0x000D0000;

		[Description("The requested operation was aborted by the client.")]
		public const int NS_S_CALLABORTED = 0x000D0001;

		[Description("The stream was purposefully stopped before completion.")]
		public const int NS_S_STREAM_TRUNCATED = 0x000D0002;

		[Description("The requested operation has caused the source to rebuffer.")]
		public const int NS_S_REBUFFERING = 0x000D0BC8;

		[Description("The requested operation has caused the source to degrade codec quality.")]
		public const int NS_S_DEGRADING_QUALITY = 0x000D0BC9;

		[Description("The transcryptor object has reached end of file.")]
		public const int NS_S_TRANSCRYPTOR_EOF = 0x000D0BDB;

		[Description("An upgrade is needed for the theme manager to correctly show this skin. Skin reports version: %.1f.")]
		public const int NS_S_WMP_UI_VERSIONMISMATCH = 0x000D0FE8;

		[Description("An error occurred in one of the UI components.")]
		public const int NS_S_WMP_EXCEPTION = 0x000D0FE9;

		[Description("Successfully loaded a GIF file.")]
		public const int NS_S_WMP_LOADED_GIF_IMAGE = 0x000D1040;

		[Description("Successfully loaded a PNG file.")]
		public const int NS_S_WMP_LOADED_PNG_IMAGE = 0x000D1041;

		[Description("Successfully loaded a BMP file.")]
		public const int NS_S_WMP_LOADED_BMP_IMAGE = 0x000D1042;

		[Description("Successfully loaded a JPG file.")]
		public const int NS_S_WMP_LOADED_JPG_IMAGE = 0x000D1043;

		[Description("Drop this frame.")]
		public const int NS_S_WMG_FORCE_DROP_FRAME = 0x000D104F;

		[Description("The specified stream has already been rendered.")]
		public const int NS_S_WMR_ALREADYRENDERED = 0x000D105F;

		[Description("The specified type partially matches this pin type.")]
		public const int NS_S_WMR_PINTYPEPARTIALMATCH = 0x000D1060;

		[Description("The specified type fully matches this pin type.")]
		public const int NS_S_WMR_PINTYPEFULLMATCH = 0x000D1061;

		[Description("The timestamp is late compared to the current render position. Advise dropping this frame.")]
		public const int NS_S_WMG_ADVISE_DROP_FRAME = 0x000D1066;

		[Description("The timestamp is severely late compared to the current render position. Advise dropping everything up to the next key frame.")]
		public const int NS_S_WMG_ADVISE_DROP_TO_KEYFRAME = 0x000D1067;

		[Description("No burn rights. You will be prompted to buy burn rights when you try to burn this file to an audio CD.")]
		public const int NS_S_NEED_TO_BUY_BURN_RIGHTS = 0x000D10DB;

		[Description("Failed to clear playlist because it was aborted by user.")]
		public const int NS_S_WMPCORE_PLAYLISTCLEARABORT = 0x000D10FE;

		[Description("Failed to remove item in the playlist since it was aborted by user.")]
		public const int NS_S_WMPCORE_PLAYLISTREMOVEITEMABORT = 0x000D10FF;

		[Description("Playlist is being generated asynchronously.")]
		public const int NS_S_WMPCORE_PLAYLIST_CREATION_PENDING = 0x000D1102;

		[Description("Validation of the media is pending.")]
		public const int NS_S_WMPCORE_MEDIA_VALIDATION_PENDING = 0x000D1103;

		[Description("Encountered more than one Repeat block during ASX processing.")]
		public const int NS_S_WMPCORE_PLAYLIST_REPEAT_SECONDARY_SEGMENTS_IGNORED = 0x000D1104;

		[Description("Current state of WMP disallows calling this method or property.")]
		public const int NS_S_WMPCORE_COMMAND_NOT_AVAILABLE = 0x000D1105;

		[Description("Name for the playlist has been auto generated.")]
		public const int NS_S_WMPCORE_PLAYLIST_NAME_AUTO_GENERATED = 0x000D1106;

		[Description("The imported playlist does not contain all items from the original.")]
		public const int NS_S_WMPCORE_PLAYLIST_IMPORT_MISSING_ITEMS = 0x000D1107;

		[Description("The M3U playlist has been ignored because it only contains one item.")]
		public const int NS_S_WMPCORE_PLAYLIST_COLLAPSED_TO_SINGLE_MEDIA = 0x000D1108;

		[Description("The open for the child playlist associated with this media is pending.")]
		public const int NS_S_WMPCORE_MEDIA_CHILD_PLAYLIST_OPEN_PENDING = 0x000D1109;

		[Description("More nodes support the interface requested, but the array for returning them is full.")]
		public const int NS_S_WMPCORE_MORE_NODES_AVAIABLE = 0x000D110A;

		[Description("Backup or Restore successful!.")]
		public const int NS_S_WMPBR_SUCCESS = 0x000D1135;

		[Description("Transfer complete with limitations.")]
		public const int NS_S_WMPBR_PARTIALSUCCESS = 0x000D1136;

		[Description("Request to the effects control to change transparency status to transparent.")]
		public const int NS_S_WMPEFFECT_TRANSPARENT = 0x000D1144;

		[Description("Request to the effects control to change transparency status to opaque.")]
		public const int NS_S_WMPEFFECT_OPAQUE = 0x000D1145;

		[Description("The requested application pane is performing an operation and will not be released.")]
		public const int NS_S_OPERATION_PENDING = 0x000D114E;

		[Description("The file is only available for purchase when you buy the entire album.")]
		public const int NS_S_TRACK_BUY_REQUIRES_ALBUM_PURCHASE = 0x000D1359;

		[Description("There were problems completing the requested navigation. There are identifiers missing in the catalog.")]
		public const int NS_S_NAVIGATION_COMPLETE_WITH_ERRORS = 0x000D135E;

		[Description("Track already downloaded.")]
		public const int NS_S_TRACK_ALREADY_DOWNLOADED = 0x000D1361;

		[Description("The publishing point successfully started, but one or more of the requested data writer plug-ins failed.")]
		public const int NS_S_PUBLISHING_POINT_STARTED_WITH_FAILED_SINKS = 0x000D1519;

		[Description("Status message: The license was acquired.")]
		public const int NS_S_DRM_LICENSE_ACQUIRED = 0x000D2726;

		[Description("Status message: The security upgrade has been completed.")]
		public const int NS_S_DRM_INDIVIDUALIZED = 0x000D2727;

		[Description("Status message: License monitoring has been canceled.")]
		public const int NS_S_DRM_MONITOR_CANCELLED = 0x000D2746;

		[Description("Status message: License acquisition has been canceled.")]
		public const int NS_S_DRM_ACQUIRE_CANCELLED = 0x000D2747;

		[Description("The track is burnable and had no playlist burn limit.")]
		public const int NS_S_DRM_BURNABLE_TRACK = 0x000D276E;

		[Description("The track is burnable but has a playlist burn limit.")]
		public const int NS_S_DRM_BURNABLE_TRACK_WITH_PLAYLIST_RESTRICTION = 0x000D276F;

		[Description("A security upgrade is required to perform the operation on this media file.")]
		public const int NS_S_DRM_NEEDS_INDIVIDUALIZATION = 0x000D27DE;

		[Description("Installation was successful; however, some file cleanup is not complete. For best results, restart your computer.")]
		public const int NS_S_REBOOT_RECOMMENDED = 0x000D2AF8;

		[Description("Installation was successful; however, some file cleanup is not complete. To continue, you must restart your computer.")]
		public const int NS_S_REBOOT_REQUIRED = 0x000D2AF9;

		[Description("EOS hit during rewinding.")]
		public const int NS_S_EOSRECEDING = 0x000D2F09;

		[Description("Internal.")]
		public const int NS_S_CHANGENOTICE = 0x000D2F0D;

		[Description("The IO was completed by a filter.")]
		public const int ERROR_FLT_IO_COMPLETE = 0x001F0001;

		[Description("No mode is pinned on the specified VidPN source or target.")]
		public const int ERROR_GRAPHICS_MODE_NOT_PINNED = 0x00262307;

		[Description("Specified mode set does not specify preference for one of its modes.")]
		public const int ERROR_GRAPHICS_NO_PREFERRED_MODE = 0x0026231E;

		[Description("Specified data set (for example, mode set, frequency range set, descriptor set, and topology) is empty.")]
		public const int ERROR_GRAPHICS_DATASET_IS_EMPTY = 0x0026234B;

		[Description("Specified data set (for example, mode set, frequency range set, descriptor set, and topology) does not contain any more elements.")]
		public const int ERROR_GRAPHICS_NO_MORE_ELEMENTS_IN_DATASET = 0x0026234C;

		[Description("Specified content transformation is not pinned on the specified VidPN present path.")]
		public const int ERROR_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_PINNED = 0x00262351;

		[Description("Property value will be ignored.")]
		public const int PLA_S_PROPERTY_IGNORED = 0x00300100;

		[Description("The request will be completed later by a Network Driver Interface Specification (NDIS) status indication.")]
		public const int ERROR_NDIS_INDICATION_REQUIRED = 0x00340001;

		[Description("The VolumeSequenceNumber of a MOVE_NOTIFICATION request is incorrect.")]
		public const int TRK_S_OUT_OF_SYNC = 0x0DEAD100;

		[Description("The VolumeID in a request was not found in the server's ServerVolumeTable.")]
		public const int TRK_VOLUME_NOT_FOUND = 0x0DEAD102;

		[Description("A notification was sent to the LnkSvrMessage method, but the RequestMachine for the request was not the VolumeOwner for a VolumeID in the request.")]
		public const int TRK_VOLUME_NOT_OWNED = 0x0DEAD103;

		[Description("The server received a MOVE_NOTIFICATION request, but the FileTable size limit has already been reached.")]
		public const int TRK_S_NOTIFICATION_QUOTA_EXCEEDED = 0x0DEAD107;

		[Description("The Title Server %1 is running.")]
		public const int NS_I_TIGER_START = 0x400D004F;

		[Description("Content Server %1 (%2) is starting.")]
		public const int NS_I_CUB_START = 0x400D0051;

		[Description("Content Server %1 (%2) is running.")]
		public const int NS_I_CUB_RUNNING = 0x400D0052;

		[Description("Disk %1 ( %2 ) on Content Server %3, is running.")]
		public const int NS_I_DISK_START = 0x400D0054;

		[Description("Started rebuilding disk %1 ( %2 ) on Content Server %3.")]
		public const int NS_I_DISK_REBUILD_STARTED = 0x400D0056;

		[Description("Finished rebuilding disk %1 ( %2 ) on Content Server %3.")]
		public const int NS_I_DISK_REBUILD_FINISHED = 0x400D0057;

		[Description("Aborted rebuilding disk %1 ( %2 ) on Content Server %3.")]
		public const int NS_I_DISK_REBUILD_ABORTED = 0x400D0058;

		[Description("A NetShow administrator at network location %1 set the data stream limit to %2 streams.")]
		public const int NS_I_LIMIT_FUNNELS = 0x400D0059;

		[Description("A NetShow administrator at network location %1 started disk %2.")]
		public const int NS_I_START_DISK = 0x400D005A;

		[Description("A NetShow administrator at network location %1 stopped disk %2.")]
		public const int NS_I_STOP_DISK = 0x400D005B;

		[Description("A NetShow administrator at network location %1 stopped Content Server %2.")]
		public const int NS_I_STOP_CUB = 0x400D005C;

		[Description("A NetShow administrator at network location %1 aborted user session %2 from the system.")]
		public const int NS_I_KILL_USERSESSION = 0x400D005D;

		[Description("A NetShow administrator at network location %1 aborted obsolete connection %2 from the system.")]
		public const int NS_I_KILL_CONNECTION = 0x400D005E;

		[Description("A NetShow administrator at network location %1 started rebuilding disk %2.")]
		public const int NS_I_REBUILD_DISK = 0x400D005F;

		[Description("Event initialization failed, there will be no MCM events.")]
		public const int MCMADM_I_NO_EVENTS = 0x400D0069;

		[Description("The logging operation failed.")]
		public const int NS_I_LOGGING_FAILED = 0x400D006E;

		[Description("A NetShow administrator at network location %1 set the maximum bandwidth limit to %2 bps.")]
		public const int NS_I_LIMIT_BANDWIDTH = 0x400D0070;

		[Description("Content Server %1 (%2) has established its link to Content Server %3.")]
		public const int NS_I_CUB_UNFAIL_LINK = 0x400D0191;

		[Description("Restripe operation has started.")]
		public const int NS_I_RESTRIPE_START = 0x400D0193;

		[Description("Restripe operation has completed.")]
		public const int NS_I_RESTRIPE_DONE = 0x400D0194;

		[Description("Content disk %1 (%2) on Content Server %3 has been restriped out.")]
		public const int NS_I_RESTRIPE_DISK_OUT = 0x400D0196;

		[Description("Content server %1 (%2) has been restriped out.")]
		public const int NS_I_RESTRIPE_CUB_OUT = 0x400D0197;

		[Description("Disk %1 ( %2 ) on Content Server %3, has been offlined.")]
		public const int NS_I_DISK_STOP = 0x400D0198;

		[Description("The playlist change occurred while receding.")]
		public const int NS_I_PLAYLIST_CHANGE_RECEDING = 0x400D14BE;

		[Description("The client is reconnected.")]
		public const int NS_I_RECONNECTED = 0x400D2EFF;

		[Description("Forcing a switch to a pending header on start.")]
		public const int NS_I_NOLOG_STOP = 0x400D2F01;

		[Description("There is already an existing packetizer plugin for the stream.")]
		public const int NS_I_EXISTING_PACKETIZER = 0x400D2F03;

		[Description("The proxy setting is manual.")]
		public const int NS_I_MANUAL_PROXY = 0x400D2F04;

		[Description("The kernel driver detected a version mismatch between it and the user mode driver.")]
		public const int ERROR_GRAPHICS_DRIVER_MISMATCH = 0x40262009;

		[Description("Child device presence was not reliably detected.")]
		public const int ERROR_GRAPHICS_UNKNOWN_CHILD_STATUS = 0x4026242F;

		[Description("Starting the lead-link adapter has been deferred temporarily.")]
		public const int ERROR_GRAPHICS_LEADLINK_START_DEFERRED = 0x40262437;

		[Description("The display adapter is being polled for children too frequently at the same polling level.")]
		public const int ERROR_GRAPHICS_POLLING_TOO_FREQUENTLY = 0x40262439;

		[Description("Starting the adapter has been deferred temporarily.")]
		public const int ERROR_GRAPHICS_START_DEFERRED = 0x4026243A;

		[Description("The data necessary to complete this operation is not yet available.")]
		public const int E_PENDING = unchecked((int)0x8000000A);

		[Description("The operation attempted to access data outside the valid range")]
		public const int E_BOUNDS = unchecked((int)0x8000000B);

		[Description("A concurrent or interleaved operation changed the state of the object, invalidating this operation.")]
		public const int E_CHANGED_STATE = unchecked((int)0x8000000C);

		[Description("An illegal state change was requested.")]
		public const int E_ILLEGAL_STATE_CHANGE = unchecked((int)0x8000000D);

		[Description("A method was called at an unexpected time.")]
		public const int E_ILLEGAL_METHOD_CALL = unchecked((int)0x8000000E);

		[Description("Typename or Namespace was not found in metadata file.")]
		public const int RO_E_METADATA_NAME_NOT_FOUND = unchecked((int)0x8000000F);

		[Description("Name is an existing namespace rather than a typename.")]
		public const int RO_E_METADATA_NAME_IS_NAMESPACE = unchecked((int)0x80000010);

		[Description("Typename has an invalid format.")]
		public const int RO_E_METADATA_INVALID_TYPE_FORMAT = unchecked((int)0x80000011);

		[Description("Metadata file is invalid or corrupted.")]
		public const int RO_E_INVALID_METADATA_FILE = unchecked((int)0x80000012);

		[Description("The object has been closed.")]
		public const int RO_E_CLOSED = unchecked((int)0x80000013);

		[Description("Only one thread may access the object during a write operation.")]
		public const int RO_E_EXCLUSIVE_WRITE = unchecked((int)0x80000014);

		[Description("Operation is prohibited during change notification.")]
		public const int RO_E_CHANGE_NOTIFICATION_IN_PROGRESS = unchecked((int)0x80000015);
		
		[Description("The text associated with this error code could not be found.")]
		public const int RO_E_ERROR_STRING_NOT_FOUND = unchecked((int)0x80000016);
		
		[Description("String not null terminated.")]
		public const int E_STRING_NOT_NULL_TERMINATED = unchecked((int)0x80000017);
		
		[Description("A delegate was assigned when not allowed.")]
		public const int E_ILLEGAL_DELEGATE_ASSIGNMENT = unchecked((int)0x80000018);
		
		[Description("An async operation was not properly started.")]
		public const int E_ASYNC_OPERATION_NOT_STARTED = unchecked((int)0x80000019);
		
		[Description("The application is exiting and cannot service this request.")]
		public const int E_APPLICATION_EXITING = unchecked((int)0x8000001A);
		
		[Description("The application view is exiting and cannot service this request.")]
		public const int E_APPLICATION_VIEW_EXITING = unchecked((int)0x8000001B);
		
		[Description("The object must support the IAgileObject interface.")]
		public const int RO_E_MUST_BE_AGILE = unchecked((int)0x8000001C);
		
		[Description("Activating a single-threaded class from MTA is not supported.")]
		public const int RO_E_UNSUPPORTED_FROM_MTA = unchecked((int)0x8000001D);
		
		[Description("The object has been committed.")]
		public const int RO_E_COMMITTED = unchecked((int)0x8000001E);

		[Description("Not implemented.")]
		public const int E_NOTIMPL = unchecked((int)0x80004001);

		[Description("No such interface supported.")]
		public const int E_NOINTERFACE = unchecked((int)0x80004002);

		[Description("Invalid pointer.")]
		public const int E_POINTER = unchecked((int)0x80004003);

		[Description("Operation aborted.")]
		public const int E_ABORT = unchecked((int)0x80004004);

		[Description("Unspecified error.")]
		public const int E_FAIL = unchecked((int)0x80004005);

		[Description("Thread local storage failure.")]
		public const int CO_E_INIT_TLS = unchecked((int)0x80004006);

		[Description("Get shared memory allocator failure.")]
		public const int CO_E_INIT_SHARED_ALLOCATOR = unchecked((int)0x80004007);

		[Description("Get memory allocator failure.")]
		public const int CO_E_INIT_MEMORY_ALLOCATOR = unchecked((int)0x80004008);

		[Description("Unable to initialize class cache.")]
		public const int CO_E_INIT_CLASS_CACHE = unchecked((int)0x80004009);

		[Description("Unable to initialize remote procedure call (RPC) services.")]
		public const int CO_E_INIT_RPC_CHANNEL = unchecked((int)0x8000400A);

		[Description("Cannot set thread local storage channel control.")]
		public const int CO_E_INIT_TLS_SET_CHANNEL_CONTROL = unchecked((int)0x8000400B);

		[Description("Could not allocate thread local storage channel control.")]
		public const int CO_E_INIT_TLS_CHANNEL_CONTROL = unchecked((int)0x8000400C);

		[Description("The user-supplied memory allocator is unacceptable.")]
		public const int CO_E_INIT_UNACCEPTED_USER_ALLOCATOR = unchecked((int)0x8000400D);

		[Description("The OLE service mutex already exists.")]
		public const int CO_E_INIT_SCM_MUTEX_EXISTS = unchecked((int)0x8000400E);

		[Description("The OLE service file mapping already exists.")]
		public const int CO_E_INIT_SCM_FILE_MAPPING_EXISTS = unchecked((int)0x8000400F);

		[Description("Unable to map view of file for OLE service.")]
		public const int CO_E_INIT_SCM_MAP_VIEW_OF_FILE = unchecked((int)0x80004010);

		[Description("Failure attempting to launch OLE service.")]
		public const int CO_E_INIT_SCM_EXEC_FAILURE = unchecked((int)0x80004011);

		[Description("There was an attempt to call CoInitialize a second time while single-threaded.")]
		public const int CO_E_INIT_ONLY_SINGLE_THREADED = unchecked((int)0x80004012);

		[Description("A Remote activation was necessary but was not allowed.")]
		public const int CO_E_CANT_REMOTE = unchecked((int)0x80004013);

		[Description("A Remote activation was necessary, but the server name provided was invalid.")]
		public const int CO_E_BAD_SERVER_NAME = unchecked((int)0x80004014);

		[Description("The class is configured to run as a security ID different from the caller.")]
		public const int CO_E_WRONG_SERVER_IDENTITY = unchecked((int)0x80004015);

		[Description("Use of OLE1 services requiring Dynamic Data Exchange (DDE) Windows is disabled.")]
		public const int CO_E_OLE1DDE_DISABLED = unchecked((int)0x80004016);

		[Description("A RunAs specification must be <domain name><user name>; or simply <user name>.")]
		public const int CO_E_RUNAS_SYNTAX = unchecked((int)0x80004017);

		[Description("The server process could not be started. The path name might be incorrect.")]
		public const int CO_E_CREATEPROCESS_FAILURE = unchecked((int)0x80004018);

		[Description("The server process could not be started as the configured identity. The path name might be incorrect or unavailable.")]
		public const int CO_E_RUNAS_CREATEPROCESS_FAILURE = unchecked((int)0x80004019);

		[Description("The server process could not be started because the configured identity is incorrect. Check the user name and password.")]
		public const int CO_E_RUNAS_LOGON_FAILURE = unchecked((int)0x8000401A);

		[Description("The client is not allowed to launch this server.")]
		public const int CO_E_LAUNCH_PERMSSION_DENIED = unchecked((int)0x8000401B);

		[Description("The service providing this server could not be started.")]
		public const int CO_E_START_SERVICE_FAILURE = unchecked((int)0x8000401C);

		[Description("This computer was unable to communicate with the computer providing the server.")]
		public const int CO_E_REMOTE_COMMUNICATION_FAILURE = unchecked((int)0x8000401D);

		[Description("The server did not respond after being launched.")]
		public const int CO_E_SERVER_START_TIMEOUT = unchecked((int)0x8000401E);

		[Description("The registration information for this server is inconsistent or incomplete.")]
		public const int CO_E_CLSREG_INCONSISTENT = unchecked((int)0x8000401F);

		[Description("The registration information for this interface is inconsistent or incomplete.")]
		public const int CO_E_IIDREG_INCONSISTENT = unchecked((int)0x80004020);

		[Description("The operation attempted is not supported.")]
		public const int CO_E_NOT_SUPPORTED = unchecked((int)0x80004021);

		[Description("A DLL must be loaded.")]
		public const int CO_E_RELOAD_DLL = unchecked((int)0x80004022);

		[Description("A Microsoft Software Installer error was encountered.")]
		public const int CO_E_MSI_ERROR = unchecked((int)0x80004023);

		[Description("The specified activation could not occur in the client context as specified.")]
		public const int CO_E_ATTEMPT_TO_CREATE_OUTSIDE_CLIENT_CONTEXT = unchecked((int)0x80004024);

		[Description("Activations on the server are paused.")]
		public const int CO_E_SERVER_PAUSED = unchecked((int)0x80004025);

		[Description("Activations on the server are not paused.")]
		public const int CO_E_SERVER_NOT_PAUSED = unchecked((int)0x80004026);

		[Description("The component or application containing the component has been disabled.")]
		public const int CO_E_CLASS_DISABLED = unchecked((int)0x80004027);

		[Description("The common language runtime is not available.")]
		public const int CO_E_CLRNOTAVAILABLE = unchecked((int)0x80004028);

		[Description("The thread-pool rejected the submitted asynchronous work.")]
		public const int CO_E_ASYNC_WORK_REJECTED = unchecked((int)0x80004029);

		[Description("The server started, but it did not finish initializing in a timely fashion.")]
		public const int CO_E_SERVER_INIT_TIMEOUT = unchecked((int)0x8000402A);

		[Description("Unable to complete the call because there is no COM+ security context inside IObjectControl.Activate.")]
		public const int CO_E_NO_SECCTX_IN_ACTIVATE = unchecked((int)0x8000402B);

		[Description("The provided tracker configuration is invalid.")]
		public const int CO_E_TRACKER_CONFIG = unchecked((int)0x80004030);

		[Description("The provided thread pool configuration is invalid.")]
		public const int CO_E_THREADPOOL_CONFIG = unchecked((int)0x80004031);

		[Description("The provided side-by-side configuration is invalid.")]
		public const int CO_E_SXS_CONFIG = unchecked((int)0x80004032);

		[Description("The server principal name (SPN) obtained during security negotiation is malformed.")]
		public const int CO_E_MALFORMED_SPN = unchecked((int)0x80004033);

		[Description("Catastrophic failure.")]
		public const int E_UNEXPECTED = unchecked((int)0x8000FFFF);

		[Description("Call was rejected by callee.")]
		public const int RPC_E_CALL_REJECTED = unchecked((int)0x80010001);

		[Description("Call was canceled by the message filter.")]
		public const int RPC_E_CALL_CANCELED = unchecked((int)0x80010002);

		[Description("The caller is dispatching an intertask SendMessage call and cannot call out via PostMessage.")]
		public const int RPC_E_CANTPOST_INSENDCALL = unchecked((int)0x80010003);

		[Description("The caller is dispatching an asynchronous call and cannot make an outgoing call on behalf of this call.")]
		public const int RPC_E_CANTCALLOUT_INASYNCCALL = unchecked((int)0x80010004);

		[Description("It is illegal to call out while inside message filter.")]
		public const int RPC_E_CANTCALLOUT_INEXTERNALCALL = unchecked((int)0x80010005);

		[Description("The connection terminated or is in a bogus state and can no longer be used. Other connections are still valid.")]
		public const int RPC_E_CONNECTION_TERMINATED = unchecked((int)0x80010006);

		[Description("The callee (the server, not the server application) is not available and disappeared; all connections are invalid. The call might have executed.")]
		public const int RPC_E_SERVER_DIED = unchecked((int)0x80010007);

		[Description("The caller (client) disappeared while the callee (server) was processing a call.")]
		public const int RPC_E_CLIENT_DIED = unchecked((int)0x80010008);

		[Description("The data packet with the marshaled parameter data is incorrect.")]
		public const int RPC_E_INVALID_DATAPACKET = unchecked((int)0x80010009);

		[Description("The call was not transmitted properly; the message queue was full and was not emptied after yielding.")]
		public const int RPC_E_CANTTRANSMIT_CALL = unchecked((int)0x8001000A);

		[Description("The client RPC caller cannot marshal the parameter data due to errors (such as low memory).")]
		public const int RPC_E_CLIENT_CANTMARSHAL_DATA = unchecked((int)0x8001000B);

		[Description("The client RPC caller cannot unmarshal the return data due to errors (such as low memory).")]
		public const int RPC_E_CLIENT_CANTUNMARSHAL_DATA = unchecked((int)0x8001000C);

		[Description("The server RPC callee cannot marshal the return data due to errors (such as low memory).")]
		public const int RPC_E_SERVER_CANTMARSHAL_DATA = unchecked((int)0x8001000D);

		[Description("The server RPC callee cannot unmarshal the parameter data due to errors (such as low memory).")]
		public const int RPC_E_SERVER_CANTUNMARSHAL_DATA = unchecked((int)0x8001000E);

		[Description("Received data is invalid. The data might be server or client data.")]
		public const int RPC_E_INVALID_DATA = unchecked((int)0x8001000F);

		[Description("A particular parameter is invalid and cannot be (un)marshaled.")]
		public const int RPC_E_INVALID_PARAMETER = unchecked((int)0x80010010);

		[Description("There is no second outgoing call on same channel in DDE conversation.")]
		public const int RPC_E_CANTCALLOUT_AGAIN = unchecked((int)0x80010011);

		[Description("The callee (the server, not the server application) is not available and disappeared; all connections are invalid. The call did not execute.")]
		public const int RPC_E_SERVER_DIED_DNE = unchecked((int)0x80010012);

		[Description("System call failed.")]
		public const int RPC_E_SYS_CALL_FAILED = unchecked((int)0x80010100);

		[Description("Could not allocate some required resource (such as memory or events)")]
		public const int RPC_E_OUT_OF_RESOURCES = unchecked((int)0x80010101);

		[Description("Attempted to make calls on more than one thread in single-threaded mode.")]
		public const int RPC_E_ATTEMPTED_MULTITHREAD = unchecked((int)0x80010102);

		[Description("The requested interface is not registered on the server object.")]
		public const int RPC_E_NOT_REGISTERED = unchecked((int)0x80010103);

		[Description("RPC could not call the server or could not return the results of calling the server.")]
		public const int RPC_E_FAULT = unchecked((int)0x80010104);

		[Description("The server threw an exception.")]
		public const int RPC_E_SERVERFAULT = unchecked((int)0x80010105);

		[Description("Cannot change thread mode after it is set.")]
		public const int RPC_E_CHANGED_MODE = unchecked((int)0x80010106);

		[Description("The method called does not exist on the server.")]
		public const int RPC_E_INVALIDMETHOD = unchecked((int)0x80010107);

		[Description("The object invoked has disconnected from its clients.")]
		public const int RPC_E_DISCONNECTED = unchecked((int)0x80010108);

		[Description("The object invoked chose not to process the call now. Try again later.")]
		public const int RPC_E_RETRY = unchecked((int)0x80010109);

		[Description("The message filter indicated that the application is busy.")]
		public const int RPC_E_SERVERCALL_RETRYLATER = unchecked((int)0x8001010A);

		[Description("The message filter rejected the call.")]
		public const int RPC_E_SERVERCALL_REJECTED = unchecked((int)0x8001010B);

		[Description("A call control interface was called with invalid data.")]
		public const int RPC_E_INVALID_CALLDATA = unchecked((int)0x8001010C);

		[Description("An outgoing call cannot be made because the application is dispatching an input-synchronous call.")]
		public const int RPC_E_CANTCALLOUT_ININPUTSYNCCALL = unchecked((int)0x8001010D);

		[Description("The application called an interface that was marshaled for a different thread.")]
		public const int RPC_E_WRONG_THREAD = unchecked((int)0x8001010E);

		[Description("CoInitialize has not been called on the current thread.")]
		public const int RPC_E_THREAD_NOT_INIT = unchecked((int)0x8001010F);

		[Description("The version of OLE on the client and server machines does not match.")]
		public const int RPC_E_VERSION_MISMATCH = unchecked((int)0x80010110);

		[Description("OLE received a packet with an invalid header.")]
		public const int RPC_E_INVALID_HEADER = unchecked((int)0x80010111);

		[Description("OLE received a packet with an invalid extension.")]
		public const int RPC_E_INVALID_EXTENSION = unchecked((int)0x80010112);

		[Description("The requested object or interface does not exist.")]
		public const int RPC_E_INVALID_IPID = unchecked((int)0x80010113);

		[Description("The requested object does not exist.")]
		public const int RPC_E_INVALID_OBJECT = unchecked((int)0x80010114);

		[Description("OLE has sent a request and is waiting for a reply.")]
		public const int RPC_S_CALLPENDING = unchecked((int)0x80010115);

		[Description("OLE is waiting before retrying a request.")]
		public const int RPC_S_WAITONTIMER = unchecked((int)0x80010116);

		[Description("Call context cannot be accessed after call completed.")]
		public const int RPC_E_CALL_COMPLETE = unchecked((int)0x80010117);

		[Description("Impersonate on unsecure calls is not supported.")]
		public const int RPC_E_UNSECURE_CALL = unchecked((int)0x80010118);

		[Description("Security must be initialized before any interfaces are marshaled or unmarshaled. It cannot be changed after initialized.")]
		public const int RPC_E_TOO_LATE = unchecked((int)0x80010119);

		[Description("No security packages are installed on this machine, the user is not logged on, or there are no compatible security packages between the client and server.")]
		public const int RPC_E_NO_GOOD_SECURITY_PACKAGES = unchecked((int)0x8001011A);

		[Description("Access is denied.")]
		public const int RPC_E_ACCESS_DENIED = unchecked((int)0x8001011B);

		[Description("Remote calls are not allowed for this process.")]
		public const int RPC_E_REMOTE_DISABLED = unchecked((int)0x8001011C);

		[Description("The marshaled interface data packet (OBJREF) has an invalid or unknown format.")]
		public const int RPC_E_INVALID_OBJREF = unchecked((int)0x8001011D);

		[Description("No context is associated with this call. This happens for some custom marshaled calls and on the client side of the call.")]
		public const int RPC_E_NO_CONTEXT = unchecked((int)0x8001011E);

		[Description("This operation returned because the time-out period expired.")]
		public const int RPC_E_TIMEOUT = unchecked((int)0x8001011F);

		[Description("There are no synchronize objects to wait on.")]
		public const int RPC_E_NO_SYNC = unchecked((int)0x80010120);

		[Description("Full subject issuer chain Secure Sockets Layer (SSL) principal name expected from the server.")]
		public const int RPC_E_FULLSIC_REQUIRED = unchecked((int)0x80010121);

		[Description("Principal name is not a valid Microsoft standard (msstd) name.")]
		public const int RPC_E_INVALID_STD_NAME = unchecked((int)0x80010122);

		[Description("Unable to impersonate DCOM client.")]
		public const int CO_E_FAILEDTOIMPERSONATE = unchecked((int)0x80010123);

		[Description("Unable to obtain server's security context.")]
		public const int CO_E_FAILEDTOGETSECCTX = unchecked((int)0x80010124);

		[Description("Unable to open the access token of the current thread.")]
		public const int CO_E_FAILEDTOOPENTHREADTOKEN = unchecked((int)0x80010125);

		[Description("Unable to obtain user information from an access token.")]
		public const int CO_E_FAILEDTOGETTOKENINFO = unchecked((int)0x80010126);

		[Description("The client who called IAccessControl::IsAccessPermitted was not the trustee provided to the method.")]
		public const int CO_E_TRUSTEEDOESNTMATCHCLIENT = unchecked((int)0x80010127);

		[Description("Unable to obtain the client's security blanket.")]
		public const int CO_E_FAILEDTOQUERYCLIENTBLANKET = unchecked((int)0x80010128);

		[Description("Unable to set a discretionary access control list (ACL) into a security descriptor.")]
		public const int CO_E_FAILEDTOSETDACL = unchecked((int)0x80010129);

		[Description("The system function AccessCheck returned false.")]
		public const int CO_E_ACCESSCHECKFAILED = unchecked((int)0x8001012A);

		[Description("Either NetAccessDel or NetAccessAdd returned an error code.")]
		public const int CO_E_NETACCESSAPIFAILED = unchecked((int)0x8001012B);

		[Description("One of the trustee strings provided by the user did not conform to the <Domain>\\<Name> syntax and it was not the *\" string\".")]
		public const int CO_E_WRONGTRUSTEENAMESYNTAX = unchecked((int)0x8001012C);

		[Description("One of the security identifiers provided by the user was invalid.")]
		public const int CO_E_INVALIDSID = unchecked((int)0x8001012D);

		[Description("Unable to convert a wide character trustee string to a multiple-byte trustee string.")]
		public const int CO_E_CONVERSIONFAILED = unchecked((int)0x8001012E);

		[Description("Unable to find a security identifier that corresponds to a trustee string provided by the user.")]
		public const int CO_E_NOMATCHINGSIDFOUND = unchecked((int)0x8001012F);

		[Description("The system function LookupAccountSID failed.")]
		public const int CO_E_LOOKUPACCSIDFAILED = unchecked((int)0x80010130);

		[Description("Unable to find a trustee name that corresponds to a security identifier provided by the user.")]
		public const int CO_E_NOMATCHINGNAMEFOUND = unchecked((int)0x80010131);

		[Description("The system function LookupAccountName failed.")]
		public const int CO_E_LOOKUPACCNAMEFAILED = unchecked((int)0x80010132);

		[Description("Unable to set or reset a serialization handle.")]
		public const int CO_E_SETSERLHNDLFAILED = unchecked((int)0x80010133);

		[Description("Unable to obtain the Windows directory.")]
		public const int CO_E_FAILEDTOGETWINDIR = unchecked((int)0x80010134);

		[Description("Path too long.")]
		public const int CO_E_PATHTOOLONG = unchecked((int)0x80010135);

		[Description("Unable to generate a UUID.")]
		public const int CO_E_FAILEDTOGENUUID = unchecked((int)0x80010136);

		[Description("Unable to create file.")]
		public const int CO_E_FAILEDTOCREATEFILE = unchecked((int)0x80010137);

		[Description("Unable to close a serialization handle or a file handle.")]
		public const int CO_E_FAILEDTOCLOSEHANDLE = unchecked((int)0x80010138);

		[Description("The number of access control entries (ACEs) in an ACL exceeds the system limit.")]
		public const int CO_E_EXCEEDSYSACLLIMIT = unchecked((int)0x80010139);

		[Description("Not all the DENY_ACCESS ACEs are arranged in front of the GRANT_ACCESS ACEs in the stream.")]
		public const int CO_E_ACESINWRONGORDER = unchecked((int)0x8001013A);

		[Description("The version of ACL format in the stream is not supported by this implementation of IAccessControl.")]
		public const int CO_E_INCOMPATIBLESTREAMVERSION = unchecked((int)0x8001013B);

		[Description("Unable to open the access token of the server process.")]
		public const int CO_E_FAILEDTOOPENPROCESSTOKEN = unchecked((int)0x8001013C);

		[Description("Unable to decode the ACL in the stream provided by the user.")]
		public const int CO_E_DECODEFAILED = unchecked((int)0x8001013D);

		[Description("The COM IAccessControl object is not initialized.")]
		public const int CO_E_ACNOTINITIALIZED = unchecked((int)0x8001013F);

		[Description("Call Cancellation is disabled.")]
		public const int CO_E_CANCEL_DISABLED = unchecked((int)0x80010140);

		[Description("An internal error occurred.")]
		public const int RPC_E_UNEXPECTED = unchecked((int)0x8001FFFF);

		[Description("Unknown interface.")]
		public const int DISP_E_UNKNOWNINTERFACE = unchecked((int)0x80020001);

		[Description("Member not found.")]
		public const int DISP_E_MEMBERNOTFOUND = unchecked((int)0x80020003);

		[Description("Parameter not found.")]
		public const int DISP_E_PARAMNOTFOUND = unchecked((int)0x80020004);

		[Description("Type mismatch.")]
		public const int DISP_E_TYPEMISMATCH = unchecked((int)0x80020005);

		[Description("Unknown name.")]
		public const int DISP_E_UNKNOWNNAME = unchecked((int)0x80020006);

		[Description("No named arguments.")]
		public const int DISP_E_NONAMEDARGS = unchecked((int)0x80020007);

		[Description("Bad variable type.")]
		public const int DISP_E_BADVARTYPE = unchecked((int)0x80020008);

		[Description("Exception occurred.")]
		public const int DISP_E_EXCEPTION = unchecked((int)0x80020009);

		[Description("Out of present range.")]
		public const int DISP_E_OVERFLOW = unchecked((int)0x8002000A);

		[Description("Invalid index.")]
		public const int DISP_E_BADINDEX = unchecked((int)0x8002000B);

		[Description("Unknown language.")]
		public const int DISP_E_UNKNOWNLCID = unchecked((int)0x8002000C);

		[Description("Memory is locked.")]
		public const int DISP_E_ARRAYISLOCKED = unchecked((int)0x8002000D);

		[Description("Invalid number of parameters.")]
		public const int DISP_E_BADPARAMCOUNT = unchecked((int)0x8002000E);

		[Description("Parameter not optional.")]
		public const int DISP_E_PARAMNOTOPTIONAL = unchecked((int)0x8002000F);

		[Description("Invalid callee.")]
		public const int DISP_E_BADCALLEE = unchecked((int)0x80020010);

		[Description("Does not support a collection.")]
		public const int DISP_E_NOTACOLLECTION = unchecked((int)0x80020011);

		[Description("Division by zero.")]
		public const int DISP_E_DIVBYZERO = unchecked((int)0x80020012);

		[Description("Buffer too small.")]
		public const int DISP_E_BUFFERTOOSMALL = unchecked((int)0x80020013);

		[Description("Buffer too small.")]
		public const int TYPE_E_BUFFERTOOSMALL = unchecked((int)0x80028016);

		[Description("Field name not defined in the record.")]
		public const int TYPE_E_FIELDNOTFOUND = unchecked((int)0x80028017);

		[Description("Old format or invalid type library.")]
		public const int TYPE_E_INVDATAREAD = unchecked((int)0x80028018);

		[Description("Old format or invalid type library.")]
		public const int TYPE_E_UNSUPFORMAT = unchecked((int)0x80028019);

		[Description("Error accessing the OLE registry.")]
		public const int TYPE_E_REGISTRYACCESS = unchecked((int)0x8002801C);

		[Description("Library not registered.")]
		public const int TYPE_E_LIBNOTREGISTERED = unchecked((int)0x8002801D);

		[Description("Bound to unknown type.")]
		public const int TYPE_E_UNDEFINEDTYPE = unchecked((int)0x80028027);

		[Description("Qualified name disallowed.")]
		public const int TYPE_E_QUALIFIEDNAMEDISALLOWED = unchecked((int)0x80028028);

		[Description("Invalid forward reference, or reference to uncompiled type.")]
		public const int TYPE_E_INVALIDSTATE = unchecked((int)0x80028029);

		[Description("Type mismatch.")]
		public const int TYPE_E_WRONGTYPEKIND = unchecked((int)0x8002802A);

		[Description("Element not found.")]
		public const int TYPE_E_ELEMENTNOTFOUND = unchecked((int)0x8002802B);

		[Description("Ambiguous name.")]
		public const int TYPE_E_AMBIGUOUSNAME = unchecked((int)0x8002802C);

		[Description("Name already exists in the library.")]
		public const int TYPE_E_NAMECONFLICT = unchecked((int)0x8002802D);

		[Description("Unknown language code identifier (LCID).")]
		public const int TYPE_E_UNKNOWNLCID = unchecked((int)0x8002802E);

		[Description("Function not defined in specified DLL.")]
		public const int TYPE_E_DLLFUNCTIONNOTFOUND = unchecked((int)0x8002802F);

		[Description("Wrong module kind for the operation.")]
		public const int TYPE_E_BADMODULEKIND = unchecked((int)0x800288BD);

		[Description("Size cannot exceed 64 KB.")]
		public const int TYPE_E_SIZETOOBIG = unchecked((int)0x800288C5);

		[Description("Duplicate ID in inheritance hierarchy.")]
		public const int TYPE_E_DUPLICATEID = unchecked((int)0x800288C6);

		[Description("Incorrect inheritance depth in standard OLE hmember.")]
		public const int TYPE_E_INVALIDID = unchecked((int)0x800288CF);

		[Description("Type mismatch.")]
		public const int TYPE_E_TYPEMISMATCH = unchecked((int)0x80028CA0);

		[Description("Invalid number of arguments.")]
		public const int TYPE_E_OUTOFBOUNDS = unchecked((int)0x80028CA1);

		[Description("I/O error.")]
		public const int TYPE_E_IOERROR = unchecked((int)0x80028CA2);

		[Description("Error creating unique .tmp file.")]
		public const int TYPE_E_CANTCREATETMPFILE = unchecked((int)0x80028CA3);

		[Description("Error loading type library or DLL.")]
		public const int TYPE_E_CANTLOADLIBRARY = unchecked((int)0x80029C4A);

		[Description("Inconsistent property functions.")]
		public const int TYPE_E_INCONSISTENTPROPFUNCS = unchecked((int)0x80029C83);

		[Description("Circular dependency between types and modules.")]
		public const int TYPE_E_CIRCULARTYPE = unchecked((int)0x80029C84);

		[Description("Unable to perform requested operation.")]
		public const int STG_E_INVALIDFUNCTION = unchecked((int)0x80030001);

		[Description("%1 could not be found.")]
		public const int STG_E_FILENOTFOUND = unchecked((int)0x80030002);

		[Description("The path %1 could not be found.")]
		public const int STG_E_PATHNOTFOUND = unchecked((int)0x80030003);

		[Description("There are insufficient resources to open another file.")]
		public const int STG_E_TOOMANYOPENFILES = unchecked((int)0x80030004);

		[Description("Access denied.")]
		public const int STG_E_ACCESSDENIED = unchecked((int)0x80030005);

		[Description("Attempted an operation on an invalid object.")]
		public const int STG_E_INVALIDHANDLE = unchecked((int)0x80030006);

		[Description("There is insufficient memory available to complete operation.")]
		public const int STG_E_INSUFFICIENTMEMORY = unchecked((int)0x80030008);

		[Description("Invalid pointer error.")]
		public const int STG_E_INVALIDPOINTER = unchecked((int)0x80030009);

		[Description("There are no more entries to return.")]
		public const int STG_E_NOMOREFILES = unchecked((int)0x80030012);

		[Description("Disk is write-protected.")]
		public const int STG_E_DISKISWRITEPROTECTED = unchecked((int)0x80030013);

		[Description("An error occurred during a seek operation.")]
		public const int STG_E_SEEKERROR = unchecked((int)0x80030019);

		[Description("A disk error occurred during a write operation.")]
		public const int STG_E_WRITEFAULT = unchecked((int)0x8003001D);

		[Description("A disk error occurred during a read operation.")]
		public const int STG_E_READFAULT = unchecked((int)0x8003001E);

		[Description("A share violation has occurred.")]
		public const int STG_E_SHAREVIOLATION = unchecked((int)0x80030020);

		[Description("A lock violation has occurred.")]
		public const int STG_E_LOCKVIOLATION = unchecked((int)0x80030021);

		[Description("%1 already exists.")]
		public const int STG_E_FILEALREADYEXISTS = unchecked((int)0x80030050);

		[Description("Invalid parameter error.")]
		public const int STG_E_INVALIDPARAMETER = unchecked((int)0x80030057);

		[Description("There is insufficient disk space to complete operation.")]
		public const int STG_E_MEDIUMFULL = unchecked((int)0x80030070);

		[Description("Illegal write of non-simple property to simple property set.")]
		public const int STG_E_PROPSETMISMATCHED = unchecked((int)0x800300F0);

		[Description("An application programming interface (API) call exited abnormally.")]
		public const int STG_E_ABNORMALAPIEXIT = unchecked((int)0x800300FA);

		[Description("The file %1 is not a valid compound file.")]
		public const int STG_E_INVALIDHEADER = unchecked((int)0x800300FB);

		[Description("The name %1 is not valid.")]
		public const int STG_E_INVALIDNAME = unchecked((int)0x800300FC);

		[Description("An unexpected error occurred.")]
		public const int STG_E_UNKNOWN = unchecked((int)0x800300FD);

		[Description("That function is not implemented.")]
		public const int STG_E_UNIMPLEMENTEDFUNCTION = unchecked((int)0x800300FE);

		[Description("Invalid flag error.")]
		public const int STG_E_INVALIDFLAG = unchecked((int)0x800300FF);

		[Description("Attempted to use an object that is busy.")]
		public const int STG_E_INUSE = unchecked((int)0x80030100);

		[Description("The storage has been changed since the last commit.")]
		public const int STG_E_NOTCURRENT = unchecked((int)0x80030101);

		[Description("Attempted to use an object that has ceased to exist.")]
		public const int STG_E_REVERTED = unchecked((int)0x80030102);

		[Description("Cannot save.")]
		public const int STG_E_CANTSAVE = unchecked((int)0x80030103);

		[Description("The compound file %1 was produced with an incompatible version of storage.")]
		public const int STG_E_OLDFORMAT = unchecked((int)0x80030104);

		[Description("The compound file %1 was produced with a newer version of storage.")]
		public const int STG_E_OLDDLL = unchecked((int)0x80030105);

		[Description("Share.exe or equivalent is required for operation.")]
		public const int STG_E_SHAREREQUIRED = unchecked((int)0x80030106);

		[Description("Illegal operation called on non-file based storage.")]
		public const int STG_E_NOTFILEBASEDSTORAGE = unchecked((int)0x80030107);

		[Description("Illegal operation called on object with extant marshalings.")]
		public const int STG_E_EXTANTMARSHALLINGS = unchecked((int)0x80030108);

		[Description("The docfile has been corrupted.")]
		public const int STG_E_DOCFILECORRUPT = unchecked((int)0x80030109);

		[Description("OLE32.DLL has been loaded at the wrong address.")]
		public const int STG_E_BADBASEADDRESS = unchecked((int)0x80030110);

		[Description("The compound file is too large for the current implementation.")]
		public const int STG_E_DOCFILETOOLARGE = unchecked((int)0x80030111);

		[Description("The compound file was not created with the STGM_SIMPLE flag.")]
		public const int STG_E_NOTSIMPLEFORMAT = unchecked((int)0x80030112);

		[Description("The file download was aborted abnormally. The file is incomplete.")]
		public const int STG_E_INCOMPLETE = unchecked((int)0x80030201);

		[Description("The file download has been terminated.")]
		public const int STG_E_TERMINATED = unchecked((int)0x80030202);

		[Description("Generic Copy Protection Error.")]
		public const int STG_E_STATUS_COPY_PROTECTION_FAILURE = unchecked((int)0x80030305);

		[Description("Copy Protection Error—DVD CSS Authentication failed.")]
		public const int STG_E_CSS_AUTHENTICATION_FAILURE = unchecked((int)0x80030306);

		[Description("Copy Protection Error—The given sector does not have a valid CSS key.")]
		public const int STG_E_CSS_KEY_NOT_PRESENT = unchecked((int)0x80030307);

		[Description("Copy Protection Error—DVD session key not established.")]
		public const int STG_E_CSS_KEY_NOT_ESTABLISHED = unchecked((int)0x80030308);

		[Description("Copy Protection Error—The read failed because the sector is encrypted.")]
		public const int STG_E_CSS_SCRAMBLED_SECTOR = unchecked((int)0x80030309);

		[Description("Copy Protection Error—The current DVD's region does not correspond to the region setting of the drive.")]
		public const int STG_E_CSS_REGION_MISMATCH = unchecked((int)0x8003030A);

		[Description("Copy Protection Error—The drive's region setting might be permanent or the number of user resets has been exhausted.")]
		public const int STG_E_RESETS_EXHAUSTED = unchecked((int)0x8003030B);

		[Description("Invalid OLEVERB structure.")]
		public const int OLE_E_OLEVERB = unchecked((int)0x80040000);

		[Description("Invalid advise flags.")]
		public const int OLE_E_ADVF = unchecked((int)0x80040001);

		[Description("Cannot enumerate any more because the associated data is missing.")]
		public const int OLE_E_ENUM_NOMORE = unchecked((int)0x80040002);

		[Description("This implementation does not take advises.")]
		public const int OLE_E_ADVISENOTSUPPORTED = unchecked((int)0x80040003);

		[Description("There is no connection for this connection ID.")]
		public const int OLE_E_NOCONNECTION = unchecked((int)0x80040004);

		[Description("Need to run the object to perform this operation.")]
		public const int OLE_E_NOTRUNNING = unchecked((int)0x80040005);

		[Description("There is no cache to operate on.")]
		public const int OLE_E_NOCACHE = unchecked((int)0x80040006);

		[Description("Uninitialized object.")]
		public const int OLE_E_BLANK = unchecked((int)0x80040007);

		[Description("Linked object's source class has changed.")]
		public const int OLE_E_CLASSDIFF = unchecked((int)0x80040008);

		[Description("Not able to get the moniker of the object.")]
		public const int OLE_E_CANT_GETMONIKER = unchecked((int)0x80040009);

		[Description("Not able to bind to the source.")]
		public const int OLE_E_CANT_BINDTOSOURCE = unchecked((int)0x8004000A);

		[Description("Object is static; operation not allowed.")]
		public const int OLE_E_STATIC = unchecked((int)0x8004000B);

		[Description("User canceled out of the Save dialog box.")]
		public const int OLE_E_PROMPTSAVECANCELLED = unchecked((int)0x8004000C);

		[Description("Invalid rectangle.")]
		public const int OLE_E_INVALIDRECT = unchecked((int)0x8004000D);

		[Description("compobj.dll is too old for the ole2.dll initialized.")]
		public const int OLE_E_WRONGCOMPOBJ = unchecked((int)0x8004000E);

		[Description("Invalid window handle.")]
		public const int OLE_E_INVALIDHWND = unchecked((int)0x8004000F);

		[Description("Object is not in any of the inplace active states.")]
		public const int OLE_E_NOT_INPLACEACTIVE = unchecked((int)0x80040010);

		[Description("Not able to convert object.")]
		public const int OLE_E_CANTCONVERT = unchecked((int)0x80040011);

		[Description("Not able to perform the operation because object is not given storage yet.")]
		public const int OLE_E_NOSTORAGE = unchecked((int)0x80040012);

		[Description("Invalid FORMATETC structure.")]
		public const int DV_E_FORMATETC = unchecked((int)0x80040064);

		[Description("Invalid DVTARGETDEVICE structure.")]
		public const int DV_E_DVTARGETDEVICE = unchecked((int)0x80040065);

		[Description("Invalid STDGMEDIUM structure.")]
		public const int DV_E_STGMEDIUM = unchecked((int)0x80040066);

		[Description("Invalid STATDATA structure.")]
		public const int DV_E_STATDATA = unchecked((int)0x80040067);

		[Description("Invalid lindex.")]
		public const int DV_E_LINDEX = unchecked((int)0x80040068);

		[Description("Invalid TYMED structure.")]
		public const int DV_E_TYMED = unchecked((int)0x80040069);

		[Description("Invalid clipboard format.")]
		public const int DV_E_CLIPFORMAT = unchecked((int)0x8004006A);

		[Description("Invalid aspects.")]
		public const int DV_E_DVASPECT = unchecked((int)0x8004006B);

		[Description("The tdSize parameter of the DVTARGETDEVICE structure is invalid.")]
		public const int DV_E_DVTARGETDEVICE_SIZE = unchecked((int)0x8004006C);

		[Description("Object does not support IViewObject interface.")]
		public const int DV_E_NOIVIEWOBJECT = unchecked((int)0x8004006D);

		[Description("Trying to revoke a drop target that has not been registered.")]
		public const int DRAGDROP_E_NOTREGISTERED = unchecked((int)0x80040100);

		[Description("This window has already been registered as a drop target.")]
		public const int DRAGDROP_E_ALREADYREGISTERED = unchecked((int)0x80040101);

		[Description("Invalid window handle.")]
		public const int DRAGDROP_E_INVALIDHWND = unchecked((int)0x80040102);

		[Description("Class does not support aggregation (or class object is remote).")]
		public const int CLASS_E_NOAGGREGATION = unchecked((int)0x80040110);

		[Description("ClassFactory cannot supply requested class.")]
		public const int CLASS_E_CLASSNOTAVAILABLE = unchecked((int)0x80040111);

		[Description("Class is not licensed for use.")]
		public const int CLASS_E_NOTLICENSED = unchecked((int)0x80040112);

		[Description("Error drawing view.")]
		public const int VIEW_E_DRAW = unchecked((int)0x80040140);

		[Description("Could not read key from registry.")]
		public const int REGDB_E_READREGDB = unchecked((int)0x80040150);

		[Description("Could not write key to registry.")]
		public const int REGDB_E_WRITEREGDB = unchecked((int)0x80040151);

		[Description("Could not find the key in the registry.")]
		public const int REGDB_E_KEYMISSING = unchecked((int)0x80040152);

		[Description("Invalid value for registry.")]
		public const int REGDB_E_INVALIDVALUE = unchecked((int)0x80040153);

		[Description("Class not registered.")]
		public const int REGDB_E_CLASSNOTREG = unchecked((int)0x80040154);

		[Description("Interface not registered.")]
		public const int REGDB_E_IIDNOTREG = unchecked((int)0x80040155);

		[Description("Threading model entry is not valid.")]
		public const int REGDB_E_BADTHREADINGMODEL = unchecked((int)0x80040156);

		[Description("CATID does not exist.")]
		public const int CAT_E_CATIDNOEXIST = unchecked((int)0x80040160);

		[Description("Description not found.")]
		public const int CAT_E_NODESCRIPTION = unchecked((int)0x80040161);

		[Description("No package in the software installation data in Active Directory meets this criteria.")]
		public const int CS_E_PACKAGE_NOTFOUND = unchecked((int)0x80040164);

		[Description("Deleting this will break the referential integrity of the software installation data in Active Directory.")]
		public const int CS_E_NOT_DELETABLE = unchecked((int)0x80040165);

		[Description("The CLSID was not found in the software installation data in Active Directory.")]
		public const int CS_E_CLASS_NOTFOUND = unchecked((int)0x80040166);

		[Description("The software installation data in Active Directory is corrupt.")]
		public const int CS_E_INVALID_VERSION = unchecked((int)0x80040167);

		[Description("There is no software installation data in Active Directory.")]
		public const int CS_E_NO_CLASSSTORE = unchecked((int)0x80040168);

		[Description("There is no software installation data object in Active Directory.")]
		public const int CS_E_OBJECT_NOTFOUND = unchecked((int)0x80040169);

		[Description("The software installation data object in Active Directory already exists.")]
		public const int CS_E_OBJECT_ALREADY_EXISTS = unchecked((int)0x8004016A);

		[Description("The path to the software installation data in Active Directory is not correct.")]
		public const int CS_E_INVALID_PATH = unchecked((int)0x8004016B);

		[Description("A network error interrupted the operation.")]
		public const int CS_E_NETWORK_ERROR = unchecked((int)0x8004016C);

		[Description("The size of this object exceeds the maximum size set by the administrator.")]
		public const int CS_E_ADMIN_LIMIT_EXCEEDED = unchecked((int)0x8004016D);

		[Description("The schema for the software installation data in Active Directory does not match the required schema.")]
		public const int CS_E_SCHEMA_MISMATCH = unchecked((int)0x8004016E);

		[Description("An error occurred in the software installation data in Active Directory.")]
		public const int CS_E_INTERNAL_ERROR = unchecked((int)0x8004016F);

		[Description("Cache not updated.")]
		public const int CACHE_E_NOCACHE_UPDATED = unchecked((int)0x80040170);

		[Description("No verbs for OLE object.")]
		public const int OLEOBJ_E_NOVERBS = unchecked((int)0x80040180);

		[Description("Invalid verb for OLE object.")]
		public const int OLEOBJ_E_INVALIDVERB = unchecked((int)0x80040181);

		[Description("Undo is not available.")]
		public const int INPLACE_E_NOTUNDOABLE = unchecked((int)0x800401A0);

		[Description("Space for tools is not available.")]
		public const int INPLACE_E_NOTOOLSPACE = unchecked((int)0x800401A1);

		[Description("OLESTREAM Get method failed.")]
		public const int CONVERT10_E_OLESTREAM_GET = unchecked((int)0x800401C0);

		[Description("OLESTREAM Put method failed.")]
		public const int CONVERT10_E_OLESTREAM_PUT = unchecked((int)0x800401C1);

		[Description("Contents of the OLESTREAM not in correct format.")]
		public const int CONVERT10_E_OLESTREAM_FMT = unchecked((int)0x800401C2);

		[Description("There was an error in a Windows GDI call while converting the bitmap to a device-independent bitmap (DIB).")]
		public const int CONVERT10_E_OLESTREAM_BITMAP_TO_DIB = unchecked((int)0x800401C3);

		[Description("Contents of the IStorage not in correct format.")]
		public const int CONVERT10_E_STG_FMT = unchecked((int)0x800401C4);

		[Description("Contents of IStorage is missing one of the standard streams.")]
		public const int CONVERT10_E_STG_NO_STD_STREAM = unchecked((int)0x800401C5);

		[Description("There was an error in a Windows Graphics Device Interface (GDI) call while converting the DIB to a bitmap.")]
		public const int CONVERT10_E_STG_DIB_TO_BITMAP = unchecked((int)0x800401C6);

		[Description("OpenClipboard failed.")]
		public const int CLIPBRD_E_CANT_OPEN = unchecked((int)0x800401D0);

		[Description("EmptyClipboard failed.")]
		public const int CLIPBRD_E_CANT_EMPTY = unchecked((int)0x800401D1);

		[Description("SetClipboard failed.")]
		public const int CLIPBRD_E_CANT_SET = unchecked((int)0x800401D2);

		[Description("Data on clipboard is invalid.")]
		public const int CLIPBRD_E_BAD_DATA = unchecked((int)0x800401D3);

		[Description("CloseClipboard failed.")]
		public const int CLIPBRD_E_CANT_CLOSE = unchecked((int)0x800401D4);

		[Description("Moniker needs to be connected manually.")]
		public const int MK_E_CONNECTMANUALLY = unchecked((int)0x800401E0);

		[Description("Operation exceeded deadline.")]
		public const int MK_E_EXCEEDEDDEADLINE = unchecked((int)0x800401E1);

		[Description("Moniker needs to be generic.")]
		public const int MK_E_NEEDGENERIC = unchecked((int)0x800401E2);

		[Description("Operation unavailable.")]
		public const int MK_E_UNAVAILABLE = unchecked((int)0x800401E3);

		[Description("Invalid syntax.")]
		public const int MK_E_SYNTAX = unchecked((int)0x800401E4);

		[Description("No object for moniker.")]
		public const int MK_E_NOOBJECT = unchecked((int)0x800401E5);

		[Description("Bad extension for file.")]
		public const int MK_E_INVALIDEXTENSION = unchecked((int)0x800401E6);

		[Description("Intermediate operation failed.")]
		public const int MK_E_INTERMEDIATEINTERFACENOTSUPPORTED = unchecked((int)0x800401E7);

		[Description("Moniker is not bindable.")]
		public const int MK_E_NOTBINDABLE = unchecked((int)0x800401E8);

		[Description("Moniker is not bound.")]
		public const int MK_E_NOTBOUND = unchecked((int)0x800401E9);

		[Description("Moniker cannot open file.")]
		public const int MK_E_CANTOPENFILE = unchecked((int)0x800401EA);

		[Description("User input required for operation to succeed.")]
		public const int MK_E_MUSTBOTHERUSER = unchecked((int)0x800401EB);

		[Description("Moniker class has no inverse.")]
		public const int MK_E_NOINVERSE = unchecked((int)0x800401EC);

		[Description("Moniker does not refer to storage.")]
		public const int MK_E_NOSTORAGE = unchecked((int)0x800401ED);

		[Description("No common prefix.")]
		public const int MK_E_NOPREFIX = unchecked((int)0x800401EE);

		[Description("Moniker could not be enumerated.")]
		public const int MK_E_ENUMERATION_FAILED = unchecked((int)0x800401EF);

		[Description("CoInitialize has not been called.")]
		public const int CO_E_NOTINITIALIZED = unchecked((int)0x800401F0);

		[Description("CoInitialize has already been called.")]
		public const int CO_E_ALREADYINITIALIZED = unchecked((int)0x800401F1);

		[Description("Class of object cannot be determined.")]
		public const int CO_E_CANTDETERMINECLASS = unchecked((int)0x800401F2);

		[Description("Invalid class string.")]
		public const int CO_E_CLASSSTRING = unchecked((int)0x800401F3);

		[Description("Invalid interface string.")]
		public const int CO_E_IIDSTRING = unchecked((int)0x800401F4);

		[Description("Application not found.")]
		public const int CO_E_APPNOTFOUND = unchecked((int)0x800401F5);

		[Description("Application cannot be run more than once.")]
		public const int CO_E_APPSINGLEUSE = unchecked((int)0x800401F6);

		[Description("Some error in application.")]
		public const int CO_E_ERRORINAPP = unchecked((int)0x800401F7);

		[Description("DLL for class not found.")]
		public const int CO_E_DLLNOTFOUND = unchecked((int)0x800401F8);

		[Description("Error in the DLL.")]
		public const int CO_E_ERRORINDLL = unchecked((int)0x800401F9);

		[Description("Wrong operating system or operating system version for application.")]
		public const int CO_E_WRONGOSFORAPP = unchecked((int)0x800401FA);

		[Description("Object is not registered.")]
		public const int CO_E_OBJNOTREG = unchecked((int)0x800401FB);

		[Description("Object is already registered.")]
		public const int CO_E_OBJISREG = unchecked((int)0x800401FC);

		[Description("Object is not connected to server.")]
		public const int CO_E_OBJNOTCONNECTED = unchecked((int)0x800401FD);

		[Description("Application was launched, but it did not register a class factory.")]
		public const int CO_E_APPDIDNTREG = unchecked((int)0x800401FE);

		[Description("Object has been released.")]
		public const int CO_E_RELEASED = unchecked((int)0x800401FF);

		[Description("An event was unable to invoke any of the subscribers.")]
		public const int EVENT_E_ALL_SUBSCRIBERS_FAILED = unchecked((int)0x80040201);

		[Description("A syntax error occurred trying to evaluate a query string.")]
		public const int EVENT_E_QUERYSYNTAX = unchecked((int)0x80040203);

		[Description("An invalid field name was used in a query string.")]
		public const int EVENT_E_QUERYFIELD = unchecked((int)0x80040204);

		[Description("An unexpected exception was raised.")]
		public const int EVENT_E_INTERNALEXCEPTION = unchecked((int)0x80040205);

		[Description("An unexpected internal error was detected.")]
		public const int EVENT_E_INTERNALERROR = unchecked((int)0x80040206);

		[Description("The owner security identifier (SID) on a per-user subscription does not exist.")]
		public const int EVENT_E_INVALID_PER_USER_SID = unchecked((int)0x80040207);

		[Description("A user-supplied component or subscriber raised an exception.")]
		public const int EVENT_E_USER_EXCEPTION = unchecked((int)0x80040208);

		[Description("An interface has too many methods to fire events from.")]
		public const int EVENT_E_TOO_MANY_METHODS = unchecked((int)0x80040209);

		[Description("A subscription cannot be stored unless its event class already exists.")]
		public const int EVENT_E_MISSING_EVENTCLASS = unchecked((int)0x8004020A);

		[Description("Not all the objects requested could be removed.")]
		public const int EVENT_E_NOT_ALL_REMOVED = unchecked((int)0x8004020B);

		[Description("COM+ is required for this operation, but it is not installed.")]
		public const int EVENT_E_COMPLUS_NOT_INSTALLED = unchecked((int)0x8004020C);

		[Description("Cannot modify or delete an object that was not added using the COM+ Administrative SDK.")]
		public const int EVENT_E_CANT_MODIFY_OR_DELETE_UNCONFIGURED_OBJECT = unchecked((int)0x8004020D);

		[Description("Cannot modify or delete an object that was added using the COM+ Administrative SDK.")]
		public const int EVENT_E_CANT_MODIFY_OR_DELETE_CONFIGURED_OBJECT = unchecked((int)0x8004020E);

		[Description("The event class for this subscription is in an invalid partition.")]
		public const int EVENT_E_INVALID_EVENT_CLASS_PARTITION = unchecked((int)0x8004020F);

		[Description("The owner of the PerUser subscription is not logged on to the system specified.")]
		public const int EVENT_E_PER_USER_SID_NOT_LOGGED_ON = unchecked((int)0x80040210);

		[Description("Trigger not found.")]
		public const int SCHED_E_TRIGGER_NOT_FOUND = unchecked((int)0x80041309);

		[Description("One or more of the properties that are needed to run this task have not been set.")]
		public const int SCHED_E_TASK_NOT_READY = unchecked((int)0x8004130A);

		[Description("There is no running instance of the task.")]
		public const int SCHED_E_TASK_NOT_RUNNING = unchecked((int)0x8004130B);

		[Description("The Task Scheduler service is not installed on this computer.")]
		public const int SCHED_E_SERVICE_NOT_INSTALLED = unchecked((int)0x8004130C);

		[Description("The task object could not be opened.")]
		public const int SCHED_E_CANNOT_OPEN_TASK = unchecked((int)0x8004130D);

		[Description("The object is either an invalid task object or is not a task object.")]
		public const int SCHED_E_INVALID_TASK = unchecked((int)0x8004130E);

		[Description("No account information could be found in the Task Scheduler security database for the task indicated.")]
		public const int SCHED_E_ACCOUNT_INFORMATION_NOT_SET = unchecked((int)0x8004130F);

		[Description("Unable to establish existence of the account specified.")]
		public const int SCHED_E_ACCOUNT_NAME_NOT_FOUND = unchecked((int)0x80041310);

		[Description("Corruption was detected in the Task Scheduler security database; the database has been reset.")]
		public const int SCHED_E_ACCOUNT_DBASE_CORRUPT = unchecked((int)0x80041311);

		[Description("Task Scheduler security services are available only on Windows NT operating system.")]
		public const int SCHED_E_NO_SECURITY_SERVICES = unchecked((int)0x80041312);

		[Description("The task object version is either unsupported or invalid.")]
		public const int SCHED_E_UNKNOWN_OBJECT_VERSION = unchecked((int)0x80041313);

		[Description("The task has been configured with an unsupported combination of account settings and run-time options.")]
		public const int SCHED_E_UNSUPPORTED_ACCOUNT_OPTION = unchecked((int)0x80041314);

		[Description("The Task Scheduler service is not running.")]
		public const int SCHED_E_SERVICE_NOT_RUNNING = unchecked((int)0x80041315);

		[Description("The task XML contains an unexpected node.")]
		public const int SCHED_E_UNEXPECTEDNODE = unchecked((int)0x80041316);

		[Description("The task XML contains an element or attribute from an unexpected namespace.")]
		public const int SCHED_E_NAMESPACE = unchecked((int)0x80041317);

		[Description("The task XML contains a value that is incorrectly formatted or out of range.")]
		public const int SCHED_E_INVALIDVALUE = unchecked((int)0x80041318);

		[Description("The task XML is missing a required element or attribute.")]
		public const int SCHED_E_MISSINGNODE = unchecked((int)0x80041319);

		[Description("The task XML is malformed.")]
		public const int SCHED_E_MALFORMEDXML = unchecked((int)0x8004131A);

		[Description("The task XML contains too many nodes of the same type.")]
		public const int SCHED_E_TOO_MANY_NODES = unchecked((int)0x8004131D);

		[Description("The task cannot be started after the trigger's end boundary.")]
		public const int SCHED_E_PAST_END_BOUNDARY = unchecked((int)0x8004131E);

		[Description("An instance of this task is already running.")]
		public const int SCHED_E_ALREADY_RUNNING = unchecked((int)0x8004131F);

		[Description("The task will not run because the user is not logged on.")]
		public const int SCHED_E_USER_NOT_LOGGED_ON = unchecked((int)0x80041320);

		[Description("The task image is corrupt or has been tampered with.")]
		public const int SCHED_E_INVALID_TASK_HASH = unchecked((int)0x80041321);

		[Description("The Task Scheduler service is not available.")]
		public const int SCHED_E_SERVICE_NOT_AVAILABLE = unchecked((int)0x80041322);

		[Description("The Task Scheduler service is too busy to handle your request. Try again later.")]
		public const int SCHED_E_SERVICE_TOO_BUSY = unchecked((int)0x80041323);

		[Description("The Task Scheduler service attempted to run the task, but the task did not run due to one of the constraints in the task definition.")]
		public const int SCHED_E_TASK_ATTEMPTED = unchecked((int)0x80041324);

		[Description("Another single phase resource manager has already been enlisted in this transaction.")]
		public const int XACT_E_ALREADYOTHERSINGLEPHASE = unchecked((int)0x8004D000);

		[Description("A retaining commit or abort is not supported.")]
		public const int XACT_E_CANTRETAIN = unchecked((int)0x8004D001);

		[Description("The transaction failed to commit for an unknown reason. The transaction was aborted.")]
		public const int XACT_E_COMMITFAILED = unchecked((int)0x8004D002);

		[Description("Cannot call commit on this transaction object because the calling application did not initiate the transaction.")]
		public const int XACT_E_COMMITPREVENTED = unchecked((int)0x8004D003);

		[Description("Instead of committing, the resource heuristically aborted.")]
		public const int XACT_E_HEURISTICABORT = unchecked((int)0x8004D004);

		[Description("Instead of aborting, the resource heuristically committed.")]
		public const int XACT_E_HEURISTICCOMMIT = unchecked((int)0x8004D005);

		[Description("Some of the states of the resource were committed while others were aborted, likely because of heuristic decisions.")]
		public const int XACT_E_HEURISTICDAMAGE = unchecked((int)0x8004D006);

		[Description("Some of the states of the resource might have been committed while others were aborted, likely because of heuristic decisions.")]
		public const int XACT_E_HEURISTICDANGER = unchecked((int)0x8004D007);

		[Description("The requested isolation level is not valid or supported.")]
		public const int XACT_E_ISOLATIONLEVEL = unchecked((int)0x8004D008);

		[Description("The transaction manager does not support an asynchronous operation for this method.")]
		public const int XACT_E_NOASYNC = unchecked((int)0x8004D009);

		[Description("Unable to enlist in the transaction.")]
		public const int XACT_E_NOENLIST = unchecked((int)0x8004D00A);

		[Description("The requested semantics of retention of isolation across retaining commit and abort boundaries cannot be supported by this transaction implementation, or isoFlags was not equal to 0.")]
		public const int XACT_E_NOISORETAIN = unchecked((int)0x8004D00B);

		[Description("There is no resource presently associated with this enlistment.")]
		public const int XACT_E_NORESOURCE = unchecked((int)0x8004D00C);

		[Description("The transaction failed to commit due to the failure of optimistic concurrency control in at least one of the resource managers.")]
		public const int XACT_E_NOTCURRENT = unchecked((int)0x8004D00D);

		[Description("The transaction has already been implicitly or explicitly committed or aborted.")]
		public const int XACT_E_NOTRANSACTION = unchecked((int)0x8004D00E);

		[Description("An invalid combination of flags was specified.")]
		public const int XACT_E_NOTSUPPORTED = unchecked((int)0x8004D00F);

		[Description("The resource manager ID is not associated with this transaction or the transaction manager.")]
		public const int XACT_E_UNKNOWNRMGRID = unchecked((int)0x8004D010);

		[Description("This method was called in the wrong state.")]
		public const int XACT_E_WRONGSTATE = unchecked((int)0x8004D011);

		[Description("The indicated unit of work does not match the unit of work expected by the resource manager.")]
		public const int XACT_E_WRONGUOW = unchecked((int)0x8004D012);

		[Description("An enlistment in a transaction already exists.")]
		public const int XACT_E_XTIONEXISTS = unchecked((int)0x8004D013);

		[Description("An import object for the transaction could not be found.")]
		public const int XACT_E_NOIMPORTOBJECT = unchecked((int)0x8004D014);

		[Description("The transaction cookie is invalid.")]
		public const int XACT_E_INVALIDCOOKIE = unchecked((int)0x8004D015);

		[Description("The transaction status is in doubt. A communication failure occurred, or a transaction manager or resource manager has failed.")]
		public const int XACT_E_INDOUBT = unchecked((int)0x8004D016);

		[Description("A time-out was specified, but time-outs are not supported.")]
		public const int XACT_E_NOTIMEOUT = unchecked((int)0x8004D017);

		[Description("The requested operation is already in progress for the transaction.")]
		public const int XACT_E_ALREADYINPROGRESS = unchecked((int)0x8004D018);

		[Description("The transaction has already been aborted.")]
		public const int XACT_E_ABORTED = unchecked((int)0x8004D019);

		[Description("The Transaction Manager returned a log full error.")]
		public const int XACT_E_LOGFULL = unchecked((int)0x8004D01A);

		[Description("The transaction manager is not available.")]
		public const int XACT_E_TMNOTAVAILABLE = unchecked((int)0x8004D01B);

		[Description("A connection with the transaction manager was lost.")]
		public const int XACT_E_CONNECTION_DOWN = unchecked((int)0x8004D01C);

		[Description("A request to establish a connection with the transaction manager was denied.")]
		public const int XACT_E_CONNECTION_DENIED = unchecked((int)0x8004D01D);

		[Description("Resource manager reenlistment to determine transaction status timed out.")]
		public const int XACT_E_REENLISTTIMEOUT = unchecked((int)0x8004D01E);

		[Description("The transaction manager failed to establish a connection with another Transaction Internet Protocol (TIP) transaction manager.")]
		public const int XACT_E_TIP_CONNECT_FAILED = unchecked((int)0x8004D01F);

		[Description("The transaction manager encountered a protocol error with another TIP transaction manager.")]
		public const int XACT_E_TIP_PROTOCOL_ERROR = unchecked((int)0x8004D020);

		[Description("The transaction manager could not propagate a transaction from another TIP transaction manager.")]
		public const int XACT_E_TIP_PULL_FAILED = unchecked((int)0x8004D021);

		[Description("The transaction manager on the destination machine is not available.")]
		public const int XACT_E_DEST_TMNOTAVAILABLE = unchecked((int)0x8004D022);

		[Description("The transaction manager has disabled its support for TIP.")]
		public const int XACT_E_TIP_DISABLED = unchecked((int)0x8004D023);

		[Description("The transaction manager has disabled its support for remote or network transactions.")]
		public const int XACT_E_NETWORK_TX_DISABLED = unchecked((int)0x8004D024);

		[Description("The partner transaction manager has disabled its support for remote or network transactions.")]
		public const int XACT_E_PARTNER_NETWORK_TX_DISABLED = unchecked((int)0x8004D025);

		[Description("The transaction manager has disabled its support for XA transactions.")]
		public const int XACT_E_XA_TX_DISABLED = unchecked((int)0x8004D026);

		[Description("Microsoft Distributed Transaction Coordinator (MSDTC) was unable to read its configuration information.")]
		public const int XACT_E_UNABLE_TO_READ_DTC_CONFIG = unchecked((int)0x8004D027);

		[Description("MSDTC was unable to load the DTC proxy DLL.")]
		public const int XACT_E_UNABLE_TO_LOAD_DTC_PROXY = unchecked((int)0x8004D028);

		[Description("The local transaction has aborted.")]
		public const int XACT_E_ABORTING = unchecked((int)0x8004D029);

		[Description("The specified CRM clerk was not found. It might have completed before it could be held.")]
		public const int XACT_E_CLERKNOTFOUND = unchecked((int)0x8004D080);

		[Description("The specified CRM clerk does not exist.")]
		public const int XACT_E_CLERKEXISTS = unchecked((int)0x8004D081);

		[Description("Recovery of the CRM log file is still in progress.")]
		public const int XACT_E_RECOVERYINPROGRESS = unchecked((int)0x8004D082);

		[Description("The transaction has completed, and the log records have been discarded from the log file. They are no longer available.")]
		public const int XACT_E_TRANSACTIONCLOSED = unchecked((int)0x8004D083);

		[Description("lsnToRead is outside of the current limits of the log")]
		public const int XACT_E_INVALIDLSN = unchecked((int)0x8004D084);

		[Description("The COM+ Compensating Resource Manager has records it wishes to replay.")]
		public const int XACT_E_REPLAYREQUEST = unchecked((int)0x8004D085);

		[Description("The request to connect to the specified transaction coordinator was denied.")]
		public const int XACT_E_CONNECTION_REQUEST_DENIED = unchecked((int)0x8004D100);

		[Description("The maximum number of enlistments for the specified transaction has been reached.")]
		public const int XACT_E_TOOMANY_ENLISTMENTS = unchecked((int)0x8004D101);

		[Description("A resource manager with the same identifier is already registered with the specified transaction coordinator.")]
		public const int XACT_E_DUPLICATE_GUID = unchecked((int)0x8004D102);

		[Description("The prepare request given was not eligible for single-phase optimizations.")]
		public const int XACT_E_NOTSINGLEPHASE = unchecked((int)0x8004D103);

		[Description("RecoveryComplete has already been called for the given resource manager.")]
		public const int XACT_E_RECOVERYALREADYDONE = unchecked((int)0x8004D104);

		[Description("The interface call made was incorrect for the current state of the protocol.")]
		public const int XACT_E_PROTOCOL = unchecked((int)0x8004D105);

		[Description("The xa_open call failed for the XA resource.")]
		public const int XACT_E_RM_FAILURE = unchecked((int)0x8004D106);

		[Description("The xa_recover call failed for the XA resource.")]
		public const int XACT_E_RECOVERY_FAILED = unchecked((int)0x8004D107);

		[Description("The logical unit of work specified cannot be found.")]
		public const int XACT_E_LU_NOT_FOUND = unchecked((int)0x8004D108);

		[Description("The specified logical unit of work already exists.")]
		public const int XACT_E_DUPLICATE_LU = unchecked((int)0x8004D109);

		[Description("Subordinate creation failed. The specified logical unit of work was not connected.")]
		public const int XACT_E_LU_NOT_CONNECTED = unchecked((int)0x8004D10A);

		[Description("A transaction with the given identifier already exists.")]
		public const int XACT_E_DUPLICATE_TRANSID = unchecked((int)0x8004D10B);

		[Description("The resource is in use.")]
		public const int XACT_E_LU_BUSY = unchecked((int)0x8004D10C);

		[Description("The LU Recovery process is down.")]
		public const int XACT_E_LU_NO_RECOVERY_PROCESS = unchecked((int)0x8004D10D);

		[Description("The remote session was lost.")]
		public const int XACT_E_LU_DOWN = unchecked((int)0x8004D10E);

		[Description("The resource is currently recovering.")]
		public const int XACT_E_LU_RECOVERING = unchecked((int)0x8004D10F);

		[Description("There was a mismatch in driving recovery.")]
		public const int XACT_E_LU_RECOVERY_MISMATCH = unchecked((int)0x8004D110);

		[Description("An error occurred with the XA resource.")]
		public const int XACT_E_RM_UNAVAILABLE = unchecked((int)0x8004D111);

		[Description("The root transaction wanted to commit, but the transaction aborted.")]
		public const int CONTEXT_E_ABORTED = unchecked((int)0x8004E002);

		[Description("The COM+ component on which the method call was made has a transaction that has already aborted or is in the process of aborting.")]
		public const int CONTEXT_E_ABORTING = unchecked((int)0x8004E003);

		[Description("There is no Microsoft Transaction Server (MTS) object context.")]
		public const int CONTEXT_E_NOCONTEXT = unchecked((int)0x8004E004);

		[Description("The component is configured to use synchronization, and this method call would cause a deadlock to occur.")]
		public const int CONTEXT_E_WOULD_DEADLOCK = unchecked((int)0x8004E005);

		[Description("The component is configured to use synchronization, and a thread has timed out waiting to enter the context.")]
		public const int CONTEXT_E_SYNCH_TIMEOUT = unchecked((int)0x8004E006);

		[Description("You made a method call on a COM+ component that has a transaction that has already committed or aborted.")]
		public const int CONTEXT_E_OLDREF = unchecked((int)0x8004E007);

		[Description("The specified role was not configured for the application.")]
		public const int CONTEXT_E_ROLENOTFOUND = unchecked((int)0x8004E00C);

		[Description("COM+ was unable to talk to the MSDTC.")]
		public const int CONTEXT_E_TMNOTAVAILABLE = unchecked((int)0x8004E00F);

		[Description("An unexpected error occurred during COM+ activation.")]
		public const int CO_E_ACTIVATIONFAILED = unchecked((int)0x8004E021);

		[Description("COM+ activation failed. Check the event log for more information.")]
		public const int CO_E_ACTIVATIONFAILED_EVENTLOGGED = unchecked((int)0x8004E022);

		[Description("COM+ activation failed due to a catalog or configuration error.")]
		public const int CO_E_ACTIVATIONFAILED_CATALOGERROR = unchecked((int)0x8004E023);

		[Description("COM+ activation failed because the activation could not be completed in the specified amount of time.")]
		public const int CO_E_ACTIVATIONFAILED_TIMEOUT = unchecked((int)0x8004E024);

		[Description("COM+ activation failed because an initialization function failed. Check the event log for more information.")]
		public const int CO_E_INITIALIZATIONFAILED = unchecked((int)0x8004E025);

		[Description("The requested operation requires that just-in-time (JIT) be in the current context, and it is not.")]
		public const int CONTEXT_E_NOJIT = unchecked((int)0x8004E026);

		[Description("The requested operation requires that the current context have a transaction, and it does not.")]
		public const int CONTEXT_E_NOTRANSACTION = unchecked((int)0x8004E027);

		[Description("The components threading model has changed after install into a COM+ application. Re-install component.")]
		public const int CO_E_THREADINGMODEL_CHANGED = unchecked((int)0x8004E028);

		[Description("Internet Information Services (IIS) intrinsics not available. Start your work with IIS.")]
		public const int CO_E_NOIISINTRINSICS = unchecked((int)0x8004E029);

		[Description("An attempt to write a cookie failed.")]
		public const int CO_E_NOCOOKIES = unchecked((int)0x8004E02A);

		[Description("An attempt to use a database generated a database-specific error.")]
		public const int CO_E_DBERROR = unchecked((int)0x8004E02B);

		[Description("The COM+ component you created must use object pooling to work.")]
		public const int CO_E_NOTPOOLED = unchecked((int)0x8004E02C);

		[Description("The COM+ component you created must use object construction to work correctly.")]
		public const int CO_E_NOTCONSTRUCTED = unchecked((int)0x8004E02D);

		[Description("The COM+ component requires synchronization, and it is not configured for it.")]
		public const int CO_E_NOSYNCHRONIZATION = unchecked((int)0x8004E02E);

		[Description("The TxIsolation Level property for the COM+ component being created is stronger than the TxIsolationLevel for the root.")]
		public const int CO_E_ISOLEVELMISMATCH = unchecked((int)0x8004E02F);

		[Description("The component attempted to make a cross-context call between invocations of EnterTransactionScope and ExitTransactionScope. This is not allowed. Cross-context calls cannot be made while inside a transaction scope.")]
		public const int CO_E_CALL_OUT_OF_TX_SCOPE_NOT_ALLOWED = unchecked((int)0x8004E030);

		[Description("The component made a call to EnterTransactionScope, but did not make a corresponding call to ExitTransactionScope before returning.")]
		public const int CO_E_EXIT_TRANSACTION_SCOPE_NOT_CALLED = unchecked((int)0x8004E031);

		[Description("General access denied error.")]
		public const int E_ACCESSDENIED = unchecked((int)0x80070005);

		[Description("The server does not have enough memory for the new channel.")]
		public const int E_OUTOFMEMORY = unchecked((int)0x8007000E);

		[Description("The server cannot support a client request for a dynamic virtual channel.")]
		public const int ERROR_NOT_SUPPORTED = unchecked((int)0x80070032);

		[Description("One or more arguments are invalid.")]
		public const int E_INVALIDARG = unchecked((int)0x80070057);

		[Description("There is not enough space on the disk.")]
		public const int ERROR_DISK_FULL = unchecked((int)0x80070070);

		[Description("Attempt to create a class object failed.")]
		public const int CO_E_CLASS_CREATE_FAILED = unchecked((int)0x80080001);

		[Description("OLE service could not bind object.")]
		public const int CO_E_SCM_ERROR = unchecked((int)0x80080002);

		[Description("RPC communication failed with OLE service.")]
		public const int CO_E_SCM_RPC_FAILURE = unchecked((int)0x80080003);

		[Description("Bad path to object.")]
		public const int CO_E_BAD_PATH = unchecked((int)0x80080004);

		[Description("Server execution failed.")]
		public const int CO_E_SERVER_EXEC_FAILURE = unchecked((int)0x80080005);

		[Description("OLE service could not communicate with the object server.")]
		public const int CO_E_OBJSRV_RPC_FAILURE = unchecked((int)0x80080006);

		[Description("Moniker path could not be normalized.")]
		public const int MK_E_NO_NORMALIZED = unchecked((int)0x80080007);

		[Description("Object server is stopping when OLE service contacts it.")]
		public const int CO_E_SERVER_STOPPING = unchecked((int)0x80080008);

		[Description("An invalid root block pointer was specified.")]
		public const int MEM_E_INVALID_ROOT = unchecked((int)0x80080009);

		[Description("An allocation chain contained an invalid link pointer.")]
		public const int MEM_E_INVALID_LINK = unchecked((int)0x80080010);

		[Description("The requested allocation size was too large.")]
		public const int MEM_E_INVALID_SIZE = unchecked((int)0x80080011);

		[Description("The activation requires a display name to be present under the class identifier (CLSID) key.")]
		public const int CO_E_MISSING_DISPLAYNAME = unchecked((int)0x80080015);

		[Description("The activation requires that the RunAs value for the application is Activate As Activator.")]
		public const int CO_E_RUNAS_VALUE_MUST_BE_AAA = unchecked((int)0x80080016);

		[Description("The class is not configured to support elevated activation.")]
		public const int CO_E_ELEVATION_DISABLED = unchecked((int)0x80080017);

		[Description("Bad UID.")]
		public const int NTE_BAD_UID = unchecked((int)0x80090001);

		[Description("Bad hash.")]
		public const int NTE_BAD_HASH = unchecked((int)0x80090002);

		[Description("Bad key.")]
		public const int NTE_BAD_KEY = unchecked((int)0x80090003);

		[Description("Bad length.")]
		public const int NTE_BAD_LEN = unchecked((int)0x80090004);

		[Description("Bad data.")]
		public const int NTE_BAD_DATA = unchecked((int)0x80090005);

		[Description("Invalid signature.")]
		public const int NTE_BAD_SIGNATURE = unchecked((int)0x80090006);

		[Description("Bad version of provider.")]
		public const int NTE_BAD_VER = unchecked((int)0x80090007);

		[Description("Invalid algorithm specified.")]
		public const int NTE_BAD_ALGID = unchecked((int)0x80090008);

		[Description("Invalid flags specified.")]
		public const int NTE_BAD_FLAGS = unchecked((int)0x80090009);

		[Description("Invalid type specified.")]
		public const int NTE_BAD_TYPE = unchecked((int)0x8009000A);

		[Description("Key not valid for use in specified state.")]
		public const int NTE_BAD_KEY_STATE = unchecked((int)0x8009000B);

		[Description("Hash not valid for use in specified state.")]
		public const int NTE_BAD_HASH_STATE = unchecked((int)0x8009000C);

		[Description("Key does not exist.")]
		public const int NTE_NO_KEY = unchecked((int)0x8009000D);

		[Description("Insufficient memory available for the operation.")]
		public const int NTE_NO_MEMORY = unchecked((int)0x8009000E);

		[Description("Object already exists.")]
		public const int NTE_EXISTS = unchecked((int)0x8009000F);

		[Description("Access denied.")]
		public const int NTE_PERM = unchecked((int)0x80090010);

		[Description("Object was not found.")]
		public const int NTE_NOT_FOUND = unchecked((int)0x80090011);

		[Description("Data already encrypted.")]
		public const int NTE_DOUBLE_ENCRYPT = unchecked((int)0x80090012);

		[Description("Invalid provider specified.")]
		public const int NTE_BAD_PROVIDER = unchecked((int)0x80090013);

		[Description("Invalid provider type specified.")]
		public const int NTE_BAD_PROV_TYPE = unchecked((int)0x80090014);

		[Description("Provider's public key is invalid.")]
		public const int NTE_BAD_PUBLIC_KEY = unchecked((int)0x80090015);

		[Description("Key set does not exist.")]
		public const int NTE_BAD_KEYSET = unchecked((int)0x80090016);

		[Description("Provider type not defined.")]
		public const int NTE_PROV_TYPE_NOT_DEF = unchecked((int)0x80090017);

		[Description("The provider type, as registered, is invalid.")]
		public const int NTE_PROV_TYPE_ENTRY_BAD = unchecked((int)0x80090018);

		[Description("The key set is not defined.")]
		public const int NTE_KEYSET_NOT_DEF = unchecked((int)0x80090019);

		[Description("The key set, as registered, is invalid.")]
		public const int NTE_KEYSET_ENTRY_BAD = unchecked((int)0x8009001A);

		[Description("Provider type does not match registered value.")]
		public const int NTE_PROV_TYPE_NO_MATCH = unchecked((int)0x8009001B);

		[Description("The digital signature file is corrupt.")]
		public const int NTE_SIGNATURE_FILE_BAD = unchecked((int)0x8009001C);

		[Description("Provider DLL failed to initialize correctly.")]
		public const int NTE_PROVIDER_DLL_FAIL = unchecked((int)0x8009001D);

		[Description("Provider DLL could not be found.")]
		public const int NTE_PROV_DLL_NOT_FOUND = unchecked((int)0x8009001E);

		[Description("The keyset parameter is invalid.")]
		public const int NTE_BAD_KEYSET_PARAM = unchecked((int)0x8009001F);

		[Description("An internal error occurred.")]
		public const int NTE_FAIL = unchecked((int)0x80090020);

		[Description("A base error occurred.")]
		public const int NTE_SYS_ERR = unchecked((int)0x80090021);

		[Description("Provider could not perform the action because the context was acquired as silent.")]
		public const int NTE_SILENT_CONTEXT = unchecked((int)0x80090022);

		[Description("The security token does not have storage space available for an additional container.")]
		public const int NTE_TOKEN_KEYSET_STORAGE_FULL = unchecked((int)0x80090023);

		[Description("The profile for the user is a temporary profile.")]
		public const int NTE_TEMPORARY_PROFILE = unchecked((int)0x80090024);

		[Description("The key parameters could not be set because the configuration service provider (CSP) uses fixed parameters.")]
		public const int NTE_FIXEDPARAMETER = unchecked((int)0x80090025);

		[Description("The supplied handle is invalid.")]
		public const int NTE_INVALID_HANDLE = unchecked((int)0x80090026);

		[Description("The parameter is incorrect.")]
		public const int NTE_INVALID_PARAMETER = unchecked((int)0x80090027);

		[Description("The buffer supplied to a function was too small.")]
		public const int NTE_BUFFER_TOO_SMALL = unchecked((int)0x80090028);

		[Description("The requested operation is not supported.")]
		public const int NTE_NOT_SUPPORTED = unchecked((int)0x80090029);

		[Description("No more data is available.")]
		public const int NTE_NO_MORE_ITEMS = unchecked((int)0x8009002A);

		[Description("The supplied buffers overlap incorrectly.")]
		public const int NTE_BUFFERS_OVERLAP = unchecked((int)0x8009002B);

		[Description("The specified data could not be decrypted.")]
		public const int NTE_DECRYPTION_FAILURE = unchecked((int)0x8009002C);

		[Description("An internal consistency check failed.")]
		public const int NTE_INTERNAL_ERROR = unchecked((int)0x8009002D);

		[Description("This operation requires input from the user.")]
		public const int NTE_UI_REQUIRED = unchecked((int)0x8009002E);

		[Description("The cryptographic provider does not support Hash Message Authentication Code (HMAC).")]
		public const int NTE_HMAC_NOT_SUPPORTED = unchecked((int)0x8009002F);

		[Description("Not enough memory is available to complete this request.")]
		public const int SEC_E_INSUFFICIENT_MEMORY = unchecked((int)0x80090300);

		[Description("The handle specified is invalid.")]
		public const int SEC_E_INVALID_HANDLE = unchecked((int)0x80090301);

		[Description("The function requested is not supported.")]
		public const int SEC_E_UNSUPPORTED_FUNCTION = unchecked((int)0x80090302);

		[Description("The specified target is unknown or unreachable.")]
		public const int SEC_E_TARGET_UNKNOWN = unchecked((int)0x80090303);

		[Description("The Local Security Authority (LSA) cannot be contacted.")]
		public const int SEC_E_INTERNAL_ERROR = unchecked((int)0x80090304);

		[Description("The requested security package does not exist.")]
		public const int SEC_E_SECPKG_NOT_FOUND = unchecked((int)0x80090305);

		[Description("The caller is not the owner of the desired credentials.")]
		public const int SEC_E_NOT_OWNER = unchecked((int)0x80090306);

		[Description("The security package failed to initialize and cannot be installed.")]
		public const int SEC_E_CANNOT_INSTALL = unchecked((int)0x80090307);

		[Description("The token supplied to the function is invalid.")]
		public const int SEC_E_INVALID_TOKEN = unchecked((int)0x80090308);

		[Description("The security package is not able to marshal the logon buffer, so the logon attempt has failed.")]
		public const int SEC_E_CANNOT_PACK = unchecked((int)0x80090309);

		[Description("The per-message quality of protection is not supported by the security package.")]
		public const int SEC_E_QOP_NOT_SUPPORTED = unchecked((int)0x8009030A);

		[Description("The security context does not allow impersonation of the client.")]
		public const int SEC_E_NO_IMPERSONATION = unchecked((int)0x8009030B);

		[Description("The logon attempt failed.")]
		public const int SEC_E_LOGON_DENIED = unchecked((int)0x8009030C);

		[Description("The credentials supplied to the package were not recognized.")]
		public const int SEC_E_UNKNOWN_CREDENTIALS = unchecked((int)0x8009030D);

		[Description("No credentials are available in the security package.")]
		public const int SEC_E_NO_CREDENTIALS = unchecked((int)0x8009030E);

		[Description("The message or signature supplied for verification has been altered.")]
		public const int SEC_E_MESSAGE_ALTERED = unchecked((int)0x8009030F);

		[Description("The message supplied for verification is out of sequence.")]
		public const int SEC_E_OUT_OF_SEQUENCE = unchecked((int)0x80090310);

		[Description("No authority could be contacted for authentication.")]
		public const int SEC_E_NO_AUTHENTICATING_AUTHORITY = unchecked((int)0x80090311);

		[Description("The requested security package does not exist.")]
		public const int SEC_E_BAD_PKGID = unchecked((int)0x80090316);

		[Description("The context has expired and can no longer be used.")]
		public const int SEC_E_CONTEXT_EXPIRED = unchecked((int)0x80090317);

		[Description("The supplied message is incomplete. The signature was not verified.")]
		public const int SEC_E_INCOMPLETE_MESSAGE = unchecked((int)0x80090318);

		[Description("The credentials supplied were not complete and could not be verified. The context could not be initialized.")]
		public const int SEC_E_INCOMPLETE_CREDENTIALS = unchecked((int)0x80090320);

		[Description("The buffers supplied to a function was too small.")]
		public const int SEC_E_BUFFER_TOO_SMALL = unchecked((int)0x80090321);

		[Description("The target principal name is incorrect.")]
		public const int SEC_E_WRONG_PRINCIPAL = unchecked((int)0x80090322);

		[Description("The clocks on the client and server machines are skewed.")]
		public const int SEC_E_TIME_SKEW = unchecked((int)0x80090324);

		[Description("The certificate chain was issued by an authority that is not trusted.")]
		public const int SEC_E_UNTRUSTED_ROOT = unchecked((int)0x80090325);

		[Description("The message received was unexpected or badly formatted.")]
		public const int SEC_E_ILLEGAL_MESSAGE = unchecked((int)0x80090326);

		[Description("An unknown error occurred while processing the certificate.")]
		public const int SEC_E_CERT_UNKNOWN = unchecked((int)0x80090327);

		[Description("The received certificate has expired.")]
		public const int SEC_E_CERT_EXPIRED = unchecked((int)0x80090328);

		[Description("The specified data could not be encrypted.")]
		public const int SEC_E_ENCRYPT_FAILURE = unchecked((int)0x80090329);

		[Description("The specified data could not be decrypted.")]
		public const int SEC_E_DECRYPT_FAILURE = unchecked((int)0x80090330);

		[Description("The client and server cannot communicate because they do not possess a common algorithm.")]
		public const int SEC_E_ALGORITHM_MISMATCH = unchecked((int)0x80090331);

		[Description("The security context could not be established due to a failure in the requested quality of service (for example, mutual authentication or delegation).")]
		public const int SEC_E_SECURITY_QOS_FAILED = unchecked((int)0x80090332);

		[Description("A security context was deleted before the context was completed. This is considered a logon failure.")]
		public const int SEC_E_UNFINISHED_CONTEXT_DELETED = unchecked((int)0x80090333);

		[Description("The client is trying to negotiate a context and the server requires user-to-user but did not send a ticket granting ticket (TGT) reply.")]
		public const int SEC_E_NO_TGT_REPLY = unchecked((int)0x80090334);

		[Description("Unable to accomplish the requested task because the local machine does not have an IP addresses.")]
		public const int SEC_E_NO_IP_ADDRESSES = unchecked((int)0x80090335);

		[Description("The supplied credential handle does not match the credential associated with the security context.")]
		public const int SEC_E_WRONG_CREDENTIAL_HANDLE = unchecked((int)0x80090336);

		[Description("The cryptographic system or checksum function is invalid because a required function is unavailable.")]
		public const int SEC_E_CRYPTO_SYSTEM_INVALID = unchecked((int)0x80090337);

		[Description("The number of maximum ticket referrals has been exceeded.")]
		public const int SEC_E_MAX_REFERRALS_EXCEEDED = unchecked((int)0x80090338);

		[Description("The local machine must be a Kerberos domain controller (KDC), and it is not.")]
		public const int SEC_E_MUST_BE_KDC = unchecked((int)0x80090339);

		[Description("The other end of the security negotiation requires strong cryptographics, but it is not supported on the local machine.")]
		public const int SEC_E_STRONG_CRYPTO_NOT_SUPPORTED = unchecked((int)0x8009033A);

		[Description("The KDC reply contained more than one principal name.")]
		public const int SEC_E_TOO_MANY_PRINCIPALS = unchecked((int)0x8009033B);

		[Description("Expected to find PA data for a hint of what etype to use, but it was not found.")]
		public const int SEC_E_NO_PA_DATA = unchecked((int)0x8009033C);

		[Description("The client certificate does not contain a valid user principal name (UPN), or does not match the client name in the logon request. Contact your administrator.")]
		public const int SEC_E_PKINIT_NAME_MISMATCH = unchecked((int)0x8009033D);

		[Description("Smart card logon is required and was not used.")]
		public const int SEC_E_SMARTCARD_LOGON_REQUIRED = unchecked((int)0x8009033E);

		[Description("A system shutdown is in progress.")]
		public const int SEC_E_SHUTDOWN_IN_PROGRESS = unchecked((int)0x8009033F);

		[Description("An invalid request was sent to the KDC.")]
		public const int SEC_E_KDC_INVALID_REQUEST = unchecked((int)0x80090340);

		[Description("The KDC was unable to generate a referral for the service requested.")]
		public const int SEC_E_KDC_UNABLE_TO_REFER = unchecked((int)0x80090341);

		[Description("The encryption type requested is not supported by the KDC.")]
		public const int SEC_E_KDC_UNKNOWN_ETYPE = unchecked((int)0x80090342);

		[Description("An unsupported pre-authentication mechanism was presented to the Kerberos package.")]
		public const int SEC_E_UNSUPPORTED_PREAUTH = unchecked((int)0x80090343);

		[Description("The requested operation cannot be completed. The computer must be trusted for delegation, and the current user account must be configured to allow delegation.")]
		public const int SEC_E_DELEGATION_REQUIRED = unchecked((int)0x80090345);

		[Description("Client's supplied Security Support Provider Interface (SSPI) channel bindings were incorrect.")]
		public const int SEC_E_BAD_BINDINGS = unchecked((int)0x80090346);

		[Description("The received certificate was mapped to multiple accounts.")]
		public const int SEC_E_MULTIPLE_ACCOUNTS = unchecked((int)0x80090347);

		[Description("No Kerberos key was found.")]
		public const int SEC_E_NO_KERB_KEY = unchecked((int)0x80090348);

		[Description("The certificate is not valid for the requested usage.")]
		public const int SEC_E_CERT_WRONG_USAGE = unchecked((int)0x80090349);

		[Description("The system detected a possible attempt to compromise security. Ensure that you can contact the server that authenticated you.")]
		public const int SEC_E_DOWNGRADE_DETECTED = unchecked((int)0x80090350);

		[Description("The smart card certificate used for authentication has been revoked. Contact your system administrator. The event log might contain additional information.")]
		public const int SEC_E_SMARTCARD_CERT_REVOKED = unchecked((int)0x80090351);

		[Description("An untrusted certification authority (CA) was detected while processing the smart card certificate used for authentication. Contact your system administrator.")]
		public const int SEC_E_ISSUING_CA_UNTRUSTED = unchecked((int)0x80090352);

		[Description("The revocation status of the smart card certificate used for authentication could not be determined. Contact your system administrator.")]
		public const int SEC_E_REVOCATION_OFFLINE_C = unchecked((int)0x80090353);

		[Description("The smart card certificate used for authentication was not trusted. Contact your system administrator.")]
		public const int SEC_E_PKINIT_CLIENT_FAILURE = unchecked((int)0x80090354);

		[Description("The smart card certificate used for authentication has expired. Contact your system administrator.")]
		public const int SEC_E_SMARTCARD_CERT_EXPIRED = unchecked((int)0x80090355);

		[Description("The Kerberos subsystem encountered an error. A service for user protocol requests was made against a domain controller that does not support services for users.")]
		public const int SEC_E_NO_S4U_PROT_SUPPORT = unchecked((int)0x80090356);

		[Description("An attempt was made by this server to make a Kerberos-constrained delegation request for a target outside the server's realm. This is not supported and indicates a misconfiguration on this server's allowed-to-delegate-to list. Contact your administrator.")]
		public const int SEC_E_CROSSREALM_DELEGATION_FAILURE = unchecked((int)0x80090357);

		[Description("The revocation status of the domain controller certificate used for smart card authentication could not be determined. The system event log contains additional information. Contact your system administrator.")]
		public const int SEC_E_REVOCATION_OFFLINE_KDC = unchecked((int)0x80090358);

		[Description("An untrusted CA was detected while processing the domain controller certificate used for authentication. The system event log contains additional information. Contact your system administrator.")]
		public const int SEC_E_ISSUING_CA_UNTRUSTED_KDC = unchecked((int)0x80090359);

		[Description("The domain controller certificate used for smart card logon has expired. Contact your system administrator with the contents of your system event log.")]
		public const int SEC_E_KDC_CERT_EXPIRED = unchecked((int)0x8009035A);

		[Description("The domain controller certificate used for smart card logon has been revoked. Contact your system administrator with the contents of your system event log.")]
		public const int SEC_E_KDC_CERT_REVOKED = unchecked((int)0x8009035B);

		[Description("One or more of the parameters passed to the function were invalid.")]
		public const int SEC_E_INVALID_PARAMETER = unchecked((int)0x8009035D);

		[Description("The client policy does not allow credential delegation to the target server.")]
		public const int SEC_E_DELEGATION_POLICY = unchecked((int)0x8009035E);

		[Description("The client policy does not allow credential delegation to the target server with NLTM only authentication.")]
		public const int SEC_E_POLICY_NLTM_ONLY = unchecked((int)0x8009035F);

		[Description("An error occurred while performing an operation on a cryptographic message.")]
		public const int CRYPT_E_MSG_ERROR = unchecked((int)0x80091001);

		[Description("Unknown cryptographic algorithm.")]
		public const int CRYPT_E_UNKNOWN_ALGO = unchecked((int)0x80091002);

		[Description("The object identifier is poorly formatted.")]
		public const int CRYPT_E_OID_FORMAT = unchecked((int)0x80091003);

		[Description("Invalid cryptographic message type.")]
		public const int CRYPT_E_INVALID_MSG_TYPE = unchecked((int)0x80091004);

		[Description("Unexpected cryptographic message encoding.")]
		public const int CRYPT_E_UNEXPECTED_ENCODING = unchecked((int)0x80091005);

		[Description("The cryptographic message does not contain an expected authenticated attribute.")]
		public const int CRYPT_E_AUTH_ATTR_MISSING = unchecked((int)0x80091006);

		[Description("The hash value is not correct.")]
		public const int CRYPT_E_HASH_VALUE = unchecked((int)0x80091007);

		[Description("The index value is not valid.")]
		public const int CRYPT_E_INVALID_INDEX = unchecked((int)0x80091008);

		[Description("The content of the cryptographic message has already been decrypted.")]
		public const int CRYPT_E_ALREADY_DECRYPTED = unchecked((int)0x80091009);

		[Description("The content of the cryptographic message has not been decrypted yet.")]
		public const int CRYPT_E_NOT_DECRYPTED = unchecked((int)0x8009100A);

		[Description("The enveloped-data message does not contain the specified recipient.")]
		public const int CRYPT_E_RECIPIENT_NOT_FOUND = unchecked((int)0x8009100B);

		[Description("Invalid control type.")]
		public const int CRYPT_E_CONTROL_TYPE = unchecked((int)0x8009100C);

		[Description("Invalid issuer or serial number.")]
		public const int CRYPT_E_ISSUER_SERIALNUMBER = unchecked((int)0x8009100D);

		[Description("Cannot find the original signer.")]
		public const int CRYPT_E_SIGNER_NOT_FOUND = unchecked((int)0x8009100E);

		[Description("The cryptographic message does not contain all of the requested attributes.")]
		public const int CRYPT_E_ATTRIBUTES_MISSING = unchecked((int)0x8009100F);

		[Description("The streamed cryptographic message is not ready to return data.")]
		public const int CRYPT_E_STREAM_MSG_NOT_READY = unchecked((int)0x80091010);

		[Description("The streamed cryptographic message requires more data to complete the decode operation.")]
		public const int CRYPT_E_STREAM_INSUFFICIENT_DATA = unchecked((int)0x80091011);

		[Description("The length specified for the output data was insufficient.")]
		public const int CRYPT_E_BAD_LEN = unchecked((int)0x80092001);

		[Description("An error occurred during the encode or decode operation.")]
		public const int CRYPT_E_BAD_ENCODE = unchecked((int)0x80092002);

		[Description("An error occurred while reading or writing to a file.")]
		public const int CRYPT_E_FILE_ERROR = unchecked((int)0x80092003);

		[Description("Cannot find object or property.")]
		public const int CRYPT_E_NOT_FOUND = unchecked((int)0x80092004);

		[Description("The object or property already exists.")]
		public const int CRYPT_E_EXISTS = unchecked((int)0x80092005);

		[Description("No provider was specified for the store or object.")]
		public const int CRYPT_E_NO_PROVIDER = unchecked((int)0x80092006);

		[Description("The specified certificate is self-signed.")]
		public const int CRYPT_E_SELF_SIGNED = unchecked((int)0x80092007);

		[Description("The previous certificate or certificate revocation list (CRL) context was deleted.")]
		public const int CRYPT_E_DELETED_PREV = unchecked((int)0x80092008);

		[Description("Cannot find the requested object.")]
		public const int CRYPT_E_NO_MATCH = unchecked((int)0x80092009);

		[Description("The certificate does not have a property that references a private key.")]
		public const int CRYPT_E_UNEXPECTED_MSG_TYPE = unchecked((int)0x8009200A);

		[Description("Cannot find the certificate and private key for decryption.")]
		public const int CRYPT_E_NO_KEY_PROPERTY = unchecked((int)0x8009200B);

		[Description("Cannot find the certificate and private key to use for decryption.")]
		public const int CRYPT_E_NO_DECRYPT_CERT = unchecked((int)0x8009200C);

		[Description("Not a cryptographic message or the cryptographic message is not formatted correctly.")]
		public const int CRYPT_E_BAD_MSG = unchecked((int)0x8009200D);

		[Description("The signed cryptographic message does not have a signer for the specified signer index.")]
		public const int CRYPT_E_NO_SIGNER = unchecked((int)0x8009200E);

		[Description("Final closure is pending until additional frees or closes.")]
		public const int CRYPT_E_PENDING_CLOSE = unchecked((int)0x8009200F);

		[Description("The certificate is revoked.")]
		public const int CRYPT_E_REVOKED = unchecked((int)0x80092010);

		[Description("No DLL or exported function was found to verify revocation.")]
		public const int CRYPT_E_NO_REVOCATION_DLL = unchecked((int)0x80092011);

		[Description("The revocation function was unable to check revocation for the certificate.")]
		public const int CRYPT_E_NO_REVOCATION_CHECK = unchecked((int)0x80092012);

		[Description("The revocation function was unable to check revocation because the revocation server was offline.")]
		public const int CRYPT_E_REVOCATION_OFFLINE = unchecked((int)0x80092013);

		[Description("The certificate is not in the revocation server's database.")]
		public const int CRYPT_E_NOT_IN_REVOCATION_DATABASE = unchecked((int)0x80092014);

		[Description("The string contains a non-numeric character.")]
		public const int CRYPT_E_INVALID_NUMERIC_STRING = unchecked((int)0x80092020);

		[Description("The string contains a nonprintable character.")]
		public const int CRYPT_E_INVALID_PRINTABLE_STRING = unchecked((int)0x80092021);

		[Description("The string contains a character not in the 7-bit ASCII character set.")]
		public const int CRYPT_E_INVALID_IA5_STRING = unchecked((int)0x80092022);

		[Description("The string contains an invalid X500 name attribute key, object identifier (OID), value, or delimiter.")]
		public const int CRYPT_E_INVALID_X500_STRING = unchecked((int)0x80092023);

		[Description("The dwValueType for the CERT_NAME_VALUE is not one of the character strings. Most likely it is either a CERT_RDN_ENCODED_BLOB or CERT_TDN_OCTED_STRING.")]
		public const int CRYPT_E_NOT_CHAR_STRING = unchecked((int)0x80092024);

		[Description("The Put operation cannot continue. The file needs to be resized. However, there is already a signature present. A complete signing operation must be done.")]
		public const int CRYPT_E_FILERESIZED = unchecked((int)0x80092025);

		[Description("The cryptographic operation failed due to a local security option setting.")]
		public const int CRYPT_E_SECURITY_SETTINGS = unchecked((int)0x80092026);

		[Description("No DLL or exported function was found to verify subject usage.")]
		public const int CRYPT_E_NO_VERIFY_USAGE_DLL = unchecked((int)0x80092027);

		[Description("The called function was unable to perform a usage check on the subject.")]
		public const int CRYPT_E_NO_VERIFY_USAGE_CHECK = unchecked((int)0x80092028);

		[Description("The called function was unable to complete the usage check because the server was offline.")]
		public const int CRYPT_E_VERIFY_USAGE_OFFLINE = unchecked((int)0x80092029);

		[Description("The subject was not found in a certificate trust list (CTL).")]
		public const int CRYPT_E_NOT_IN_CTL = unchecked((int)0x8009202A);

		[Description("None of the signers of the cryptographic message or certificate trust list is trusted.")]
		public const int CRYPT_E_NO_TRUSTED_SIGNER = unchecked((int)0x8009202B);

		[Description("The public key's algorithm parameters are missing.")]
		public const int CRYPT_E_MISSING_PUBKEY_PARA = unchecked((int)0x8009202C);

		[Description("OSS Certificate encode/decode error code base.")]
		public const int CRYPT_E_OSS_ERROR = unchecked((int)0x80093000);

		[Description("OSS ASN.1 Error: Output Buffer is too small.")]
		public const int OSS_MORE_BUF = unchecked((int)0x80093001);

		[Description("OSS ASN.1 Error: Signed integer is encoded as a unsigned integer.")]
		public const int OSS_NEGATIVE_UINTEGER = unchecked((int)0x80093002);

		[Description("OSS ASN.1 Error: Unknown ASN.1 data type.")]
		public const int OSS_PDU_RANGE = unchecked((int)0x80093003);

		[Description("OSS ASN.1 Error: Output buffer is too small; the decoded data has been truncated.")]
		public const int OSS_MORE_INPUT = unchecked((int)0x80093004);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_DATA_ERROR = unchecked((int)0x80093005);

		[Description("OSS ASN.1 Error: Invalid argument.")]
		public const int OSS_BAD_ARG = unchecked((int)0x80093006);

		[Description("OSS ASN.1 Error: Encode/Decode version mismatch.")]
		public const int OSS_BAD_VERSION = unchecked((int)0x80093007);

		[Description("OSS ASN.1 Error: Out of memory.")]
		public const int OSS_OUT_MEMORY = unchecked((int)0x80093008);

		[Description("OSS ASN.1 Error: Encode/Decode error.")]
		public const int OSS_PDU_MISMATCH = unchecked((int)0x80093009);

		[Description("OSS ASN.1 Error: Internal error.")]
		public const int OSS_LIMITED = unchecked((int)0x8009300A);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_BAD_PTR = unchecked((int)0x8009300B);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_BAD_TIME = unchecked((int)0x8009300C);

		[Description("OSS ASN.1 Error: Unsupported BER indefinite-length encoding.")]
		public const int OSS_INDEFINITE_NOT_SUPPORTED = unchecked((int)0x8009300D);

		[Description("OSS ASN.1 Error: Access violation.")]
		public const int OSS_MEM_ERROR = unchecked((int)0x8009300E);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_BAD_TABLE = unchecked((int)0x8009300F);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_TOO_LONG = unchecked((int)0x80093010);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_CONSTRAINT_VIOLATED = unchecked((int)0x80093011);

		[Description("OSS ASN.1 Error: Internal error.")]
		public const int OSS_FATAL_ERROR = unchecked((int)0x80093012);

		[Description("OSS ASN.1 Error: Multithreading conflict.")]
		public const int OSS_ACCESS_SERIALIZATION_ERROR = unchecked((int)0x80093013);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_NULL_TBL = unchecked((int)0x80093014);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_NULL_FCN = unchecked((int)0x80093015);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_BAD_ENCRULES = unchecked((int)0x80093016);

		[Description("OSS ASN.1 Error: Encode/Decode function not implemented.")]
		public const int OSS_UNAVAIL_ENCRULES = unchecked((int)0x80093017);

		[Description("OSS ASN.1 Error: Trace file error.")]
		public const int OSS_CANT_OPEN_TRACE_WINDOW = unchecked((int)0x80093018);

		[Description("OSS ASN.1 Error: Function not implemented.")]
		public const int OSS_UNIMPLEMENTED = unchecked((int)0x80093019);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_OID_DLL_NOT_LINKED = unchecked((int)0x8009301A);

		[Description("OSS ASN.1 Error: Trace file error.")]
		public const int OSS_CANT_OPEN_TRACE_FILE = unchecked((int)0x8009301B);

		[Description("OSS ASN.1 Error: Trace file error.")]
		public const int OSS_TRACE_FILE_ALREADY_OPEN = unchecked((int)0x8009301C);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_TABLE_MISMATCH = unchecked((int)0x8009301D);

		[Description("OSS ASN.1 Error: Invalid data.")]
		public const int OSS_TYPE_NOT_SUPPORTED = unchecked((int)0x8009301E);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_REAL_DLL_NOT_LINKED = unchecked((int)0x8009301F);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_REAL_CODE_NOT_LINKED = unchecked((int)0x80093020);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_OUT_OF_RANGE = unchecked((int)0x80093021);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_COPIER_DLL_NOT_LINKED = unchecked((int)0x80093022);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_CONSTRAINT_DLL_NOT_LINKED = unchecked((int)0x80093023);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_COMPARATOR_DLL_NOT_LINKED = unchecked((int)0x80093024);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_COMPARATOR_CODE_NOT_LINKED = unchecked((int)0x80093025);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_MEM_MGR_DLL_NOT_LINKED = unchecked((int)0x80093026);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_PDV_DLL_NOT_LINKED = unchecked((int)0x80093027);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_PDV_CODE_NOT_LINKED = unchecked((int)0x80093028);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_API_DLL_NOT_LINKED = unchecked((int)0x80093029);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_BERDER_DLL_NOT_LINKED = unchecked((int)0x8009302A);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_PER_DLL_NOT_LINKED = unchecked((int)0x8009302B);

		[Description("OSS ASN.1 Error: Program link error.")]
		public const int OSS_OPEN_TYPE_ERROR = unchecked((int)0x8009302C);

		[Description("OSS ASN.1 Error: System resource error.")]
		public const int OSS_MUTEX_NOT_CREATED = unchecked((int)0x8009302D);

		[Description("OSS ASN.1 Error: Trace file error.")]
		public const int OSS_CANT_CLOSE_TRACE_FILE = unchecked((int)0x8009302E);

		[Description("ASN1 Certificate encode/decode error code base.")]
		public const int CRYPT_E_ASN1_ERROR = unchecked((int)0x80093100);

		[Description("ASN1 internal encode or decode error.")]
		public const int CRYPT_E_ASN1_INTERNAL = unchecked((int)0x80093101);

		[Description("ASN1 unexpected end of data.")]
		public const int CRYPT_E_ASN1_EOD = unchecked((int)0x80093102);

		[Description("ASN1 corrupted data.")]
		public const int CRYPT_E_ASN1_CORRUPT = unchecked((int)0x80093103);

		[Description("ASN1 value too large.")]
		public const int CRYPT_E_ASN1_LARGE = unchecked((int)0x80093104);

		[Description("ASN1 constraint violated.")]
		public const int CRYPT_E_ASN1_CONSTRAINT = unchecked((int)0x80093105);

		[Description("ASN1 out of memory.")]
		public const int CRYPT_E_ASN1_MEMORY = unchecked((int)0x80093106);

		[Description("ASN1 buffer overflow.")]
		public const int CRYPT_E_ASN1_OVERFLOW = unchecked((int)0x80093107);

		[Description("ASN1 function not supported for this protocol data unit (PDU).")]
		public const int CRYPT_E_ASN1_BADPDU = unchecked((int)0x80093108);

		[Description("ASN1 bad arguments to function call.")]
		public const int CRYPT_E_ASN1_BADARGS = unchecked((int)0x80093109);

		[Description("ASN1 bad real value.")]
		public const int CRYPT_E_ASN1_BADREAL = unchecked((int)0x8009310A);

		[Description("ASN1 bad tag value met.")]
		public const int CRYPT_E_ASN1_BADTAG = unchecked((int)0x8009310B);

		[Description("ASN1 bad choice value.")]
		public const int CRYPT_E_ASN1_CHOICE = unchecked((int)0x8009310C);

		[Description("ASN1 bad encoding rule.")]
		public const int CRYPT_E_ASN1_RULE = unchecked((int)0x8009310D);

		[Description("ASN1 bad Unicode (UTF8).")]
		public const int CRYPT_E_ASN1_UTF8 = unchecked((int)0x8009310E);

		[Description("ASN1 bad PDU type.")]
		public const int CRYPT_E_ASN1_PDU_TYPE = unchecked((int)0x80093133);

		[Description("ASN1 not yet implemented.")]
		public const int CRYPT_E_ASN1_NYI = unchecked((int)0x80093134);

		[Description("ASN1 skipped unknown extensions.")]
		public const int CRYPT_E_ASN1_EXTENDED = unchecked((int)0x80093201);

		[Description("ASN1 end of data expected.")]
		public const int CRYPT_E_ASN1_NOEOD = unchecked((int)0x80093202);

		[Description("The request subject name is invalid or too long.")]
		public const int CERTSRV_E_BAD_REQUESTSUBJECT = unchecked((int)0x80094001);

		[Description("The request does not exist.")]
		public const int CERTSRV_E_NO_REQUEST = unchecked((int)0x80094002);

		[Description("The request's current status does not allow this operation.")]
		public const int CERTSRV_E_BAD_REQUESTSTATUS = unchecked((int)0x80094003);

		[Description("The requested property value is empty.")]
		public const int CERTSRV_E_PROPERTY_EMPTY = unchecked((int)0x80094004);

		[Description("The CA's certificate contains invalid data.")]
		public const int CERTSRV_E_INVALID_CA_CERTIFICATE = unchecked((int)0x80094005);

		[Description("Certificate service has been suspended for a database restore operation.")]
		public const int CERTSRV_E_SERVER_SUSPENDED = unchecked((int)0x80094006);

		[Description("The certificate contains an encoded length that is potentially incompatible with older enrollment software.")]
		public const int CERTSRV_E_ENCODING_LENGTH = unchecked((int)0x80094007);

		[Description("The operation is denied. The user has multiple roles assigned, and the CA is configured to enforce role separation.")]
		public const int CERTSRV_E_ROLECONFLICT = unchecked((int)0x80094008);

		[Description("The operation is denied. It can only be performed by a certificate manager that is allowed to manage certificates for the current requester.")]
		public const int CERTSRV_E_RESTRICTEDOFFICER = unchecked((int)0x80094009);

		[Description("Cannot archive private key. The CA is not configured for key archival.")]
		public const int CERTSRV_E_KEY_ARCHIVAL_NOT_CONFIGURED = unchecked((int)0x8009400A);

		[Description("Cannot archive private key. The CA could not verify one or more key recovery certificates.")]
		public const int CERTSRV_E_NO_VALID_KRA = unchecked((int)0x8009400B);

		[Description("The request is incorrectly formatted. The encrypted private key must be in an unauthenticated attribute in an outermost signature.")]
		public const int CERTSRV_E_BAD_REQUEST_KEY_ARCHIVAL = unchecked((int)0x8009400C);

		[Description("At least one security principal must have the permission to manage this CA.")]
		public const int CERTSRV_E_NO_CAADMIN_DEFINED = unchecked((int)0x8009400D);

		[Description("The request contains an invalid renewal certificate attribute.")]
		public const int CERTSRV_E_BAD_RENEWAL_CERT_ATTRIBUTE = unchecked((int)0x8009400E);

		[Description("An attempt was made to open a CA database session, but there are already too many active sessions. The server needs to be configured to allow additional sessions.")]
		public const int CERTSRV_E_NO_DB_SESSIONS = unchecked((int)0x8009400F);

		[Description("A memory reference caused a data alignment fault.")]
		public const int CERTSRV_E_ALIGNMENT_FAULT = unchecked((int)0x80094010);

		[Description("The permissions on this CA do not allow the current user to enroll for certificates.")]
		public const int CERTSRV_E_ENROLL_DENIED = unchecked((int)0x80094011);

		[Description("The permissions on the certificate template do not allow the current user to enroll for this type of certificate.")]
		public const int CERTSRV_E_TEMPLATE_DENIED = unchecked((int)0x80094012);

		[Description("The contacted domain controller cannot support signed Lightweight Directory Access Protocol (LDAP) traffic. Update the domain controller or configure Certificate Services to use SSL for Active Directory access.")]
		public const int CERTSRV_E_DOWNLEVEL_DC_SSL_OR_UPGRADE = unchecked((int)0x80094013);

		[Description("The requested certificate template is not supported by this CA.")]
		public const int CERTSRV_E_UNSUPPORTED_CERT_TYPE = unchecked((int)0x80094800);

		[Description("The request contains no certificate template information.")]
		public const int CERTSRV_E_NO_CERT_TYPE = unchecked((int)0x80094801);

		[Description("The request contains conflicting template information.")]
		public const int CERTSRV_E_TEMPLATE_CONFLICT = unchecked((int)0x80094802);

		[Description("The request is missing a required Subject Alternate name extension.")]
		public const int CERTSRV_E_SUBJECT_ALT_NAME_REQUIRED = unchecked((int)0x80094803);

		[Description("The request is missing a required private key for archival by the server.")]
		public const int CERTSRV_E_ARCHIVED_KEY_REQUIRED = unchecked((int)0x80094804);

		[Description("The request is missing a required SMIME capabilities extension.")]
		public const int CERTSRV_E_SMIME_REQUIRED = unchecked((int)0x80094805);

		[Description("The request was made on behalf of a subject other than the caller. The certificate template must be configured to require at least one signature to authorize the request.")]
		public const int CERTSRV_E_BAD_RENEWAL_SUBJECT = unchecked((int)0x80094806);

		[Description("The request template version is newer than the supported template version.")]
		public const int CERTSRV_E_BAD_TEMPLATE_VERSION = unchecked((int)0x80094807);

		[Description("The template is missing a required signature policy attribute.")]
		public const int CERTSRV_E_TEMPLATE_POLICY_REQUIRED = unchecked((int)0x80094808);

		[Description("The request is missing required signature policy information.")]
		public const int CERTSRV_E_SIGNATURE_POLICY_REQUIRED = unchecked((int)0x80094809);

		[Description("The request is missing one or more required signatures.")]
		public const int CERTSRV_E_SIGNATURE_COUNT = unchecked((int)0x8009480A);

		[Description("One or more signatures did not include the required application or issuance policies. The request is missing one or more required valid signatures.")]
		public const int CERTSRV_E_SIGNATURE_REJECTED = unchecked((int)0x8009480B);

		[Description("The request is missing one or more required signature issuance policies.")]
		public const int CERTSRV_E_ISSUANCE_POLICY_REQUIRED = unchecked((int)0x8009480C);

		[Description("The UPN is unavailable and cannot be added to the Subject Alternate name.")]
		public const int CERTSRV_E_SUBJECT_UPN_REQUIRED = unchecked((int)0x8009480D);

		[Description("The Active Directory GUID is unavailable and cannot be added to the Subject Alternate name.")]
		public const int CERTSRV_E_SUBJECT_DIRECTORY_GUID_REQUIRED = unchecked((int)0x8009480E);

		[Description("The Domain Name System (DNS) name is unavailable and cannot be added to the Subject Alternate name.")]
		public const int CERTSRV_E_SUBJECT_DNS_REQUIRED = unchecked((int)0x8009480F);

		[Description("The request includes a private key for archival by the server, but key archival is not enabled for the specified certificate template.")]
		public const int CERTSRV_E_ARCHIVED_KEY_UNEXPECTED = unchecked((int)0x80094810);

		[Description("The public key does not meet the minimum size required by the specified certificate template.")]
		public const int CERTSRV_E_KEY_LENGTH = unchecked((int)0x80094811);

		[Description("The email name is unavailable and cannot be added to the Subject or Subject Alternate name.")]
		public const int CERTSRV_E_SUBJECT_EMAIL_REQUIRED = unchecked((int)0x80094812);

		[Description("One or more certificate templates to be enabled on this CA could not be found.")]
		public const int CERTSRV_E_UNKNOWN_CERT_TYPE = unchecked((int)0x80094813);

		[Description("The certificate template renewal period is longer than the certificate validity period. The template should be reconfigured or the CA certificate renewed.")]
		public const int CERTSRV_E_CERT_TYPE_OVERLAP = unchecked((int)0x80094814);

		[Description("The certificate template requires too many return authorization (RA) signatures. Only one RA signature is allowed.")]
		public const int CERTSRV_E_TOO_MANY_SIGNATURES = unchecked((int)0x80094815);

		[Description("The key used in a renewal request does not match one of the certificates being renewed.")]
		public const int CERTSRV_E_RENEWAL_BAD_PUBLIC_KEY = unchecked((int)0x80094816);

		[Description("The endorsement key certificate is not valid.")]
		public const int CERTSRV_E_INVALID_EK = unchecked((int)0x80094817);

		[Description("Key attestation did not succeed.")]
		public const int CERTSRV_E_KEY_ATTESTATION = unchecked((int)0x8009481A);

		[Description("The key is not exportable.")]
		public const int XENROLL_E_KEY_NOT_EXPORTABLE = unchecked((int)0x80095000);

		[Description("You cannot add the root CA certificate into your local store.")]
		public const int XENROLL_E_CANNOT_ADD_ROOT_CERT = unchecked((int)0x80095001);

		[Description("The key archival hash attribute was not found in the response.")]
		public const int XENROLL_E_RESPONSE_KA_HASH_NOT_FOUND = unchecked((int)0x80095002);

		[Description("An unexpected key archival hash attribute was found in the response.")]
		public const int XENROLL_E_RESPONSE_UNEXPECTED_KA_HASH = unchecked((int)0x80095003);

		[Description("There is a key archival hash mismatch between the request and the response.")]
		public const int XENROLL_E_RESPONSE_KA_HASH_MISMATCH = unchecked((int)0x80095004);

		[Description("Signing certificate cannot include SMIME extension.")]
		public const int XENROLL_E_KEYSPEC_SMIME_MISMATCH = unchecked((int)0x80095005);

		[Description("A system-level error occurred while verifying trust.")]
		public const int TRUST_E_SYSTEM_ERROR = unchecked((int)0x80096001);

		[Description("The certificate for the signer of the message is invalid or not found.")]
		public const int TRUST_E_NO_SIGNER_CERT = unchecked((int)0x80096002);

		[Description("One of the counter signatures was invalid.")]
		public const int TRUST_E_COUNTER_SIGNER = unchecked((int)0x80096003);

		[Description("The signature of the certificate cannot be verified.")]
		public const int TRUST_E_CERT_SIGNATURE = unchecked((int)0x80096004);

		[Description("The time-stamp signature or certificate could not be verified or is malformed.")]
		public const int TRUST_E_TIME_STAMP = unchecked((int)0x80096005);

		[Description("The digital signature of the object did not verify.")]
		public const int TRUST_E_BAD_DIGEST = unchecked((int)0x80096010);

		[Description("A certificate's basic constraint extension has not been observed.")]
		public const int TRUST_E_BASIC_CONSTRAINTS = unchecked((int)0x80096019);

		[Description("The certificate does not meet or contain the Authenticode financial extensions.")]
		public const int TRUST_E_FINANCIAL_CRITERIA = unchecked((int)0x8009601E);

		[Description("Tried to reference a part of the file outside the proper range.")]
		public const int MSSIPOTF_E_OUTOFMEMRANGE = unchecked((int)0x80097001);

		[Description("Could not retrieve an object from the file.")]
		public const int MSSIPOTF_E_CANTGETOBJECT = unchecked((int)0x80097002);

		[Description("Could not find the head table in the file.")]
		public const int MSSIPOTF_E_NOHEADTABLE = unchecked((int)0x80097003);

		[Description("The magic number in the head table is incorrect.")]
		public const int MSSIPOTF_E_BAD_MAGICNUMBER = unchecked((int)0x80097004);

		[Description("The offset table has incorrect values.")]
		public const int MSSIPOTF_E_BAD_OFFSET_TABLE = unchecked((int)0x80097005);

		[Description("Duplicate table tags or the tags are out of alphabetical order.")]
		public const int MSSIPOTF_E_TABLE_TAGORDER = unchecked((int)0x80097006);

		[Description("A table does not start on a long word boundary.")]
		public const int MSSIPOTF_E_TABLE_LONGWORD = unchecked((int)0x80097007);

		[Description("First table does not appear after header information.")]
		public const int MSSIPOTF_E_BAD_FIRST_TABLE_PLACEMENT = unchecked((int)0x80097008);

		[Description("Two or more tables overlap.")]
		public const int MSSIPOTF_E_TABLES_OVERLAP = unchecked((int)0x80097009);

		[Description("Too many pad bytes between tables, or pad bytes are not 0.")]
		public const int MSSIPOTF_E_TABLE_PADBYTES = unchecked((int)0x8009700A);

		[Description("File is too small to contain the last table.")]
		public const int MSSIPOTF_E_FILETOOSMALL = unchecked((int)0x8009700B);

		[Description("A table checksum is incorrect.")]
		public const int MSSIPOTF_E_TABLE_CHECKSUM = unchecked((int)0x8009700C);

		[Description("The file checksum is incorrect.")]
		public const int MSSIPOTF_E_FILE_CHECKSUM = unchecked((int)0x8009700D);

		[Description("The signature does not have the correct attributes for the policy.")]
		public const int MSSIPOTF_E_FAILED_POLICY = unchecked((int)0x80097010);

		[Description("The file did not pass the hints check.")]
		public const int MSSIPOTF_E_FAILED_HINTS_CHECK = unchecked((int)0x80097011);

		[Description("The file is not an OpenType file.")]
		public const int MSSIPOTF_E_NOT_OPENTYPE = unchecked((int)0x80097012);

		[Description("Failed on a file operation (such as open, map, read, or write).")]
		public const int MSSIPOTF_E_FILE = unchecked((int)0x80097013);

		[Description("A call to a CryptoAPI function failed.")]
		public const int MSSIPOTF_E_CRYPT = unchecked((int)0x80097014);

		[Description("There is a bad version number in the file.")]
		public const int MSSIPOTF_E_BADVERSION = unchecked((int)0x80097015);

		[Description("The structure of the DSIG table is incorrect.")]
		public const int MSSIPOTF_E_DSIG_STRUCTURE = unchecked((int)0x80097016);

		[Description("A check failed in a partially constant table.")]
		public const int MSSIPOTF_E_PCONST_CHECK = unchecked((int)0x80097017);

		[Description("Some kind of structural error.")]
		public const int MSSIPOTF_E_STRUCTURE = unchecked((int)0x80097018);

		[Description("The requested credential requires confirmation.")]
		public const int ERROR_CRED_REQUIRES_CONFIRMATION = unchecked((int)0x80097019);

		[Description("Unknown trust provider.")]
		public const int TRUST_E_PROVIDER_UNKNOWN = unchecked((int)0x800B0001);

		[Description("The trust verification action specified is not supported by the specified trust provider.")]
		public const int TRUST_E_ACTION_UNKNOWN = unchecked((int)0x800B0002);

		[Description("The form specified for the subject is not one supported or known by the specified trust provider.")]
		public const int TRUST_E_SUBJECT_FORM_UNKNOWN = unchecked((int)0x800B0003);

		[Description("The subject is not trusted for the specified action.")]
		public const int TRUST_E_SUBJECT_NOT_TRUSTED = unchecked((int)0x800B0004);

		[Description("Error due to problem in ASN.1 encoding process.")]
		public const int DIGSIG_E_ENCODE = unchecked((int)0x800B0005);

		[Description("Error due to problem in ASN.1 decoding process.")]
		public const int DIGSIG_E_DECODE = unchecked((int)0x800B0006);

		[Description("Reading/writing extensions where attributes are appropriate, and vice versa.")]
		public const int DIGSIG_E_EXTENSIBILITY = unchecked((int)0x800B0007);

		[Description("Unspecified cryptographic failure.")]
		public const int DIGSIG_E_CRYPTO = unchecked((int)0x800B0008);

		[Description("The size of the data could not be determined.")]
		public const int PERSIST_E_SIZEDEFINITE = unchecked((int)0x800B0009);

		[Description("The size of the indefinite-sized data could not be determined.")]
		public const int PERSIST_E_SIZEINDEFINITE = unchecked((int)0x800B000A);

		[Description("This object does not read and write self-sizing data.")]
		public const int PERSIST_E_NOTSELFSIZING = unchecked((int)0x800B000B);

		[Description("No signature was present in the subject.")]
		public const int TRUST_E_NOSIGNATURE = unchecked((int)0x800B0100);

		[Description("A required certificate is not within its validity period when verifying against the current system clock or the time stamp in the signed file.")]
		public const int CERT_E_EXPIRED = unchecked((int)0x800B0101);

		[Description("The validity periods of the certification chain do not nest correctly.")]
		public const int CERT_E_VALIDITYPERIODNESTING = unchecked((int)0x800B0102);

		[Description("A certificate that can only be used as an end entity is being used as a CA or vice versa.")]
		public const int CERT_E_ROLE = unchecked((int)0x800B0103);

		[Description("A path length constraint in the certification chain has been violated.")]
		public const int CERT_E_PATHLENCONST = unchecked((int)0x800B0104);

		[Description("A certificate contains an unknown extension that is marked \"critical\".")]
		public const int CERT_E_CRITICAL = unchecked((int)0x800B0105);

		[Description("A certificate is being used for a purpose other than the ones specified by its CA.")]
		public const int CERT_E_PURPOSE = unchecked((int)0x800B0106);

		[Description("A parent of a given certificate did not issue that child certificate.")]
		public const int CERT_E_ISSUERCHAINING = unchecked((int)0x800B0107);

		[Description("A certificate is missing or has an empty value for an important field, such as a subject or issuer name.")]
		public const int CERT_E_MALFORMED = unchecked((int)0x800B0108);

		[Description("A certificate chain processed, but terminated in a root certificate that is not trusted by the trust provider.")]
		public const int CERT_E_UNTRUSTEDROOT = unchecked((int)0x800B0109);

		[Description("A certificate chain could not be built to a trusted root authority.")]
		public const int CERT_E_CHAINING = unchecked((int)0x800B010A);

		[Description("Generic trust failure.")]
		public const int TRUST_E_FAIL = unchecked((int)0x800B010B);

		[Description("A certificate was explicitly revoked by its issuer.")]
		public const int CERT_E_REVOKED = unchecked((int)0x800B010C);

		[Description("The certification path terminates with the test root that is not trusted with the current policy settings.")]
		public const int CERT_E_UNTRUSTEDTESTROOT = unchecked((int)0x800B010D);

		[Description("The revocation process could not continue—the certificates could not be checked.")]
		public const int CERT_E_REVOCATION_FAILURE = unchecked((int)0x800B010E);

		[Description("The certificate's CN name does not match the passed value.")]
		public const int CERT_E_CN_NO_MATCH = unchecked((int)0x800B010F);

		[Description("The certificate is not valid for the requested usage.")]
		public const int CERT_E_WRONG_USAGE = unchecked((int)0x800B0110);

		[Description("The certificate was explicitly marked as untrusted by the user.")]
		public const int TRUST_E_EXPLICIT_DISTRUST = unchecked((int)0x800B0111);

		[Description("A certification chain processed correctly, but one of the CA certificates is not trusted by the policy provider.")]
		public const int CERT_E_UNTRUSTEDCA = unchecked((int)0x800B0112);

		[Description("The certificate has invalid policy.")]
		public const int CERT_E_INVALID_POLICY = unchecked((int)0x800B0113);

		[Description("The certificate has an invalid name. The name is not included in the permitted list or is explicitly excluded.")]
		public const int CERT_E_INVALID_NAME = unchecked((int)0x800B0114);

		[Description("The maximum filebitrate value specified is greater than the server's configured maximum bandwidth.")]
		public const int NS_W_SERVER_BANDWIDTH_LIMIT = unchecked((int)0x800D0003);

		[Description("The maximum bandwidth value specified is less than the maximum filebitrate.")]
		public const int NS_W_FILE_BANDWIDTH_LIMIT = unchecked((int)0x800D0004);

		[Description("Unknown %1 event encountered.")]
		public const int NS_W_UNKNOWN_EVENT = unchecked((int)0x800D0060);

		[Description("Disk %1 ( %2 ) on Content Server %3, will be failed because it is catatonic.")]
		public const int NS_I_CATATONIC_FAILURE = unchecked((int)0x800D0199);

		[Description("Disk %1 ( %2 ) on Content Server %3, auto online from catatonic state.")]
		public const int NS_I_CATATONIC_AUTO_UNFAIL = unchecked((int)0x800D019A);

		[Description("A non-empty line was encountered in the INF before the start of a section.")]
		public const int SPAPI_E_EXPECTED_SECTION_NAME = unchecked((int)0x800F0000);

		[Description("A section name marker in the information file (INF) is not complete or does not exist on a line by itself.")]
		public const int SPAPI_E_BAD_SECTION_NAME_LINE = unchecked((int)0x800F0001);

		[Description("An INF section was encountered whose name exceeds the maximum section name length.")]
		public const int SPAPI_E_SECTION_NAME_TOO_LONG = unchecked((int)0x800F0002);

		[Description("The syntax of the INF is invalid.")]
		public const int SPAPI_E_GENERAL_SYNTAX = unchecked((int)0x800F0003);

		[Description("The style of the INF is different than what was requested.")]
		public const int SPAPI_E_WRONG_INF_STYLE = unchecked((int)0x800F0100);

		[Description("The required section was not found in the INF.")]
		public const int SPAPI_E_SECTION_NOT_FOUND = unchecked((int)0x800F0101);

		[Description("The required line was not found in the INF.")]
		public const int SPAPI_E_LINE_NOT_FOUND = unchecked((int)0x800F0102);

		[Description("The files affected by the installation of this file queue have not been backed up for uninstall.")]
		public const int SPAPI_E_NO_BACKUP = unchecked((int)0x800F0103);

		[Description("The INF or the device information set or element does not have an associated install class.")]
		public const int SPAPI_E_NO_ASSOCIATED_CLASS = unchecked((int)0x800F0200);

		[Description("The INF or the device information set or element does not match the specified install class.")]
		public const int SPAPI_E_CLASS_MISMATCH = unchecked((int)0x800F0201);

		[Description("An existing device was found that is a duplicate of the device being manually installed.")]
		public const int SPAPI_E_DUPLICATE_FOUND = unchecked((int)0x800F0202);

		[Description("There is no driver selected for the device information set or element.")]
		public const int SPAPI_E_NO_DRIVER_SELECTED = unchecked((int)0x800F0203);

		[Description("The requested device registry key does not exist.")]
		public const int SPAPI_E_KEY_DOES_NOT_EXIST = unchecked((int)0x800F0204);

		[Description("The device instance name is invalid.")]
		public const int SPAPI_E_INVALID_DEVINST_NAME = unchecked((int)0x800F0205);

		[Description("The install class is not present or is invalid.")]
		public const int SPAPI_E_INVALID_CLASS = unchecked((int)0x800F0206);

		[Description("The device instance cannot be created because it already exists.")]
		public const int SPAPI_E_DEVINST_ALREADY_EXISTS = unchecked((int)0x800F0207);

		[Description("The operation cannot be performed on a device information element that has not been registered.")]
		public const int SPAPI_E_DEVINFO_NOT_REGISTERED = unchecked((int)0x800F0208);

		[Description("The device property code is invalid.")]
		public const int SPAPI_E_INVALID_REG_PROPERTY = unchecked((int)0x800F0209);

		[Description("The INF from which a driver list is to be built does not exist.")]
		public const int SPAPI_E_NO_INF = unchecked((int)0x800F020A);

		[Description("The device instance does not exist in the hardware tree.")]
		public const int SPAPI_E_NO_SUCH_DEVINST = unchecked((int)0x800F020B);

		[Description("The icon representing this install class cannot be loaded.")]
		public const int SPAPI_E_CANT_LOAD_CLASS_ICON = unchecked((int)0x800F020C);

		[Description("The class installer registry entry is invalid.")]
		public const int SPAPI_E_INVALID_CLASS_INSTALLER = unchecked((int)0x800F020D);

		[Description("The class installer has indicated that the default action should be performed for this installation request.")]
		public const int SPAPI_E_DI_DO_DEFAULT = unchecked((int)0x800F020E);

		[Description("The operation does not require any files to be copied.")]
		public const int SPAPI_E_DI_NOFILECOPY = unchecked((int)0x800F020F);

		[Description("The specified hardware profile does not exist.")]
		public const int SPAPI_E_INVALID_HWPROFILE = unchecked((int)0x800F0210);

		[Description("There is no device information element currently selected for this device information set.")]
		public const int SPAPI_E_NO_DEVICE_SELECTED = unchecked((int)0x800F0211);

		[Description("The operation cannot be performed because the device information set is locked.")]
		public const int SPAPI_E_DEVINFO_LIST_LOCKED = unchecked((int)0x800F0212);

		[Description("The operation cannot be performed because the device information element is locked.")]
		public const int SPAPI_E_DEVINFO_DATA_LOCKED = unchecked((int)0x800F0213);

		[Description("The specified path does not contain any applicable device INFs.")]
		public const int SPAPI_E_DI_BAD_PATH = unchecked((int)0x800F0214);

		[Description("No class installer parameters have been set for the device information set or element.")]
		public const int SPAPI_E_NO_CLASSINSTALL_PARAMS = unchecked((int)0x800F0215);

		[Description("The operation cannot be performed because the file queue is locked.")]
		public const int SPAPI_E_FILEQUEUE_LOCKED = unchecked((int)0x800F0216);

		[Description("A service installation section in this INF is invalid.")]
		public const int SPAPI_E_BAD_SERVICE_INSTALLSECT = unchecked((int)0x800F0217);

		[Description("There is no class driver list for the device information element.")]
		public const int SPAPI_E_NO_CLASS_DRIVER_LIST = unchecked((int)0x800F0218);

		[Description("The installation failed because a function driver was not specified for this device instance.")]
		public const int SPAPI_E_NO_ASSOCIATED_SERVICE = unchecked((int)0x800F0219);

		[Description("There is presently no default device interface designated for this interface class.")]
		public const int SPAPI_E_NO_DEFAULT_DEVICE_INTERFACE = unchecked((int)0x800F021A);

		[Description("The operation cannot be performed because the device interface is currently active.")]
		public const int SPAPI_E_DEVICE_INTERFACE_ACTIVE = unchecked((int)0x800F021B);

		[Description("The operation cannot be performed because the device interface has been removed from the system.")]
		public const int SPAPI_E_DEVICE_INTERFACE_REMOVED = unchecked((int)0x800F021C);

		[Description("An interface installation section in this INF is invalid.")]
		public const int SPAPI_E_BAD_INTERFACE_INSTALLSECT = unchecked((int)0x800F021D);

		[Description("This interface class does not exist in the system.")]
		public const int SPAPI_E_NO_SUCH_INTERFACE_CLASS = unchecked((int)0x800F021E);

		[Description("The reference string supplied for this interface device is invalid.")]
		public const int SPAPI_E_INVALID_REFERENCE_STRING = unchecked((int)0x800F021F);

		[Description("The specified machine name does not conform to Universal Naming Convention (UNCs).")]
		public const int SPAPI_E_INVALID_MACHINENAME = unchecked((int)0x800F0220);

		[Description("A general remote communication error occurred.")]
		public const int SPAPI_E_REMOTE_COMM_FAILURE = unchecked((int)0x800F0221);

		[Description("The machine selected for remote communication is not available at this time.")]
		public const int SPAPI_E_MACHINE_UNAVAILABLE = unchecked((int)0x800F0222);

		[Description("The Plug and Play service is not available on the remote machine.")]
		public const int SPAPI_E_NO_CONFIGMGR_SERVICES = unchecked((int)0x800F0223);

		[Description("The property page provider registry entry is invalid.")]
		public const int SPAPI_E_INVALID_PROPPAGE_PROVIDER = unchecked((int)0x800F0224);

		[Description("The requested device interface is not present in the system.")]
		public const int SPAPI_E_NO_SUCH_DEVICE_INTERFACE = unchecked((int)0x800F0225);

		[Description("The device's co-installer has additional work to perform after installation is complete.")]
		public const int SPAPI_E_DI_POSTPROCESSING_REQUIRED = unchecked((int)0x800F0226);

		[Description("The device's co-installer is invalid.")]
		public const int SPAPI_E_INVALID_COINSTALLER = unchecked((int)0x800F0227);

		[Description("There are no compatible drivers for this device.")]
		public const int SPAPI_E_NO_COMPAT_DRIVERS = unchecked((int)0x800F0228);

		[Description("There is no icon that represents this device or device type.")]
		public const int SPAPI_E_NO_DEVICE_ICON = unchecked((int)0x800F0229);

		[Description("A logical configuration specified in this INF is invalid.")]
		public const int SPAPI_E_INVALID_INF_LOGCONFIG = unchecked((int)0x800F022A);

		[Description("The class installer has denied the request to install or upgrade this device.")]
		public const int SPAPI_E_DI_DONT_INSTALL = unchecked((int)0x800F022B);

		[Description("One of the filter drivers installed for this device is invalid.")]
		public const int SPAPI_E_INVALID_FILTER_DRIVER = unchecked((int)0x800F022C);

		[Description("The driver selected for this device does not support Windows XP operating system.")]
		public const int SPAPI_E_NON_WINDOWS_NT_DRIVER = unchecked((int)0x800F022D);

		[Description("The driver selected for this device does not support Windows.")]
		public const int SPAPI_E_NON_WINDOWS_DRIVER = unchecked((int)0x800F022E);

		[Description("The third-party INF does not contain digital signature information.")]
		public const int SPAPI_E_NO_CATALOG_FOR_OEM_INF = unchecked((int)0x800F022F);

		[Description("An invalid attempt was made to use a device installation file queue for verification of digital signatures relative to other platforms.")]
		public const int SPAPI_E_DEVINSTALL_QUEUE_NONNATIVE = unchecked((int)0x800F0230);

		[Description("The device cannot be disabled.")]
		public const int SPAPI_E_NOT_DISABLEABLE = unchecked((int)0x800F0231);

		[Description("The device could not be dynamically removed.")]
		public const int SPAPI_E_CANT_REMOVE_DEVINST = unchecked((int)0x800F0232);

		[Description("Cannot copy to specified target.")]
		public const int SPAPI_E_INVALID_TARGET = unchecked((int)0x800F0233);

		[Description("Driver is not intended for this platform.")]
		public const int SPAPI_E_DRIVER_NONNATIVE = unchecked((int)0x800F0234);

		[Description("Operation not allowed in WOW64.")]
		public const int SPAPI_E_IN_WOW64 = unchecked((int)0x800F0235);

		[Description("The operation involving unsigned file copying was rolled back, so that a system restore point could be set.")]
		public const int SPAPI_E_SET_SYSTEM_RESTORE_POINT = unchecked((int)0x800F0236);

		[Description("An INF was copied into the Windows INF directory in an improper manner.")]
		public const int SPAPI_E_INCORRECTLY_COPIED_INF = unchecked((int)0x800F0237);

		[Description("The Security Configuration Editor (SCE) APIs have been disabled on this embedded product.")]
		public const int SPAPI_E_SCE_DISABLED = unchecked((int)0x800F0238);

		[Description("An unknown exception was encountered.")]
		public const int SPAPI_E_UNKNOWN_EXCEPTION = unchecked((int)0x800F0239);

		[Description("A problem was encountered when accessing the Plug and Play registry database.")]
		public const int SPAPI_E_PNP_REGISTRY_ERROR = unchecked((int)0x800F023A);

		[Description("The requested operation is not supported for a remote machine.")]
		public const int SPAPI_E_REMOTE_REQUEST_UNSUPPORTED = unchecked((int)0x800F023B);

		[Description("The specified file is not an installed original equipment manufacturer (OEM) INF.")]
		public const int SPAPI_E_NOT_AN_INSTALLED_OEM_INF = unchecked((int)0x800F023C);

		[Description("One or more devices are presently installed using the specified INF.")]
		public const int SPAPI_E_INF_IN_USE_BY_DEVICES = unchecked((int)0x800F023D);

		[Description("The requested device install operation is obsolete.")]
		public const int SPAPI_E_DI_FUNCTION_OBSOLETE = unchecked((int)0x800F023E);

		[Description("A file could not be verified because it does not have an associated catalog signed via Authenticode.")]
		public const int SPAPI_E_NO_AUTHENTICODE_CATALOG = unchecked((int)0x800F023F);

		[Description("Authenticode signature verification is not supported for the specified INF.")]
		public const int SPAPI_E_AUTHENTICODE_DISALLOWED = unchecked((int)0x800F0240);

		[Description("The INF was signed with an Authenticode catalog from a trusted publisher.")]
		public const int SPAPI_E_AUTHENTICODE_TRUSTED_PUBLISHER = unchecked((int)0x800F0241);

		[Description("The publisher of an Authenticode-signed catalog has not yet been established as trusted.")]
		public const int SPAPI_E_AUTHENTICODE_TRUST_NOT_ESTABLISHED = unchecked((int)0x800F0242);

		[Description("The publisher of an Authenticode-signed catalog was not established as trusted.")]
		public const int SPAPI_E_AUTHENTICODE_PUBLISHER_NOT_TRUSTED = unchecked((int)0x800F0243);

		[Description("The software was tested for compliance with Windows logo requirements on a different version of Windows and might not be compatible with this version.")]
		public const int SPAPI_E_SIGNATURE_OSATTRIBUTE_MISMATCH = unchecked((int)0x800F0244);

		[Description("The file can be validated only by a catalog signed via Authenticode.")]
		public const int SPAPI_E_ONLY_VALIDATE_VIA_AUTHENTICODE = unchecked((int)0x800F0245);

		[Description("One of the installers for this device cannot perform the installation at this time.")]
		public const int SPAPI_E_DEVICE_INSTALLER_NOT_READY = unchecked((int)0x800F0246);

		[Description("A problem was encountered while attempting to add the driver to the store.")]
		public const int SPAPI_E_DRIVER_STORE_ADD_FAILED = unchecked((int)0x800F0247);

		[Description("The installation of this device is forbidden by system policy. Contact your system administrator.")]
		public const int SPAPI_E_DEVICE_INSTALL_BLOCKED = unchecked((int)0x800F0248);

		[Description("The installation of this driver is forbidden by system policy. Contact your system administrator.")]
		public const int SPAPI_E_DRIVER_INSTALL_BLOCKED = unchecked((int)0x800F0249);

		[Description("The specified INF is the wrong type for this operation.")]
		public const int SPAPI_E_WRONG_INF_TYPE = unchecked((int)0x800F024A);

		[Description("The hash for the file is not present in the specified catalog file. The file is likely corrupt or the victim of tampering.")]
		public const int SPAPI_E_FILE_HASH_NOT_IN_CATALOG = unchecked((int)0x800F024B);

		[Description("A problem was encountered while attempting to delete the driver from the store.")]
		public const int SPAPI_E_DRIVER_STORE_DELETE_FAILED = unchecked((int)0x800F024C);

		[Description("An unrecoverable stack overflow was encountered.")]
		public const int SPAPI_E_UNRECOVERABLE_STACK_OVERFLOW = unchecked((int)0x800F0300);

		[Description("No installed components were detected.")]
		public const int SPAPI_E_ERROR_NOT_INSTALLED = unchecked((int)0x800F1000);

		[Description("An internal consistency check failed.")]
		public const int SCARD_F_INTERNAL_ERROR = unchecked((int)0x80100001);

		[Description("The action was canceled by an SCardCancel request.")]
		public const int SCARD_E_CANCELLED = unchecked((int)0x80100002);

		[Description("The supplied handle was invalid.")]
		public const int SCARD_E_INVALID_HANDLE = unchecked((int)0x80100003);

		[Description("One or more of the supplied parameters could not be properly interpreted.")]
		public const int SCARD_E_INVALID_PARAMETER = unchecked((int)0x80100004);

		[Description("Registry startup information is missing or invalid.")]
		public const int SCARD_E_INVALID_TARGET = unchecked((int)0x80100005);

		[Description("Not enough memory available to complete this command.")]
		public const int SCARD_E_NO_MEMORY = unchecked((int)0x80100006);

		[Description("An internal consistency timer has expired.")]
		public const int SCARD_F_WAITED_TOO_LONG = unchecked((int)0x80100007);

		[Description("The data buffer to receive returned data is too small for the returned data.")]
		public const int SCARD_E_INSUFFICIENT_BUFFER = unchecked((int)0x80100008);

		[Description("The specified reader name is not recognized.")]
		public const int SCARD_E_UNKNOWN_READER = unchecked((int)0x80100009);

		[Description("The user-specified time-out value has expired.")]
		public const int SCARD_E_TIMEOUT = unchecked((int)0x8010000A);

		[Description("The smart card cannot be accessed because of other connections outstanding.")]
		public const int SCARD_E_SHARING_VIOLATION = unchecked((int)0x8010000B);

		[Description("The operation requires a smart card, but no smart card is currently in the device.")]
		public const int SCARD_E_NO_SMARTCARD = unchecked((int)0x8010000C);

		[Description("The specified smart card name is not recognized.")]
		public const int SCARD_E_UNKNOWN_CARD = unchecked((int)0x8010000D);

		[Description("The system could not dispose of the media in the requested manner.")]
		public const int SCARD_E_CANT_DISPOSE = unchecked((int)0x8010000E);

		[Description("The requested protocols are incompatible with the protocol currently in use with the smart card.")]
		public const int SCARD_E_PROTO_MISMATCH = unchecked((int)0x8010000F);

		[Description("The reader or smart card is not ready to accept commands.")]
		public const int SCARD_E_NOT_READY = unchecked((int)0x80100010);

		[Description("One or more of the supplied parameters values could not be properly interpreted.")]
		public const int SCARD_E_INVALID_VALUE = unchecked((int)0x80100011);

		[Description("The action was canceled by the system, presumably to log off or shut down.")]
		public const int SCARD_E_SYSTEM_CANCELLED = unchecked((int)0x80100012);

		[Description("An internal communications error has been detected.")]
		public const int SCARD_F_COMM_ERROR = unchecked((int)0x80100013);

		[Description("An internal error has been detected, but the source is unknown.")]
		public const int SCARD_F_UNKNOWN_ERROR = unchecked((int)0x80100014);

		[Description("An automatic terminal recognition (ATR) obtained from the registry is not a valid ATR string.")]
		public const int SCARD_E_INVALID_ATR = unchecked((int)0x80100015);

		[Description("An attempt was made to end a nonexistent transaction.")]
		public const int SCARD_E_NOT_TRANSACTED = unchecked((int)0x80100016);

		[Description("The specified reader is not currently available for use.")]
		public const int SCARD_E_READER_UNAVAILABLE = unchecked((int)0x80100017);

		[Description("The operation has been aborted to allow the server application to exit.")]
		public const int SCARD_P_SHUTDOWN = unchecked((int)0x80100018);

		[Description("The peripheral component interconnect (PCI) Receive buffer was too small.")]
		public const int SCARD_E_PCI_TOO_SMALL = unchecked((int)0x80100019);

		[Description("The reader driver does not meet minimal requirements for support.")]
		public const int SCARD_E_READER_UNSUPPORTED = unchecked((int)0x8010001A);

		[Description("The reader driver did not produce a unique reader name.")]
		public const int SCARD_E_DUPLICATE_READER = unchecked((int)0x8010001B);

		[Description("The smart card does not meet minimal requirements for support.")]
		public const int SCARD_E_CARD_UNSUPPORTED = unchecked((int)0x8010001C);

		[Description("The smart card resource manager is not running.")]
		public const int SCARD_E_NO_SERVICE = unchecked((int)0x8010001D);

		[Description("The smart card resource manager has shut down.")]
		public const int SCARD_E_SERVICE_STOPPED = unchecked((int)0x8010001E);

		[Description("An unexpected card error has occurred.")]
		public const int SCARD_E_UNEXPECTED = unchecked((int)0x8010001F);

		[Description("No primary provider can be found for the smart card.")]
		public const int SCARD_E_ICC_INSTALLATION = unchecked((int)0x80100020);

		[Description("The requested order of object creation is not supported.")]
		public const int SCARD_E_ICC_CREATEORDER = unchecked((int)0x80100021);

		[Description("This smart card does not support the requested feature.")]
		public const int SCARD_E_UNSUPPORTED_FEATURE = unchecked((int)0x80100022);

		[Description("The identified directory does not exist in the smart card.")]
		public const int SCARD_E_DIR_NOT_FOUND = unchecked((int)0x80100023);

		[Description("The identified file does not exist in the smart card.")]
		public const int SCARD_E_FILE_NOT_FOUND = unchecked((int)0x80100024);

		[Description("The supplied path does not represent a smart card directory.")]
		public const int SCARD_E_NO_DIR = unchecked((int)0x80100025);

		[Description("The supplied path does not represent a smart card file.")]
		public const int SCARD_E_NO_FILE = unchecked((int)0x80100026);

		[Description("Access is denied to this file.")]
		public const int SCARD_E_NO_ACCESS = unchecked((int)0x80100027);

		[Description("The smart card does not have enough memory to store the information.")]
		public const int SCARD_E_WRITE_TOO_MANY = unchecked((int)0x80100028);

		[Description("There was an error trying to set the smart card file object pointer.")]
		public const int SCARD_E_BAD_SEEK = unchecked((int)0x80100029);

		[Description("The supplied PIN is incorrect.")]
		public const int SCARD_E_INVALID_CHV = unchecked((int)0x8010002A);

		[Description("An unrecognized error code was returned from a layered component.")]
		public const int SCARD_E_UNKNOWN_RES_MNG = unchecked((int)0x8010002B);

		[Description("The requested certificate does not exist.")]
		public const int SCARD_E_NO_SUCH_CERTIFICATE = unchecked((int)0x8010002C);

		[Description("The requested certificate could not be obtained.")]
		public const int SCARD_E_CERTIFICATE_UNAVAILABLE = unchecked((int)0x8010002D);

		[Description("Cannot find a smart card reader.")]
		public const int SCARD_E_NO_READERS_AVAILABLE = unchecked((int)0x8010002E);

		[Description("A communications error with the smart card has been detected. Retry the operation.")]
		public const int SCARD_E_COMM_DATA_LOST = unchecked((int)0x8010002F);

		[Description("The requested key container does not exist on the smart card.")]
		public const int SCARD_E_NO_KEY_CONTAINER = unchecked((int)0x80100030);

		[Description("The smart card resource manager is too busy to complete this operation.")]
		public const int SCARD_E_SERVER_TOO_BUSY = unchecked((int)0x80100031);

		[Description("The reader cannot communicate with the smart card, due to ATR configuration conflicts.")]
		public const int SCARD_W_UNSUPPORTED_CARD = unchecked((int)0x80100065);

		[Description("The smart card is not responding to a reset.")]
		public const int SCARD_W_UNRESPONSIVE_CARD = unchecked((int)0x80100066);

		[Description("Power has been removed from the smart card, so that further communication is not possible.")]
		public const int SCARD_W_UNPOWERED_CARD = unchecked((int)0x80100067);

		[Description("The smart card has been reset, so any shared state information is invalid.")]
		public const int SCARD_W_RESET_CARD = unchecked((int)0x80100068);

		[Description("The smart card has been removed, so that further communication is not possible.")]
		public const int SCARD_W_REMOVED_CARD = unchecked((int)0x80100069);

		[Description("Access was denied because of a security violation.")]
		public const int SCARD_W_SECURITY_VIOLATION = unchecked((int)0x8010006A);

		[Description("The card cannot be accessed because the wrong PIN was presented.")]
		public const int SCARD_W_WRONG_CHV = unchecked((int)0x8010006B);

		[Description("The card cannot be accessed because the maximum number of PIN entry attempts has been reached.")]
		public const int SCARD_W_CHV_BLOCKED = unchecked((int)0x8010006C);

		[Description("The end of the smart card file has been reached.")]
		public const int SCARD_W_EOF = unchecked((int)0x8010006D);

		[Description("The action was canceled by the user.")]
		public const int SCARD_W_CANCELLED_BY_USER = unchecked((int)0x8010006E);

		[Description("No PIN was presented to the smart card.")]
		public const int SCARD_W_CARD_NOT_AUTHENTICATED = unchecked((int)0x8010006F);

		[Description("Errors occurred accessing one or more objects—the ErrorInfo collection contains more detail.")]
		public const int COMADMIN_E_OBJECTERRORS = unchecked((int)0x80110401);

		[Description("One or more of the object's properties are missing or invalid.")]
		public const int COMADMIN_E_OBJECTINVALID = unchecked((int)0x80110402);

		[Description("The object was not found in the catalog.")]
		public const int COMADMIN_E_KEYMISSING = unchecked((int)0x80110403);

		[Description("The object is already registered.")]
		public const int COMADMIN_E_ALREADYINSTALLED = unchecked((int)0x80110404);

		[Description("An error occurred writing to the application file.")]
		public const int COMADMIN_E_APP_FILE_WRITEFAIL = unchecked((int)0x80110407);

		[Description("An error occurred reading the application file.")]
		public const int COMADMIN_E_APP_FILE_READFAIL = unchecked((int)0x80110408);

		[Description("Invalid version number in application file.")]
		public const int COMADMIN_E_APP_FILE_VERSION = unchecked((int)0x80110409);

		[Description("The file path is invalid.")]
		public const int COMADMIN_E_BADPATH = unchecked((int)0x8011040A);

		[Description("The application is already installed.")]
		public const int COMADMIN_E_APPLICATIONEXISTS = unchecked((int)0x8011040B);

		[Description("The role already exists.")]
		public const int COMADMIN_E_ROLEEXISTS = unchecked((int)0x8011040C);

		[Description("An error occurred copying the file.")]
		public const int COMADMIN_E_CANTCOPYFILE = unchecked((int)0x8011040D);

		[Description("One or more users are not valid.")]
		public const int COMADMIN_E_NOUSER = unchecked((int)0x8011040F);

		[Description("One or more users in the application file are not valid.")]
		public const int COMADMIN_E_INVALIDUSERIDS = unchecked((int)0x80110410);

		[Description("The component's CLSID is missing or corrupt.")]
		public const int COMADMIN_E_NOREGISTRYCLSID = unchecked((int)0x80110411);

		[Description("The component's programmatic ID is missing or corrupt.")]
		public const int COMADMIN_E_BADREGISTRYPROGID = unchecked((int)0x80110412);

		[Description("Unable to set required authentication level for update request.")]
		public const int COMADMIN_E_AUTHENTICATIONLEVEL = unchecked((int)0x80110413);

		[Description("The identity or password set on the application is not valid.")]
		public const int COMADMIN_E_USERPASSWDNOTVALID = unchecked((int)0x80110414);

		[Description("Application file CLSIDs or instance identifiers (IIDs) do not match corresponding DLLs.")]
		public const int COMADMIN_E_CLSIDORIIDMISMATCH = unchecked((int)0x80110418);

		[Description("Interface information is either missing or changed.")]
		public const int COMADMIN_E_REMOTEINTERFACE = unchecked((int)0x80110419);

		[Description("DllRegisterServer failed on component install.")]
		public const int COMADMIN_E_DLLREGISTERSERVER = unchecked((int)0x8011041A);

		[Description("No server file share available.")]
		public const int COMADMIN_E_NOSERVERSHARE = unchecked((int)0x8011041B);

		[Description("DLL could not be loaded.")]
		public const int COMADMIN_E_DLLLOADFAILED = unchecked((int)0x8011041D);

		[Description("The registered TypeLib ID is not valid.")]
		public const int COMADMIN_E_BADREGISTRYLIBID = unchecked((int)0x8011041E);

		[Description("Application install directory not found.")]
		public const int COMADMIN_E_APPDIRNOTFOUND = unchecked((int)0x8011041F);

		[Description("Errors occurred while in the component registrar.")]
		public const int COMADMIN_E_REGISTRARFAILED = unchecked((int)0x80110423);

		[Description("The file does not exist.")]
		public const int COMADMIN_E_COMPFILE_DOESNOTEXIST = unchecked((int)0x80110424);

		[Description("The DLL could not be loaded.")]
		public const int COMADMIN_E_COMPFILE_LOADDLLFAIL = unchecked((int)0x80110425);

		[Description("GetClassObject failed in the DLL.")]
		public const int COMADMIN_E_COMPFILE_GETCLASSOBJ = unchecked((int)0x80110426);

		[Description("The DLL does not support the components listed in the TypeLib.")]
		public const int COMADMIN_E_COMPFILE_CLASSNOTAVAIL = unchecked((int)0x80110427);

		[Description("The TypeLib could not be loaded.")]
		public const int COMADMIN_E_COMPFILE_BADTLB = unchecked((int)0x80110428);

		[Description("The file does not contain components or component information.")]
		public const int COMADMIN_E_COMPFILE_NOTINSTALLABLE = unchecked((int)0x80110429);

		[Description("Changes to this object and its subobjects have been disabled.")]
		public const int COMADMIN_E_NOTCHANGEABLE = unchecked((int)0x8011042A);

		[Description("The delete function has been disabled for this object.")]
		public const int COMADMIN_E_NOTDELETEABLE = unchecked((int)0x8011042B);

		[Description("The server catalog version is not supported.")]
		public const int COMADMIN_E_SESSION = unchecked((int)0x8011042C);

		[Description("The component move was disallowed because the source or destination application is either a system application or currently locked against changes.")]
		public const int COMADMIN_E_COMP_MOVE_LOCKED = unchecked((int)0x8011042D);

		[Description("The component move failed because the destination application no longer exists.")]
		public const int COMADMIN_E_COMP_MOVE_BAD_DEST = unchecked((int)0x8011042E);

		[Description("The system was unable to register the TypeLib.")]
		public const int COMADMIN_E_REGISTERTLB = unchecked((int)0x80110430);

		[Description("This operation cannot be performed on the system application.")]
		public const int COMADMIN_E_SYSTEMAPP = unchecked((int)0x80110433);

		[Description("The component registrar referenced in this file is not available.")]
		public const int COMADMIN_E_COMPFILE_NOREGISTRAR = unchecked((int)0x80110434);

		[Description("A component in the same DLL is already installed.")]
		public const int COMADMIN_E_COREQCOMPINSTALLED = unchecked((int)0x80110435);

		[Description("The service is not installed.")]
		public const int COMADMIN_E_SERVICENOTINSTALLED = unchecked((int)0x80110436);

		[Description("One or more property settings are either invalid or in conflict with each other.")]
		public const int COMADMIN_E_PROPERTYSAVEFAILED = unchecked((int)0x80110437);

		[Description("The object you are attempting to add or rename already exists.")]
		public const int COMADMIN_E_OBJECTEXISTS = unchecked((int)0x80110438);

		[Description("The component already exists.")]
		public const int COMADMIN_E_COMPONENTEXISTS = unchecked((int)0x80110439);

		[Description("The registration file is corrupt.")]
		public const int COMADMIN_E_REGFILE_CORRUPT = unchecked((int)0x8011043B);

		[Description("The property value is too large.")]
		public const int COMADMIN_E_PROPERTY_OVERFLOW = unchecked((int)0x8011043C);

		[Description("Object was not found in registry.")]
		public const int COMADMIN_E_NOTINREGISTRY = unchecked((int)0x8011043E);

		[Description("This object cannot be pooled.")]
		public const int COMADMIN_E_OBJECTNOTPOOLABLE = unchecked((int)0x8011043F);

		[Description("A CLSID with the same GUID as the new application ID is already installed on this machine.")]
		public const int COMADMIN_E_APPLID_MATCHES_CLSID = unchecked((int)0x80110446);

		[Description("A role assigned to a component, interface, or method did not exist in the application.")]
		public const int COMADMIN_E_ROLE_DOES_NOT_EXIST = unchecked((int)0x80110447);

		[Description("You must have components in an application to start the application.")]
		public const int COMADMIN_E_START_APP_NEEDS_COMPONENTS = unchecked((int)0x80110448);

		[Description("This operation is not enabled on this platform.")]
		public const int COMADMIN_E_REQUIRES_DIFFERENT_PLATFORM = unchecked((int)0x80110449);

		[Description("Application proxy is not exportable.")]
		public const int COMADMIN_E_CAN_NOT_EXPORT_APP_PROXY = unchecked((int)0x8011044A);

		[Description("Failed to start application because it is either a library application or an application proxy.")]
		public const int COMADMIN_E_CAN_NOT_START_APP = unchecked((int)0x8011044B);

		[Description("System application is not exportable.")]
		public const int COMADMIN_E_CAN_NOT_EXPORT_SYS_APP = unchecked((int)0x8011044C);

		[Description("Cannot subscribe to this component (the component might have been imported).")]
		public const int COMADMIN_E_CANT_SUBSCRIBE_TO_COMPONENT = unchecked((int)0x8011044D);

		[Description("An event class cannot also be a subscriber component.")]
		public const int COMADMIN_E_EVENTCLASS_CANT_BE_SUBSCRIBER = unchecked((int)0x8011044E);

		[Description("Library applications and application proxies are incompatible.")]
		public const int COMADMIN_E_LIB_APP_PROXY_INCOMPATIBLE = unchecked((int)0x8011044F);

		[Description("This function is valid for the base partition only.")]
		public const int COMADMIN_E_BASE_PARTITION_ONLY = unchecked((int)0x80110450);

		[Description("You cannot start an application that has been disabled.")]
		public const int COMADMIN_E_START_APP_DISABLED = unchecked((int)0x80110451);

		[Description("The specified partition name is already in use on this computer.")]
		public const int COMADMIN_E_CAT_DUPLICATE_PARTITION_NAME = unchecked((int)0x80110457);

		[Description("The specified partition name is invalid. Check that the name contains at least one visible character.")]
		public const int COMADMIN_E_CAT_INVALID_PARTITION_NAME = unchecked((int)0x80110458);

		[Description("The partition cannot be deleted because it is the default partition for one or more users.")]
		public const int COMADMIN_E_CAT_PARTITION_IN_USE = unchecked((int)0x80110459);

		[Description("The partition cannot be exported because one or more components in the partition have the same file name.")]
		public const int COMADMIN_E_FILE_PARTITION_DUPLICATE_FILES = unchecked((int)0x8011045A);

		[Description("Applications that contain one or more imported components cannot be installed into a nonbase partition.")]
		public const int COMADMIN_E_CAT_IMPORTED_COMPONENTS_NOT_ALLOWED = unchecked((int)0x8011045B);

		[Description("The application name is not unique and cannot be resolved to an application ID.")]
		public const int COMADMIN_E_AMBIGUOUS_APPLICATION_NAME = unchecked((int)0x8011045C);

		[Description("The partition name is not unique and cannot be resolved to a partition ID.")]
		public const int COMADMIN_E_AMBIGUOUS_PARTITION_NAME = unchecked((int)0x8011045D);

		[Description("The COM+ registry database has not been initialized.")]
		public const int COMADMIN_E_REGDB_NOTINITIALIZED = unchecked((int)0x80110472);

		[Description("The COM+ registry database is not open.")]
		public const int COMADMIN_E_REGDB_NOTOPEN = unchecked((int)0x80110473);

		[Description("The COM+ registry database detected a system error.")]
		public const int COMADMIN_E_REGDB_SYSTEMERR = unchecked((int)0x80110474);

		[Description("The COM+ registry database is already running.")]
		public const int COMADMIN_E_REGDB_ALREADYRUNNING = unchecked((int)0x80110475);

		[Description("This version of the COM+ registry database cannot be migrated.")]
		public const int COMADMIN_E_MIG_VERSIONNOTSUPPORTED = unchecked((int)0x80110480);

		[Description("The schema version to be migrated could not be found in the COM+ registry database.")]
		public const int COMADMIN_E_MIG_SCHEMANOTFOUND = unchecked((int)0x80110481);

		[Description("There was a type mismatch between binaries.")]
		public const int COMADMIN_E_CAT_BITNESSMISMATCH = unchecked((int)0x80110482);

		[Description("A binary of unknown or invalid type was provided.")]
		public const int COMADMIN_E_CAT_UNACCEPTABLEBITNESS = unchecked((int)0x80110483);

		[Description("There was a type mismatch between a binary and an application.")]
		public const int COMADMIN_E_CAT_WRONGAPPBITNESS = unchecked((int)0x80110484);

		[Description("The application cannot be paused or resumed.")]
		public const int COMADMIN_E_CAT_PAUSE_RESUME_NOT_SUPPORTED = unchecked((int)0x80110485);

		[Description("The COM+ catalog server threw an exception during execution.")]
		public const int COMADMIN_E_CAT_SERVERFAULT = unchecked((int)0x80110486);

		[Description("Only COM+ applications marked \"queued\" can be invoked using the \"queue\" moniker.")]
		public const int COMQC_E_APPLICATION_NOT_QUEUED = unchecked((int)0x80110600);

		[Description("At least one interface must be marked \"queued\" to create a queued component instance with the \"queue\" moniker.")]
		public const int COMQC_E_NO_QUEUEABLE_INTERFACES = unchecked((int)0x80110601);

		[Description("Message Queuing is required for the requested operation and is not installed.")]
		public const int COMQC_E_QUEUING_SERVICE_NOT_AVAILABLE = unchecked((int)0x80110602);

		[Description("Unable to marshal an interface that does not support IPersistStream.")]
		public const int COMQC_E_NO_IPERSISTSTREAM = unchecked((int)0x80110603);

		[Description("The message is improperly formatted or was damaged in transit.")]
		public const int COMQC_E_BAD_MESSAGE = unchecked((int)0x80110604);

		[Description("An unauthenticated message was received by an application that accepts only authenticated messages.")]
		public const int COMQC_E_UNAUTHENTICATED = unchecked((int)0x80110605);

		[Description("The message was requeued or moved by a user not in the QC Trusted User \"role\".")]
		public const int COMQC_E_UNTRUSTED_ENQUEUER = unchecked((int)0x80110606);

		[Description("Cannot create a duplicate resource of type Distributed Transaction Coordinator.")]
		public const int MSDTC_E_DUPLICATE_RESOURCE = unchecked((int)0x80110701);

		[Description("One of the objects being inserted or updated does not belong to a valid parent collection.")]
		public const int COMADMIN_E_OBJECT_PARENT_MISSING = unchecked((int)0x80110808);

		[Description("One of the specified objects cannot be found.")]
		public const int COMADMIN_E_OBJECT_DOES_NOT_EXIST = unchecked((int)0x80110809);

		[Description("The specified application is not currently running.")]
		public const int COMADMIN_E_APP_NOT_RUNNING = unchecked((int)0x8011080A);

		[Description("The partitions specified are not valid.")]
		public const int COMADMIN_E_INVALID_PARTITION = unchecked((int)0x8011080B);

		[Description("COM+ applications that run as Windows NT service cannot be pooled or recycled.")]
		public const int COMADMIN_E_SVCAPP_NOT_POOLABLE_OR_RECYCLABLE = unchecked((int)0x8011080D);

		[Description("One or more users are already assigned to a local partition set.")]
		public const int COMADMIN_E_USER_IN_SET = unchecked((int)0x8011080E);

		[Description("Library applications cannot be recycled.")]
		public const int COMADMIN_E_CANTRECYCLELIBRARYAPPS = unchecked((int)0x8011080F);

		[Description("Applications running as Windows NT services cannot be recycled.")]
		public const int COMADMIN_E_CANTRECYCLESERVICEAPPS = unchecked((int)0x80110811);

		[Description("The process has already been recycled.")]
		public const int COMADMIN_E_PROCESSALREADYRECYCLED = unchecked((int)0x80110812);

		[Description("A paused process cannot be recycled.")]
		public const int COMADMIN_E_PAUSEDPROCESSMAYNOTBERECYCLED = unchecked((int)0x80110813);

		[Description("Library applications cannot be Windows NT services.")]
		public const int COMADMIN_E_CANTMAKEINPROCSERVICE = unchecked((int)0x80110814);

		[Description("The ProgID provided to the copy operation is invalid. The ProgID is in use by another registered CLSID.")]
		public const int COMADMIN_E_PROGIDINUSEBYCLSID = unchecked((int)0x80110815);

		[Description("The partition specified as the default is not a member of the partition set.")]
		public const int COMADMIN_E_DEFAULT_PARTITION_NOT_IN_SET = unchecked((int)0x80110816);

		[Description("A recycled process cannot be paused.")]
		public const int COMADMIN_E_RECYCLEDPROCESSMAYNOTBEPAUSED = unchecked((int)0x80110817);

		[Description("Access to the specified partition is denied.")]
		public const int COMADMIN_E_PARTITION_ACCESSDENIED = unchecked((int)0x80110818);

		[Description("Only application files (*.msi files) can be installed into partitions.")]
		public const int COMADMIN_E_PARTITION_MSI_ONLY = unchecked((int)0x80110819);

		[Description("Applications containing one or more legacy components cannot be exported to 1.0 format.")]
		public const int COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_1_0_FORMAT = unchecked((int)0x8011081A);

		[Description("Legacy components cannot exist in nonbase partitions.")]
		public const int COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_NONBASE_PARTITIONS = unchecked((int)0x8011081B);

		[Description("A component cannot be moved (or copied) from the System Application, an application proxy, or a nonchangeable application.")]
		public const int COMADMIN_E_COMP_MOVE_SOURCE = unchecked((int)0x8011081C);

		[Description("A component cannot be moved (or copied) to the System Application, an application proxy or a nonchangeable application.")]
		public const int COMADMIN_E_COMP_MOVE_DEST = unchecked((int)0x8011081D);

		[Description("A private component cannot be moved (or copied) to a library application or to the base partition.")]
		public const int COMADMIN_E_COMP_MOVE_PRIVATE = unchecked((int)0x8011081E);

		[Description("The Base Application Partition exists in all partition sets and cannot be removed.")]
		public const int COMADMIN_E_BASEPARTITION_REQUIRED_IN_SET = unchecked((int)0x8011081F);

		[Description("Alas, Event Class components cannot be aliased.")]
		public const int COMADMIN_E_CANNOT_ALIAS_EVENTCLASS = unchecked((int)0x80110820);

		[Description("Access is denied because the component is private.")]
		public const int COMADMIN_E_PRIVATE_ACCESSDENIED = unchecked((int)0x80110821);

		[Description("The specified SAFER level is invalid.")]
		public const int COMADMIN_E_SAFERINVALID = unchecked((int)0x80110822);

		[Description("The specified user cannot write to the system registry.")]
		public const int COMADMIN_E_REGISTRY_ACCESSDENIED = unchecked((int)0x80110823);

		[Description("COM+ partitions are currently disabled.")]
		public const int COMADMIN_E_PARTITIONS_DISABLED = unchecked((int)0x80110824);

		[Description("A handler was not defined by the filter for this operation.")]
		public const int ERROR_FLT_NO_HANDLER_DEFINED = unchecked((int)0x801F0001);

		[Description("A context is already defined for this object.")]
		public const int ERROR_FLT_CONTEXT_ALREADY_DEFINED = unchecked((int)0x801F0002);

		[Description("Asynchronous requests are not valid for this operation.")]
		public const int ERROR_FLT_INVALID_ASYNCHRONOUS_REQUEST = unchecked((int)0x801F0003);

		[Description("Disallow the Fast IO path for this operation.")]
		public const int ERROR_FLT_DISALLOW_FAST_IO = unchecked((int)0x801F0004);

		[Description("An invalid name request was made. The name requested cannot be retrieved at this time.")]
		public const int ERROR_FLT_INVALID_NAME_REQUEST = unchecked((int)0x801F0005);

		[Description("Posting this operation to a worker thread for further processing is not safe at this time because it could lead to a system deadlock.")]
		public const int ERROR_FLT_NOT_SAFE_TO_POST_OPERATION = unchecked((int)0x801F0006);

		[Description("The Filter Manager was not initialized when a filter tried to register. Be sure that the Filter Manager is being loaded as a driver.")]
		public const int ERROR_FLT_NOT_INITIALIZED = unchecked((int)0x801F0007);

		[Description("The filter is not ready for attachment to volumes because it has not finished initializing (FltStartFiltering has not been called).")]
		public const int ERROR_FLT_FILTER_NOT_READY = unchecked((int)0x801F0008);

		[Description("The filter must clean up any operation-specific context at this time because it is being removed from the system before the operation is completed by the lower drivers.")]
		public const int ERROR_FLT_POST_OPERATION_CLEANUP = unchecked((int)0x801F0009);

		[Description("The Filter Manager had an internal error from which it cannot recover; therefore, the operation has been failed. This is usually the result of a filter returning an invalid value from a preoperation callback.")]
		public const int ERROR_FLT_INTERNAL_ERROR = unchecked((int)0x801F000A);

		[Description("The object specified for this action is in the process of being deleted; therefore, the action requested cannot be completed at this time.")]
		public const int ERROR_FLT_DELETING_OBJECT = unchecked((int)0x801F000B);

		[Description("Nonpaged pool must be used for this type of context.")]
		public const int ERROR_FLT_MUST_BE_NONPAGED_POOL = unchecked((int)0x801F000C);

		[Description("A duplicate handler definition has been provided for an operation.")]
		public const int ERROR_FLT_DUPLICATE_ENTRY = unchecked((int)0x801F000D);

		[Description("The callback data queue has been disabled.")]
		public const int ERROR_FLT_CBDQ_DISABLED = unchecked((int)0x801F000E);

		[Description("Do not attach the filter to the volume at this time.")]
		public const int ERROR_FLT_DO_NOT_ATTACH = unchecked((int)0x801F000F);

		[Description("Do not detach the filter from the volume at this time.")]
		public const int ERROR_FLT_DO_NOT_DETACH = unchecked((int)0x801F0010);

		[Description("An instance already exists at this altitude on the volume specified.")]
		public const int ERROR_FLT_INSTANCE_ALTITUDE_COLLISION = unchecked((int)0x801F0011);

		[Description("An instance already exists with this name on the volume specified.")]
		public const int ERROR_FLT_INSTANCE_NAME_COLLISION = unchecked((int)0x801F0012);

		[Description("The system could not find the filter specified.")]
		public const int ERROR_FLT_FILTER_NOT_FOUND = unchecked((int)0x801F0013);

		[Description("The system could not find the volume specified.")]
		public const int ERROR_FLT_VOLUME_NOT_FOUND = unchecked((int)0x801F0014);

		[Description("The system could not find the instance specified.")]
		public const int ERROR_FLT_INSTANCE_NOT_FOUND = unchecked((int)0x801F0015);

		[Description("No registered context allocation definition was found for the given request.")]
		public const int ERROR_FLT_CONTEXT_ALLOCATION_NOT_FOUND = unchecked((int)0x801F0016);

		[Description("An invalid parameter was specified during context registration.")]
		public const int ERROR_FLT_INVALID_CONTEXT_REGISTRATION = unchecked((int)0x801F0017);

		[Description("The name requested was not found in the Filter Manager name cache and could not be retrieved from the file system.")]
		public const int ERROR_FLT_NAME_CACHE_MISS = unchecked((int)0x801F0018);

		[Description("The requested device object does not exist for the given volume.")]
		public const int ERROR_FLT_NO_DEVICE_OBJECT = unchecked((int)0x801F0019);

		[Description("The specified volume is already mounted.")]
		public const int ERROR_FLT_VOLUME_ALREADY_MOUNTED = unchecked((int)0x801F001A);

		[Description("The specified Transaction Context is already enlisted in a transaction.")]
		public const int ERROR_FLT_ALREADY_ENLISTED = unchecked((int)0x801F001B);

		[Description("The specified context is already attached to another object.")]
		public const int ERROR_FLT_CONTEXT_ALREADY_LINKED = unchecked((int)0x801F001C);

		[Description("No waiter is present for the filter's reply to this message.")]
		public const int ERROR_FLT_NO_WAITER_FOR_REPLY = unchecked((int)0x801F0020);

		[Description("{Display Driver Stopped Responding} The %hs display driver has stopped working normally. Save your work and reboot the system to restore full display functionality. The next time you reboot the machine a dialog will be displayed giving you a chance to report this failure to Microsoft.")]
		public const int ERROR_HUNG_DISPLAY_DRIVER_THREAD = unchecked((int)0x80260001);

		[Description("Monitor descriptor could not be obtained.")]
		public const int ERROR_MONITOR_NO_DESCRIPTOR = unchecked((int)0x80261001);

		[Description("Format of the obtained monitor descriptor is not supported by this release.")]
		public const int ERROR_MONITOR_UNKNOWN_DESCRIPTOR_FORMAT = unchecked((int)0x80261002);

		[Description("{Desktop Composition is Disabled} The operation could not be completed because desktop composition is disabled.")]
		public const int DWM_E_COMPOSITIONDISABLED = unchecked((int)0x80263001);

		[Description("{Some Desktop Composition APIs Are Not Supported While Remoting} Some desktop composition APIs are not supported while remoting. The operation is not supported while running in a remote session.")]
		public const int DWM_E_REMOTING_NOT_SUPPORTED = unchecked((int)0x80263002);

		[Description("{No DWM Redirection Surface is Available} The Desktop Window Manager (DWM) was unable to provide a redirection surface to complete the DirectX present.")]
		public const int DWM_E_NO_REDIRECTION_SURFACE_AVAILABLE = unchecked((int)0x80263003);

		[Description("{DWM Is Not Queuing Presents for the Specified Window} The window specified is not currently using queued presents.")]
		public const int DWM_E_NOT_QUEUING_PRESENTS = unchecked((int)0x80263004);

		[Description("This is an error mask to convert Trusted Platform Module (TPM) hardware errors to Win32 errors.")]
		public const int TPM_E_ERROR_MASK = unchecked((int)0x80280000);

		[Description("Authentication failed.")]
		public const int TPM_E_AUTHFAIL = unchecked((int)0x80280001);

		[Description("The index to a Platform Configuration Register (PCR), DIR, or other register is incorrect.")]
		public const int TPM_E_BADINDEX = unchecked((int)0x80280002);

		[Description("One or more parameters are bad.")]
		public const int TPM_E_BAD_PARAMETER = unchecked((int)0x80280003);

		[Description("An operation completed successfully but the auditing of that operation failed.")]
		public const int TPM_E_AUDITFAILURE = unchecked((int)0x80280004);

		[Description("The clear disable flag is set and all clear operations now require physical access.")]
		public const int TPM_E_CLEAR_DISABLED = unchecked((int)0x80280005);

		[Description("The TPM is deactivated.")]
		public const int TPM_E_DEACTIVATED = unchecked((int)0x80280006);

		[Description("The TPM is disabled.")]
		public const int TPM_E_DISABLED = unchecked((int)0x80280007);

		[Description("The target command has been disabled.")]
		public const int TPM_E_DISABLED_CMD = unchecked((int)0x80280008);

		[Description("The operation failed.")]
		public const int TPM_E_FAIL = unchecked((int)0x80280009);

		[Description("The ordinal was unknown or inconsistent.")]
		public const int TPM_E_BAD_ORDINAL = unchecked((int)0x8028000A);

		[Description("The ability to install an owner is disabled.")]
		public const int TPM_E_INSTALL_DISABLED = unchecked((int)0x8028000B);

		[Description("The key handle cannot be interpreted.")]
		public const int TPM_E_INVALID_KEYHANDLE = unchecked((int)0x8028000C);

		[Description("The key handle points to an invalid key.")]
		public const int TPM_E_KEYNOTFOUND = unchecked((int)0x8028000D);

		[Description("Unacceptable encryption scheme.")]
		public const int TPM_E_INAPPROPRIATE_ENC = unchecked((int)0x8028000E);

		[Description("Migration authorization failed.")]
		public const int TPM_E_MIGRATEFAIL = unchecked((int)0x8028000F);

		[Description("PCR information could not be interpreted.")]
		public const int TPM_E_INVALID_PCR_INFO = unchecked((int)0x80280010);

		[Description("No room to load key.")]
		public const int TPM_E_NOSPACE = unchecked((int)0x80280011);

		[Description("There is no storage root key (SRK) set.")]
		public const int TPM_E_NOSRK = unchecked((int)0x80280012);

		[Description("An encrypted blob is invalid or was not created by this TPM.")]
		public const int TPM_E_NOTSEALED_BLOB = unchecked((int)0x80280013);

		[Description("There is already an owner.")]
		public const int TPM_E_OWNER_SET = unchecked((int)0x80280014);

		[Description("The TPM has insufficient internal resources to perform the requested action.")]
		public const int TPM_E_RESOURCES = unchecked((int)0x80280015);

		[Description("A random string was too short.")]
		public const int TPM_E_SHORTRANDOM = unchecked((int)0x80280016);

		[Description("The TPM does not have the space to perform the operation.")]
		public const int TPM_E_SIZE = unchecked((int)0x80280017);

		[Description("The named PCR value does not match the current PCR value.")]
		public const int TPM_E_WRONGPCRVAL = unchecked((int)0x80280018);

		[Description("The paramSize argument to the command has the incorrect value.")]
		public const int TPM_E_BAD_PARAM_SIZE = unchecked((int)0x80280019);

		[Description("There is no existing SHA-1 thread.")]
		public const int TPM_E_SHA_THREAD = unchecked((int)0x8028001A);

		[Description("The calculation is unable to proceed because the existing SHA-1 thread has already encountered an error.")]
		public const int TPM_E_SHA_ERROR = unchecked((int)0x8028001B);

		[Description("Self-test has failed and the TPM has shut down.")]
		public const int TPM_E_FAILEDSELFTEST = unchecked((int)0x8028001C);

		[Description("The authorization for the second key in a two-key function failed authorization.")]
		public const int TPM_E_AUTH2FAIL = unchecked((int)0x8028001D);

		[Description("The tag value sent to for a command is invalid.")]
		public const int TPM_E_BADTAG = unchecked((int)0x8028001E);

		[Description("An I/O error occurred transmitting information to the TPM.")]
		public const int TPM_E_IOERROR = unchecked((int)0x8028001F);

		[Description("The encryption process had a problem.")]
		public const int TPM_E_ENCRYPT_ERROR = unchecked((int)0x80280020);

		[Description("The decryption process did not complete.")]
		public const int TPM_E_DECRYPT_ERROR = unchecked((int)0x80280021);

		[Description("An invalid handle was used.")]
		public const int TPM_E_INVALID_AUTHHANDLE = unchecked((int)0x80280022);

		[Description("The TPM does not have an endorsement key (EK) installed.")]
		public const int TPM_E_NO_ENDORSEMENT = unchecked((int)0x80280023);

		[Description("The usage of a key is not allowed.")]
		public const int TPM_E_INVALID_KEYUSAGE = unchecked((int)0x80280024);

		[Description("The submitted entity type is not allowed.")]
		public const int TPM_E_WRONG_ENTITYTYPE = unchecked((int)0x80280025);

		[Description("The command was received in the wrong sequence relative to TPM_Init and a subsequent TPM_Startup.")]
		public const int TPM_E_INVALID_POSTINIT = unchecked((int)0x80280026);

		[Description("Signed data cannot include additional DER information.")]
		public const int TPM_E_INAPPROPRIATE_SIG = unchecked((int)0x80280027);

		[Description("The key properties in TPM_KEY_PARMs are not supported by this TPM.")]
		public const int TPM_E_BAD_KEY_PROPERTY = unchecked((int)0x80280028);

		[Description("The migration properties of this key are incorrect.")]
		public const int TPM_E_BAD_MIGRATION = unchecked((int)0x80280029);

		[Description("The signature or encryption scheme for this key is incorrect or not permitted in this situation.")]
		public const int TPM_E_BAD_SCHEME = unchecked((int)0x8028002A);

		[Description("The size of the data (or blob) parameter is bad or inconsistent with the referenced key.")]
		public const int TPM_E_BAD_DATASIZE = unchecked((int)0x8028002B);

		[Description("A mode parameter is bad, such as capArea or subCapArea for TPM_GetCapability, physicalPresence parameter for TPM_PhysicalPresence, or migrationType for TPM_CreateMigrationBlob.")]
		public const int TPM_E_BAD_MODE = unchecked((int)0x8028002C);

		[Description("Either the physicalPresence or physicalPresenceLock bits have the wrong value.")]
		public const int TPM_E_BAD_PRESENCE = unchecked((int)0x8028002D);

		[Description("The TPM cannot perform this version of the capability.")]
		public const int TPM_E_BAD_VERSION = unchecked((int)0x8028002E);

		[Description("The TPM does not allow for wrapped transport sessions.")]
		public const int TPM_E_NO_WRAP_TRANSPORT = unchecked((int)0x8028002F);

		[Description("TPM audit construction failed and the underlying command was returning a failure code also.")]
		public const int TPM_E_AUDITFAIL_UNSUCCESSFUL = unchecked((int)0x80280030);

		[Description("TPM audit construction failed and the underlying command was returning success.")]
		public const int TPM_E_AUDITFAIL_SUCCESSFUL = unchecked((int)0x80280031);

		[Description("Attempt to reset a PCR that does not have the resettable attribute.")]
		public const int TPM_E_NOTRESETABLE = unchecked((int)0x80280032);

		[Description("Attempt to reset a PCR register that requires locality and the locality modifier not part of command transport.")]
		public const int TPM_E_NOTLOCAL = unchecked((int)0x80280033);

		[Description("Make identity blob not properly typed.")]
		public const int TPM_E_BAD_TYPE = unchecked((int)0x80280034);

		[Description("When saving context identified resource type does not match actual resource.")]
		public const int TPM_E_INVALID_RESOURCE = unchecked((int)0x80280035);

		[Description("The TPM is attempting to execute a command only available when in Federal Information Processing Standards (FIPS) mode.")]
		public const int TPM_E_NOTFIPS = unchecked((int)0x80280036);

		[Description("The command is attempting to use an invalid family ID.")]
		public const int TPM_E_INVALID_FAMILY = unchecked((int)0x80280037);

		[Description("The permission to manipulate the NV storage is not available.")]
		public const int TPM_E_NO_NV_PERMISSION = unchecked((int)0x80280038);

		[Description("The operation requires a signed command.")]
		public const int TPM_E_REQUIRES_SIGN = unchecked((int)0x80280039);

		[Description("Wrong operation to load an NV key.")]
		public const int TPM_E_KEY_NOTSUPPORTED = unchecked((int)0x8028003A);

		[Description("NV_LoadKey blob requires both owner and blob authorization.")]
		public const int TPM_E_AUTH_CONFLICT = unchecked((int)0x8028003B);

		[Description("The NV area is locked and not writable.")]
		public const int TPM_E_AREA_LOCKED = unchecked((int)0x8028003C);

		[Description("The locality is incorrect for the attempted operation.")]
		public const int TPM_E_BAD_LOCALITY = unchecked((int)0x8028003D);

		[Description("The NV area is read-only and cannot be written to.")]
		public const int TPM_E_READ_ONLY = unchecked((int)0x8028003E);

		[Description("There is no protection on the write to the NV area.")]
		public const int TPM_E_PER_NOWRITE = unchecked((int)0x8028003F);

		[Description("The family count value does not match.")]
		public const int TPM_E_FAMILYCOUNT = unchecked((int)0x80280040);

		[Description("The NV area has already been written to.")]
		public const int TPM_E_WRITE_LOCKED = unchecked((int)0x80280041);

		[Description("The NV area attributes conflict.")]
		public const int TPM_E_BAD_ATTRIBUTES = unchecked((int)0x80280042);

		[Description("The structure tag and version are invalid or inconsistent.")]
		public const int TPM_E_INVALID_STRUCTURE = unchecked((int)0x80280043);

		[Description("The key is under control of the TPM owner and can only be evicted by the TPM owner.")]
		public const int TPM_E_KEY_OWNER_CONTROL = unchecked((int)0x80280044);

		[Description("The counter handle is incorrect.")]
		public const int TPM_E_BAD_COUNTER = unchecked((int)0x80280045);

		[Description("The write is not a complete write of the area.")]
		public const int TPM_E_NOT_FULLWRITE = unchecked((int)0x80280046);

		[Description("The gap between saved context counts is too large.")]
		public const int TPM_E_CONTEXT_GAP = unchecked((int)0x80280047);

		[Description("The maximum number of NV writes without an owner has been exceeded.")]
		public const int TPM_E_MAXNVWRITES = unchecked((int)0x80280048);

		[Description("No operator AuthData value is set.")]
		public const int TPM_E_NOOPERATOR = unchecked((int)0x80280049);

		[Description("The resource pointed to by context is not loaded.")]
		public const int TPM_E_RESOURCEMISSING = unchecked((int)0x8028004A);

		[Description("The delegate administration is locked.")]
		public const int TPM_E_DELEGATE_LOCK = unchecked((int)0x8028004B);

		[Description("Attempt to manage a family other then the delegated family.")]
		public const int TPM_E_DELEGATE_FAMILY = unchecked((int)0x8028004C);

		[Description("Delegation table management not enabled.")]
		public const int TPM_E_DELEGATE_ADMIN = unchecked((int)0x8028004D);

		[Description("There was a command executed outside an exclusive transport session.")]
		public const int TPM_E_TRANSPORT_NOTEXCLUSIVE = unchecked((int)0x8028004E);

		[Description("Attempt to context save an owner evict controlled key.")]
		public const int TPM_E_OWNER_CONTROL = unchecked((int)0x8028004F);

		[Description("The DAA command has no resources available to execute the command.")]
		public const int TPM_E_DAA_RESOURCES = unchecked((int)0x80280050);

		[Description("The consistency check on DAA parameter inputData0 has failed.")]
		public const int TPM_E_DAA_INPUT_DATA0 = unchecked((int)0x80280051);

		[Description("The consistency check on DAA parameter inputData1 has failed.")]
		public const int TPM_E_DAA_INPUT_DATA1 = unchecked((int)0x80280052);

		[Description("The consistency check on DAA_issuerSettings has failed.")]
		public const int TPM_E_DAA_ISSUER_SETTINGS = unchecked((int)0x80280053);

		[Description("The consistency check on DAA_tpmSpecific has failed.")]
		public const int TPM_E_DAA_TPM_SETTINGS = unchecked((int)0x80280054);

		[Description("The atomic process indicated by the submitted DAA command is not the expected process.")]
		public const int TPM_E_DAA_STAGE = unchecked((int)0x80280055);

		[Description("The issuer's validity check has detected an inconsistency.")]
		public const int TPM_E_DAA_ISSUER_VALIDITY = unchecked((int)0x80280056);

		[Description("The consistency check on w has failed.")]
		public const int TPM_E_DAA_WRONG_W = unchecked((int)0x80280057);

		[Description("The handle is incorrect.")]
		public const int TPM_E_BAD_HANDLE = unchecked((int)0x80280058);

		[Description("Delegation is not correct.")]
		public const int TPM_E_BAD_DELEGATE = unchecked((int)0x80280059);

		[Description("The context blob is invalid.")]
		public const int TPM_E_BADCONTEXT = unchecked((int)0x8028005A);

		[Description("Too many contexts held by the TPM.")]
		public const int TPM_E_TOOMANYCONTEXTS = unchecked((int)0x8028005B);

		[Description("Migration authority signature validation failure.")]
		public const int TPM_E_MA_TICKET_SIGNATURE = unchecked((int)0x8028005C);

		[Description("Migration destination not authenticated.")]
		public const int TPM_E_MA_DESTINATION = unchecked((int)0x8028005D);

		[Description("Migration source incorrect.")]
		public const int TPM_E_MA_SOURCE = unchecked((int)0x8028005E);

		[Description("Incorrect migration authority.")]
		public const int TPM_E_MA_AUTHORITY = unchecked((int)0x8028005F);

		[Description("Attempt to revoke the EK and the EK is not revocable.")]
		public const int TPM_E_PERMANENTEK = unchecked((int)0x80280061);

		[Description("Bad signature of CMK ticket.")]
		public const int TPM_E_BAD_SIGNATURE = unchecked((int)0x80280062);

		[Description("There is no room in the context list for additional contexts.")]
		public const int TPM_E_NOCONTEXTSPACE = unchecked((int)0x80280063);

		[Description("The command was blocked.")]
		public const int TPM_E_COMMAND_BLOCKED = unchecked((int)0x80280400);

		[Description("The specified handle was not found.")]
		public const int TPM_E_INVALID_HANDLE = unchecked((int)0x80280401);

		[Description("The TPM returned a duplicate handle and the command needs to be resubmitted.")]
		public const int TPM_E_DUPLICATE_VHANDLE = unchecked((int)0x80280402);

		[Description("The command within the transport was blocked.")]
		public const int TPM_E_EMBEDDED_COMMAND_BLOCKED = unchecked((int)0x80280403);

		[Description("The command within the transport is not supported.")]
		public const int TPM_E_EMBEDDED_COMMAND_UNSUPPORTED = unchecked((int)0x80280404);

		[Description("The TPM is too busy to respond to the command immediately, but the command could be resubmitted at a later time.")]
		public const int TPM_E_RETRY = unchecked((int)0x80280800);

		[Description("SelfTestFull has not been run.")]
		public const int TPM_E_NEEDS_SELFTEST = unchecked((int)0x80280801);

		[Description("The TPM is currently executing a full self-test.")]
		public const int TPM_E_DOING_SELFTEST = unchecked((int)0x80280802);

		[Description("The TPM is defending against dictionary attacks and is in a time-out period.")]
		public const int TPM_E_DEFEND_LOCK_RUNNING = unchecked((int)0x80280803);

		[Description("An internal software error has been detected.")]
		public const int TBS_E_INTERNAL_ERROR = unchecked((int)0x80284001);

		[Description("One or more input parameters are bad.")]
		public const int TBS_E_BAD_PARAMETER = unchecked((int)0x80284002);

		[Description("A specified output pointer is bad.")]
		public const int TBS_E_INVALID_OUTPUT_POINTER = unchecked((int)0x80284003);

		[Description("The specified context handle does not refer to a valid context.")]
		public const int TBS_E_INVALID_CONTEXT = unchecked((int)0x80284004);

		[Description("A specified output buffer is too small.")]
		public const int TBS_E_INSUFFICIENT_BUFFER = unchecked((int)0x80284005);

		[Description("An error occurred while communicating with the TPM.")]
		public const int TBS_E_IOERROR = unchecked((int)0x80284006);

		[Description("One or more context parameters are invalid.")]
		public const int TBS_E_INVALID_CONTEXT_PARAM = unchecked((int)0x80284007);

		[Description("The TPM Base Services (TBS) is not running and could not be started.")]
		public const int TBS_E_SERVICE_NOT_RUNNING = unchecked((int)0x80284008);

		[Description("A new context could not be created because there are too many open contexts.")]
		public const int TBS_E_TOO_MANY_TBS_CONTEXTS = unchecked((int)0x80284009);

		[Description("A new virtual resource could not be created because there are too many open virtual resources.")]
		public const int TBS_E_TOO_MANY_RESOURCES = unchecked((int)0x8028400A);

		[Description("The TBS service has been started but is not yet running.")]
		public const int TBS_E_SERVICE_START_PENDING = unchecked((int)0x8028400B);

		[Description("The physical presence interface is not supported.")]
		public const int TBS_E_PPI_NOT_SUPPORTED = unchecked((int)0x8028400C);

		[Description("The command was canceled.")]
		public const int TBS_E_COMMAND_CANCELED = unchecked((int)0x8028400D);

		[Description("The input or output buffer is too large.")]
		public const int TBS_E_BUFFER_TOO_LARGE = unchecked((int)0x8028400E);

		[Description("The command buffer is not in the correct state.")]
		public const int TPMAPI_E_INVALID_STATE = unchecked((int)0x80290100);

		[Description("The command buffer does not contain enough data to satisfy the request.")]
		public const int TPMAPI_E_NOT_ENOUGH_DATA = unchecked((int)0x80290101);

		[Description("The command buffer cannot contain any more data.")]
		public const int TPMAPI_E_TOO_MUCH_DATA = unchecked((int)0x80290102);

		[Description("One or more output parameters was null or invalid.")]
		public const int TPMAPI_E_INVALID_OUTPUT_POINTER = unchecked((int)0x80290103);

		[Description("One or more input parameters are invalid.")]
		public const int TPMAPI_E_INVALID_PARAMETER = unchecked((int)0x80290104);

		[Description("Not enough memory was available to satisfy the request.")]
		public const int TPMAPI_E_OUT_OF_MEMORY = unchecked((int)0x80290105);

		[Description("The specified buffer was too small.")]
		public const int TPMAPI_E_BUFFER_TOO_SMALL = unchecked((int)0x80290106);

		[Description("An internal error was detected.")]
		public const int TPMAPI_E_INTERNAL_ERROR = unchecked((int)0x80290107);

		[Description("The caller does not have the appropriate rights to perform the requested operation.")]
		public const int TPMAPI_E_ACCESS_DENIED = unchecked((int)0x80290108);

		[Description("The specified authorization information was invalid.")]
		public const int TPMAPI_E_AUTHORIZATION_FAILED = unchecked((int)0x80290109);

		[Description("The specified context handle was not valid.")]
		public const int TPMAPI_E_INVALID_CONTEXT_HANDLE = unchecked((int)0x8029010A);

		[Description("An error occurred while communicating with the TBS.")]
		public const int TPMAPI_E_TBS_COMMUNICATION_ERROR = unchecked((int)0x8029010B);

		[Description("The TPM returned an unexpected result.")]
		public const int TPMAPI_E_TPM_COMMAND_ERROR = unchecked((int)0x8029010C);

		[Description("The message was too large for the encoding scheme.")]
		public const int TPMAPI_E_MESSAGE_TOO_LARGE = unchecked((int)0x8029010D);

		[Description("The encoding in the binary large object (BLOB) was not recognized.")]
		public const int TPMAPI_E_INVALID_ENCODING = unchecked((int)0x8029010E);

		[Description("The key size is not valid.")]
		public const int TPMAPI_E_INVALID_KEY_SIZE = unchecked((int)0x8029010F);

		[Description("The encryption operation failed.")]
		public const int TPMAPI_E_ENCRYPTION_FAILED = unchecked((int)0x80290110);

		[Description("The key parameters structure was not valid.")]
		public const int TPMAPI_E_INVALID_KEY_PARAMS = unchecked((int)0x80290111);

		[Description("The requested supplied data does not appear to be a valid migration authorization BLOB.")]
		public const int TPMAPI_E_INVALID_MIGRATION_AUTHORIZATION_BLOB = unchecked((int)0x80290112);

		[Description("The specified PCR index was invalid.")]
		public const int TPMAPI_E_INVALID_PCR_INDEX = unchecked((int)0x80290113);

		[Description("The data given does not appear to be a valid delegate BLOB.")]
		public const int TPMAPI_E_INVALID_DELEGATE_BLOB = unchecked((int)0x80290114);

		[Description("One or more of the specified context parameters was not valid.")]
		public const int TPMAPI_E_INVALID_CONTEXT_PARAMS = unchecked((int)0x80290115);

		[Description("The data given does not appear to be a valid key BLOB.")]
		public const int TPMAPI_E_INVALID_KEY_BLOB = unchecked((int)0x80290116);

		[Description("The specified PCR data was invalid.")]
		public const int TPMAPI_E_INVALID_PCR_DATA = unchecked((int)0x80290117);

		[Description("The format of the owner authorization data was invalid.")]
		public const int TPMAPI_E_INVALID_OWNER_AUTH = unchecked((int)0x80290118);

		[Description("The specified buffer was too small.")]
		public const int TBSIMP_E_BUFFER_TOO_SMALL = unchecked((int)0x80290200);

		[Description("The context could not be cleaned up.")]
		public const int TBSIMP_E_CLEANUP_FAILED = unchecked((int)0x80290201);

		[Description("The specified context handle is invalid.")]
		public const int TBSIMP_E_INVALID_CONTEXT_HANDLE = unchecked((int)0x80290202);

		[Description("An invalid context parameter was specified.")]
		public const int TBSIMP_E_INVALID_CONTEXT_PARAM = unchecked((int)0x80290203);

		[Description("An error occurred while communicating with the TPM.")]
		public const int TBSIMP_E_TPM_ERROR = unchecked((int)0x80290204);

		[Description("No entry with the specified key was found.")]
		public const int TBSIMP_E_HASH_BAD_KEY = unchecked((int)0x80290205);

		[Description("The specified virtual handle matches a virtual handle already in use.")]
		public const int TBSIMP_E_DUPLICATE_VHANDLE = unchecked((int)0x80290206);

		[Description("The pointer to the returned handle location was null or invalid.")]
		public const int TBSIMP_E_INVALID_OUTPUT_POINTER = unchecked((int)0x80290207);

		[Description("One or more parameters are invalid.")]
		public const int TBSIMP_E_INVALID_PARAMETER = unchecked((int)0x80290208);

		[Description("The RPC subsystem could not be initialized.")]
		public const int TBSIMP_E_RPC_INIT_FAILED = unchecked((int)0x80290209);

		[Description("The TBS scheduler is not running.")]
		public const int TBSIMP_E_SCHEDULER_NOT_RUNNING = unchecked((int)0x8029020A);

		[Description("The command was canceled.")]
		public const int TBSIMP_E_COMMAND_CANCELED = unchecked((int)0x8029020B);

		[Description("There was not enough memory to fulfill the request.")]
		public const int TBSIMP_E_OUT_OF_MEMORY = unchecked((int)0x8029020C);

		[Description("The specified list is empty, or the iteration has reached the end of the list.")]
		public const int TBSIMP_E_LIST_NO_MORE_ITEMS = unchecked((int)0x8029020D);

		[Description("The specified item was not found in the list.")]
		public const int TBSIMP_E_LIST_NOT_FOUND = unchecked((int)0x8029020E);

		[Description("The TPM does not have enough space to load the requested resource.")]
		public const int TBSIMP_E_NOT_ENOUGH_SPACE = unchecked((int)0x8029020F);

		[Description("There are too many TPM contexts in use.")]
		public const int TBSIMP_E_NOT_ENOUGH_TPM_CONTEXTS = unchecked((int)0x80290210);

		[Description("The TPM command failed.")]
		public const int TBSIMP_E_COMMAND_FAILED = unchecked((int)0x80290211);

		[Description("The TBS does not recognize the specified ordinal.")]
		public const int TBSIMP_E_UNKNOWN_ORDINAL = unchecked((int)0x80290212);

		[Description("The requested resource is no longer available.")]
		public const int TBSIMP_E_RESOURCE_EXPIRED = unchecked((int)0x80290213);

		[Description("The resource type did not match.")]
		public const int TBSIMP_E_INVALID_RESOURCE = unchecked((int)0x80290214);

		[Description("No resources can be unloaded.")]
		public const int TBSIMP_E_NOTHING_TO_UNLOAD = unchecked((int)0x80290215);

		[Description("No new entries can be added to the hash table.")]
		public const int TBSIMP_E_HASH_TABLE_FULL = unchecked((int)0x80290216);

		[Description("A new TBS context could not be created because there are too many open contexts.")]
		public const int TBSIMP_E_TOO_MANY_TBS_CONTEXTS = unchecked((int)0x80290217);

		[Description("A new virtual resource could not be created because there are too many open virtual resources.")]
		public const int TBSIMP_E_TOO_MANY_RESOURCES = unchecked((int)0x80290218);

		[Description("The physical presence interface is not supported.")]
		public const int TBSIMP_E_PPI_NOT_SUPPORTED = unchecked((int)0x80290219);

		[Description("TBS is not compatible with the version of TPM found on the system.")]
		public const int TBSIMP_E_TPM_INCOMPATIBLE = unchecked((int)0x8029021A);

		[Description("A general error was detected when attempting to acquire the BIOS response to a physical presence command.")]
		public const int TPM_E_PPI_ACPI_FAILURE = unchecked((int)0x80290300);

		[Description("The user failed to confirm the TPM operation request.")]
		public const int TPM_E_PPI_USER_ABORT = unchecked((int)0x80290301);

		[Description("The BIOS failure prevented the successful execution of the requested TPM operation (for example, invalid TPM operation request, BIOS communication error with the TPM).")]
		public const int TPM_E_PPI_BIOS_FAILURE = unchecked((int)0x80290302);

		[Description("The BIOS does not support the physical presence interface.")]
		public const int TPM_E_PPI_NOT_SUPPORTED = unchecked((int)0x80290303);

		[Description("A Data Collector Set was not found.")]
		public const int PLA_E_DCS_NOT_FOUND = unchecked((int)0x80300002);

		[Description("Unable to start Data Collector Set because there are too many folders.")]
		public const int PLA_E_TOO_MANY_FOLDERS = unchecked((int)0x80300045);

		[Description("Not enough free disk space to start Data Collector Set.")]
		public const int PLA_E_NO_MIN_DISK = unchecked((int)0x80300070);

		[Description("Data Collector Set is in use.")]
		public const int PLA_E_DCS_IN_USE = unchecked((int)0x803000AA);

		[Description("Data Collector Set already exists.")]
		public const int PLA_E_DCS_ALREADY_EXISTS = unchecked((int)0x803000B7);

		[Description("Property value conflict.")]
		public const int PLA_E_PROPERTY_CONFLICT = unchecked((int)0x80300101);

		[Description("The current configuration for this Data Collector Set requires that it contain exactly one Data Collector.")]
		public const int PLA_E_DCS_SINGLETON_REQUIRED = unchecked((int)0x80300102);

		[Description("A user account is required to commit the current Data Collector Set properties.")]
		public const int PLA_E_CREDENTIALS_REQUIRED = unchecked((int)0x80300103);

		[Description("Data Collector Set is not running.")]
		public const int PLA_E_DCS_NOT_RUNNING = unchecked((int)0x80300104);

		[Description("A conflict was detected in the list of include and exclude APIs. Do not specify the same API in both the include list and the exclude list.")]
		public const int PLA_E_CONFLICT_INCL_EXCL_API = unchecked((int)0x80300105);

		[Description("The executable path specified refers to a network share or UNC path.")]
		public const int PLA_E_NETWORK_EXE_NOT_VALID = unchecked((int)0x80300106);

		[Description("The executable path specified is already configured for API tracing.")]
		public const int PLA_E_EXE_ALREADY_CONFIGURED = unchecked((int)0x80300107);

		[Description("The executable path specified does not exist. Verify that the specified path is correct.")]
		public const int PLA_E_EXE_PATH_NOT_VALID = unchecked((int)0x80300108);

		[Description("Data Collector already exists.")]
		public const int PLA_E_DC_ALREADY_EXISTS = unchecked((int)0x80300109);

		[Description("The wait for the Data Collector Set start notification has timed out.")]
		public const int PLA_E_DCS_START_WAIT_TIMEOUT = unchecked((int)0x8030010A);

		[Description("The wait for the Data Collector to start has timed out.")]
		public const int PLA_E_DC_START_WAIT_TIMEOUT = unchecked((int)0x8030010B);

		[Description("The wait for the report generation tool to finish has timed out.")]
		public const int PLA_E_REPORT_WAIT_TIMEOUT = unchecked((int)0x8030010C);

		[Description("Duplicate items are not allowed.")]
		public const int PLA_E_NO_DUPLICATES = unchecked((int)0x8030010D);

		[Description("When specifying the executable to trace, you must specify a full path to the executable and not just a file name.")]
		public const int PLA_E_EXE_FULL_PATH_REQUIRED = unchecked((int)0x8030010E);

		[Description("The session name provided is invalid.")]
		public const int PLA_E_INVALID_SESSION_NAME = unchecked((int)0x8030010F);

		[Description("The Event Log channel Microsoft-Windows-Diagnosis-PLA/Operational must be enabled to perform this operation.")]
		public const int PLA_E_PLA_CHANNEL_NOT_ENABLED = unchecked((int)0x80300110);

		[Description("The Event Log channel Microsoft-Windows-TaskScheduler must be enabled to perform this operation.")]
		public const int PLA_E_TASKSCHED_CHANNEL_NOT_ENABLED = unchecked((int)0x80300111);

		[Description("The volume must be unlocked before it can be used.")]
		public const int FVE_E_LOCKED_VOLUME = unchecked((int)0x80310000);

		[Description("The volume is fully decrypted and no key is available.")]
		public const int FVE_E_NOT_ENCRYPTED = unchecked((int)0x80310001);

		[Description("The firmware does not support using a TPM during boot.")]
		public const int FVE_E_NO_TPM_BIOS = unchecked((int)0x80310002);

		[Description("The firmware does not use a TPM to perform initial program load (IPL) measurement.")]
		public const int FVE_E_NO_MBR_METRIC = unchecked((int)0x80310003);

		[Description("The master boot record (MBR) is not TPM-aware.")]
		public const int FVE_E_NO_BOOTSECTOR_METRIC = unchecked((int)0x80310004);

		[Description("The BOOTMGR is not being measured by the TPM.")]
		public const int FVE_E_NO_BOOTMGR_METRIC = unchecked((int)0x80310005);

		[Description("The BOOTMGR component does not perform expected TPM measurements.")]
		public const int FVE_E_WRONG_BOOTMGR = unchecked((int)0x80310006);

		[Description("No secure key protection mechanism has been defined.")]
		public const int FVE_E_SECURE_KEY_REQUIRED = unchecked((int)0x80310007);

		[Description("This volume has not been provisioned for encryption.")]
		public const int FVE_E_NOT_ACTIVATED = unchecked((int)0x80310008);

		[Description("Requested action was denied by the full-volume encryption (FVE) control engine.")]
		public const int FVE_E_ACTION_NOT_ALLOWED = unchecked((int)0x80310009);

		[Description("The Active Directory forest does not contain the required attributes and classes to host FVE or TPM information.")]
		public const int FVE_E_AD_SCHEMA_NOT_INSTALLED = unchecked((int)0x8031000A);

		[Description("The type of data obtained from Active Directory was not expected.")]
		public const int FVE_E_AD_INVALID_DATATYPE = unchecked((int)0x8031000B);

		[Description("The size of the data obtained from Active Directory was not expected.")]
		public const int FVE_E_AD_INVALID_DATASIZE = unchecked((int)0x8031000C);

		[Description("The attribute read from Active Directory has no (zero) values.")]
		public const int FVE_E_AD_NO_VALUES = unchecked((int)0x8031000D);

		[Description("The attribute was not set.")]
		public const int FVE_E_AD_ATTR_NOT_SET = unchecked((int)0x8031000E);

		[Description("The specified GUID could not be found.")]
		public const int FVE_E_AD_GUID_NOT_FOUND = unchecked((int)0x8031000F);

		[Description("The control block for the encrypted volume is not valid.")]
		public const int FVE_E_BAD_INFORMATION = unchecked((int)0x80310010);

		[Description("Not enough free space remaining on volume to allow encryption.")]
		public const int FVE_E_TOO_SMALL = unchecked((int)0x80310011);

		[Description("The volume cannot be encrypted because it is required to boot the operating system.")]
		public const int FVE_E_SYSTEM_VOLUME = unchecked((int)0x80310012);

		[Description("The volume cannot be encrypted because the file system is not supported.")]
		public const int FVE_E_FAILED_WRONG_FS = unchecked((int)0x80310013);

		[Description("The file system is inconsistent. Run CHKDSK.")]
		public const int FVE_E_FAILED_BAD_FS = unchecked((int)0x80310014);

		[Description("This volume cannot be encrypted.")]
		public const int FVE_E_NOT_SUPPORTED = unchecked((int)0x80310015);

		[Description("Data supplied is malformed.")]
		public const int FVE_E_BAD_DATA = unchecked((int)0x80310016);

		[Description("Volume is not bound to the system.")]
		public const int FVE_E_VOLUME_NOT_BOUND = unchecked((int)0x80310017);

		[Description("TPM must be owned before a volume can be bound to it.")]
		public const int FVE_E_TPM_NOT_OWNED = unchecked((int)0x80310018);

		[Description("The volume specified is not a data volume.")]
		public const int FVE_E_NOT_DATA_VOLUME = unchecked((int)0x80310019);

		[Description("The buffer supplied to a function was insufficient to contain the returned data.")]
		public const int FVE_E_AD_INSUFFICIENT_BUFFER = unchecked((int)0x8031001A);

		[Description("A read operation failed while converting the volume.")]
		public const int FVE_E_CONV_READ = unchecked((int)0x8031001B);

		[Description("A write operation failed while converting the volume.")]
		public const int FVE_E_CONV_WRITE = unchecked((int)0x8031001C);

		[Description("One or more key protection mechanisms are required for this volume.")]
		public const int FVE_E_KEY_REQUIRED = unchecked((int)0x8031001D);

		[Description("Cluster configurations are not supported.")]
		public const int FVE_E_CLUSTERING_NOT_SUPPORTED = unchecked((int)0x8031001E);

		[Description("The volume is already bound to the system.")]
		public const int FVE_E_VOLUME_BOUND_ALREADY = unchecked((int)0x8031001F);

		[Description("The boot OS volume is not being protected via FVE.")]
		public const int FVE_E_OS_NOT_PROTECTED = unchecked((int)0x80310020);

		[Description("All protection mechanisms are effectively disabled (clear key exists).")]
		public const int FVE_E_PROTECTION_DISABLED = unchecked((int)0x80310021);

		[Description("A recovery key protection mechanism is required.")]
		public const int FVE_E_RECOVERY_KEY_REQUIRED = unchecked((int)0x80310022);

		[Description("This volume cannot be bound to a TPM.")]
		public const int FVE_E_FOREIGN_VOLUME = unchecked((int)0x80310023);

		[Description("The control block for the encrypted volume was updated by another thread. Try again.")]
		public const int FVE_E_OVERLAPPED_UPDATE = unchecked((int)0x80310024);

		[Description("The SRK authentication of the TPM is not zero and, therefore, is not compatible.")]
		public const int FVE_E_TPM_SRK_AUTH_NOT_ZERO = unchecked((int)0x80310025);

		[Description("The volume encryption algorithm cannot be used on this sector size.")]
		public const int FVE_E_FAILED_SECTOR_SIZE = unchecked((int)0x80310026);

		[Description("BitLocker recovery authentication failed.")]
		public const int FVE_E_FAILED_AUTHENTICATION = unchecked((int)0x80310027);

		[Description("The volume specified is not the boot OS volume.")]
		public const int FVE_E_NOT_OS_VOLUME = unchecked((int)0x80310028);

		[Description("Auto-unlock information for data volumes is present on the boot OS volume.")]
		public const int FVE_E_AUTOUNLOCK_ENABLED = unchecked((int)0x80310029);

		[Description("The system partition boot sector does not perform TPM measurements.")]
		public const int FVE_E_WRONG_BOOTSECTOR = unchecked((int)0x8031002A);

		[Description("The system partition file system must be NTFS.")]
		public const int FVE_E_WRONG_SYSTEM_FS = unchecked((int)0x8031002B);

		[Description("Group policy requires a recovery password before encryption can begin.")]
		public const int FVE_E_POLICY_PASSWORD_REQUIRED = unchecked((int)0x8031002C);

		[Description("The volume encryption algorithm and key cannot be set on an encrypted volume.")]
		public const int FVE_E_CANNOT_SET_FVEK_ENCRYPTED = unchecked((int)0x8031002D);

		[Description("A key must be specified before encryption can begin.")]
		public const int FVE_E_CANNOT_ENCRYPT_NO_KEY = unchecked((int)0x8031002E);

		[Description("A bootable CD/DVD is in the system. Remove the CD/DVD and reboot the system.")]
		public const int FVE_E_BOOTABLE_CDDVD = unchecked((int)0x80310030);

		[Description("An instance of this key protector already exists on the volume.")]
		public const int FVE_E_PROTECTOR_EXISTS = unchecked((int)0x80310031);

		[Description("The file cannot be saved to a relative path.")]
		public const int FVE_E_RELATIVE_PATH = unchecked((int)0x80310032);

		[Description("The callout does not exist.")]
		public const int FWP_E_CALLOUT_NOT_FOUND = unchecked((int)0x80320001);

		[Description("The filter condition does not exist.")]
		public const int FWP_E_CONDITION_NOT_FOUND = unchecked((int)0x80320002);

		[Description("The filter does not exist.")]
		public const int FWP_E_FILTER_NOT_FOUND = unchecked((int)0x80320003);

		[Description("The layer does not exist.")]
		public const int FWP_E_LAYER_NOT_FOUND = unchecked((int)0x80320004);

		[Description("The provider does not exist.")]
		public const int FWP_E_PROVIDER_NOT_FOUND = unchecked((int)0x80320005);

		[Description("The provider context does not exist.")]
		public const int FWP_E_PROVIDER_CONTEXT_NOT_FOUND = unchecked((int)0x80320006);

		[Description("The sublayer does not exist.")]
		public const int FWP_E_SUBLAYER_NOT_FOUND = unchecked((int)0x80320007);

		[Description("The object does not exist.")]
		public const int FWP_E_NOT_FOUND = unchecked((int)0x80320008);

		[Description("An object with that GUID or LUID already exists.")]
		public const int FWP_E_ALREADY_EXISTS = unchecked((int)0x80320009);

		[Description("The object is referenced by other objects and, therefore, cannot be deleted.")]
		public const int FWP_E_IN_USE = unchecked((int)0x8032000A);

		[Description("The call is not allowed from within a dynamic session.")]
		public const int FWP_E_DYNAMIC_SESSION_IN_PROGRESS = unchecked((int)0x8032000B);

		[Description("The call was made from the wrong session and, therefore, cannot be completed.")]
		public const int FWP_E_WRONG_SESSION = unchecked((int)0x8032000C);

		[Description("The call must be made from within an explicit transaction.")]
		public const int FWP_E_NO_TXN_IN_PROGRESS = unchecked((int)0x8032000D);

		[Description("The call is not allowed from within an explicit transaction.")]
		public const int FWP_E_TXN_IN_PROGRESS = unchecked((int)0x8032000E);

		[Description("The explicit transaction has been forcibly canceled.")]
		public const int FWP_E_TXN_ABORTED = unchecked((int)0x8032000F);

		[Description("The session has been canceled.")]
		public const int FWP_E_SESSION_ABORTED = unchecked((int)0x80320010);

		[Description("The call is not allowed from within a read-only transaction.")]
		public const int FWP_E_INCOMPATIBLE_TXN = unchecked((int)0x80320011);

		[Description("The call timed out while waiting to acquire the transaction lock.")]
		public const int FWP_E_TIMEOUT = unchecked((int)0x80320012);

		[Description("Collection of network diagnostic events is disabled.")]
		public const int FWP_E_NET_EVENTS_DISABLED = unchecked((int)0x80320013);

		[Description("The operation is not supported by the specified layer.")]
		public const int FWP_E_INCOMPATIBLE_LAYER = unchecked((int)0x80320014);

		[Description("The call is allowed for kernel-mode callers only.")]
		public const int FWP_E_KM_CLIENTS_ONLY = unchecked((int)0x80320015);

		[Description("The call tried to associate two objects with incompatible lifetimes.")]
		public const int FWP_E_LIFETIME_MISMATCH = unchecked((int)0x80320016);

		[Description("The object is built in and, therefore, cannot be deleted.")]
		public const int FWP_E_BUILTIN_OBJECT = unchecked((int)0x80320017);

		[Description("The maximum number of boot-time filters has been reached.")]
		public const int FWP_E_TOO_MANY_BOOTTIME_FILTERS = unchecked((int)0x80320018);

		[Description("A notification could not be delivered because a message queue is at its maximum capacity.")]
		public const int FWP_E_NOTIFICATION_DROPPED = unchecked((int)0x80320019);

		[Description("The traffic parameters do not match those for the security association context.")]
		public const int FWP_E_TRAFFIC_MISMATCH = unchecked((int)0x8032001A);

		[Description("The call is not allowed for the current security association state.")]
		public const int FWP_E_INCOMPATIBLE_SA_STATE = unchecked((int)0x8032001B);

		[Description("A required pointer is null.")]
		public const int FWP_E_NULL_POINTER = unchecked((int)0x8032001C);

		[Description("An enumerator is not valid.")]
		public const int FWP_E_INVALID_ENUMERATOR = unchecked((int)0x8032001D);

		[Description("The flags field contains an invalid value.")]
		public const int FWP_E_INVALID_FLAGS = unchecked((int)0x8032001E);

		[Description("A network mask is not valid.")]
		public const int FWP_E_INVALID_NET_MASK = unchecked((int)0x8032001F);

		[Description("An FWP_RANGE is not valid.")]
		public const int FWP_E_INVALID_RANGE = unchecked((int)0x80320020);

		[Description("The time interval is not valid.")]
		public const int FWP_E_INVALID_INTERVAL = unchecked((int)0x80320021);

		[Description("An array that must contain at least one element that is zero-length.")]
		public const int FWP_E_ZERO_LENGTH_ARRAY = unchecked((int)0x80320022);

		[Description("The displayData.name field cannot be null.")]
		public const int FWP_E_NULL_DISPLAY_NAME = unchecked((int)0x80320023);

		[Description("The action type is not one of the allowed action types for a filter.")]
		public const int FWP_E_INVALID_ACTION_TYPE = unchecked((int)0x80320024);

		[Description("The filter weight is not valid.")]
		public const int FWP_E_INVALID_WEIGHT = unchecked((int)0x80320025);

		[Description("A filter condition contains a match type that is not compatible with the operands.")]
		public const int FWP_E_MATCH_TYPE_MISMATCH = unchecked((int)0x80320026);

		[Description("An FWP_VALUE or FWPM_CONDITION_VALUE is of the wrong type.")]
		public const int FWP_E_TYPE_MISMATCH = unchecked((int)0x80320027);

		[Description("An integer value is outside the allowed range.")]
		public const int FWP_E_OUT_OF_BOUNDS = unchecked((int)0x80320028);

		[Description("A reserved field is nonzero.")]
		public const int FWP_E_RESERVED = unchecked((int)0x80320029);

		[Description("A filter cannot contain multiple conditions operating on a single field.")]
		public const int FWP_E_DUPLICATE_CONDITION = unchecked((int)0x8032002A);

		[Description("A policy cannot contain the same keying module more than once.")]
		public const int FWP_E_DUPLICATE_KEYMOD = unchecked((int)0x8032002B);

		[Description("The action type is not compatible with the layer.")]
		public const int FWP_E_ACTION_INCOMPATIBLE_WITH_LAYER = unchecked((int)0x8032002C);

		[Description("The action type is not compatible with the sublayer.")]
		public const int FWP_E_ACTION_INCOMPATIBLE_WITH_SUBLAYER = unchecked((int)0x8032002D);

		[Description("The raw context or the provider context is not compatible with the layer.")]
		public const int FWP_E_CONTEXT_INCOMPATIBLE_WITH_LAYER = unchecked((int)0x8032002E);

		[Description("The raw context or the provider context is not compatible with the callout.")]
		public const int FWP_E_CONTEXT_INCOMPATIBLE_WITH_CALLOUT = unchecked((int)0x8032002F);

		[Description("The authentication method is not compatible with the policy type.")]
		public const int FWP_E_INCOMPATIBLE_AUTH_METHOD = unchecked((int)0x80320030);

		[Description("The Diffie-Hellman group is not compatible with the policy type.")]
		public const int FWP_E_INCOMPATIBLE_DH_GROUP = unchecked((int)0x80320031);

		[Description("An Internet Key Exchange (IKE) policy cannot contain an Extended Mode policy.")]
		public const int FWP_E_EM_NOT_SUPPORTED = unchecked((int)0x80320032);

		[Description("The enumeration template or subscription will never match any objects.")]
		public const int FWP_E_NEVER_MATCH = unchecked((int)0x80320033);

		[Description("The provider context is of the wrong type.")]
		public const int FWP_E_PROVIDER_CONTEXT_MISMATCH = unchecked((int)0x80320034);

		[Description("The parameter is incorrect.")]
		public const int FWP_E_INVALID_PARAMETER = unchecked((int)0x80320035);

		[Description("The maximum number of sublayers has been reached.")]
		public const int FWP_E_TOO_MANY_SUBLAYERS = unchecked((int)0x80320036);

		[Description("The notification function for a callout returned an error.")]
		public const int FWP_E_CALLOUT_NOTIFICATION_FAILED = unchecked((int)0x80320037);

		[Description("The IPsec authentication configuration is not compatible with the authentication type.")]
		public const int FWP_E_INCOMPATIBLE_AUTH_CONFIG = unchecked((int)0x80320038);

		[Description("The IPsec cipher configuration is not compatible with the cipher type.")]
		public const int FWP_E_INCOMPATIBLE_CIPHER_CONFIG = unchecked((int)0x80320039);

		[Description("The binding to the network interface is being closed.")]
		public const int ERROR_NDIS_INTERFACE_CLOSING = unchecked((int)0x80340002);

		[Description("An invalid version was specified.")]
		public const int ERROR_NDIS_BAD_VERSION = unchecked((int)0x80340004);

		[Description("An invalid characteristics table was used.")]
		public const int ERROR_NDIS_BAD_CHARACTERISTICS = unchecked((int)0x80340005);

		[Description("Failed to find the network interface, or the network interface is not ready.")]
		public const int ERROR_NDIS_ADAPTER_NOT_FOUND = unchecked((int)0x80340006);

		[Description("Failed to open the network interface.")]
		public const int ERROR_NDIS_OPEN_FAILED = unchecked((int)0x80340007);

		[Description("The network interface has encountered an internal unrecoverable failure.")]
		public const int ERROR_NDIS_DEVICE_FAILED = unchecked((int)0x80340008);

		[Description("The multicast list on the network interface is full.")]
		public const int ERROR_NDIS_MULTICAST_FULL = unchecked((int)0x80340009);

		[Description("An attempt was made to add a duplicate multicast address to the list.")]
		public const int ERROR_NDIS_MULTICAST_EXISTS = unchecked((int)0x8034000A);

		[Description("At attempt was made to remove a multicast address that was never added.")]
		public const int ERROR_NDIS_MULTICAST_NOT_FOUND = unchecked((int)0x8034000B);

		[Description("The network interface aborted the request.")]
		public const int ERROR_NDIS_REQUEST_ABORTED = unchecked((int)0x8034000C);

		[Description("The network interface cannot process the request because it is being reset.")]
		public const int ERROR_NDIS_RESET_IN_PROGRESS = unchecked((int)0x8034000D);

		[Description("An attempt was made to send an invalid packet on a network interface.")]
		public const int ERROR_NDIS_INVALID_PACKET = unchecked((int)0x8034000F);

		[Description("The specified request is not a valid operation for the target device.")]
		public const int ERROR_NDIS_INVALID_DEVICE_REQUEST = unchecked((int)0x80340010);

		[Description("The network interface is not ready to complete this operation.")]
		public const int ERROR_NDIS_ADAPTER_NOT_READY = unchecked((int)0x80340011);

		[Description("The length of the buffer submitted for this operation is not valid.")]
		public const int ERROR_NDIS_INVALID_LENGTH = unchecked((int)0x80340014);

		[Description("The data used for this operation is not valid.")]
		public const int ERROR_NDIS_INVALID_DATA = unchecked((int)0x80340015);

		[Description("The length of the buffer submitted for this operation is too small.")]
		public const int ERROR_NDIS_BUFFER_TOO_SHORT = unchecked((int)0x80340016);

		[Description("The network interface does not support this OID.")]
		public const int ERROR_NDIS_INVALID_OID = unchecked((int)0x80340017);

		[Description("The network interface has been removed.")]
		public const int ERROR_NDIS_ADAPTER_REMOVED = unchecked((int)0x80340018);

		[Description("The network interface does not support this media type.")]
		public const int ERROR_NDIS_UNSUPPORTED_MEDIA = unchecked((int)0x80340019);

		[Description("An attempt was made to remove a token ring group address that is in use by other components.")]
		public const int ERROR_NDIS_GROUP_ADDRESS_IN_USE = unchecked((int)0x8034001A);

		[Description("An attempt was made to map a file that cannot be found.")]
		public const int ERROR_NDIS_FILE_NOT_FOUND = unchecked((int)0x8034001B);

		[Description("An error occurred while the NDIS tried to map the file.")]
		public const int ERROR_NDIS_ERROR_READING_FILE = unchecked((int)0x8034001C);

		[Description("An attempt was made to map a file that is already mapped.")]
		public const int ERROR_NDIS_ALREADY_MAPPED = unchecked((int)0x8034001D);

		[Description("An attempt to allocate a hardware resource failed because the resource is used by another component.")]
		public const int ERROR_NDIS_RESOURCE_CONFLICT = unchecked((int)0x8034001E);

		[Description("The I/O operation failed because network media is disconnected or the wireless access point is out of range.")]
		public const int ERROR_NDIS_MEDIA_DISCONNECTED = unchecked((int)0x8034001F);

		[Description("The network address used in the request is invalid.")]
		public const int ERROR_NDIS_INVALID_ADDRESS = unchecked((int)0x80340022);

		[Description("The offload operation on the network interface has been paused.")]
		public const int ERROR_NDIS_PAUSED = unchecked((int)0x8034002A);

		[Description("The network interface was not found.")]
		public const int ERROR_NDIS_INTERFACE_NOT_FOUND = unchecked((int)0x8034002B);

		[Description("The revision number specified in the structure is not supported.")]
		public const int ERROR_NDIS_UNSUPPORTED_REVISION = unchecked((int)0x8034002C);

		[Description("The specified port does not exist on this network interface.")]
		public const int ERROR_NDIS_INVALID_PORT = unchecked((int)0x8034002D);

		[Description("The current state of the specified port on this network interface does not support the requested operation.")]
		public const int ERROR_NDIS_INVALID_PORT_STATE = unchecked((int)0x8034002E);

		[Description("The network interface does not support this request.")]
		public const int ERROR_NDIS_NOT_SUPPORTED = unchecked((int)0x803400BB);

		[Description("The wireless local area network (LAN) interface is in auto-configuration mode and does not support the requested parameter change operation.")]
		public const int ERROR_NDIS_DOT11_AUTO_CONFIG_ENABLED = unchecked((int)0x80342000);

		[Description("The wireless LAN interface is busy and cannot perform the requested operation.")]
		public const int ERROR_NDIS_DOT11_MEDIA_IN_USE = unchecked((int)0x80342001);

		[Description("The wireless LAN interface is shutting down and does not support the requested operation.")]
		public const int ERROR_NDIS_DOT11_POWER_STATE_INVALID = unchecked((int)0x80342002);

		[Description("A requested object was not found.")]
		public const int TRK_E_NOT_FOUND = unchecked((int)0x8DEAD01B);

		[Description("The server received a CREATE_VOLUME subrequest of a SYNC_VOLUMES request, but the ServerVolumeTable size limit for the RequestMachine has already been reached.")]
		public const int TRK_E_VOLUME_QUOTA_EXCEEDED = unchecked((int)0x8DEAD01C);

		[Description("The server is busy, and the client should retry the request at a later time.")]
		public const int TRK_SERVER_TOO_BUSY = unchecked((int)0x8DEAD01E);

		[Description("The specified event is currently not being audited.")]
		public const int ERROR_AUDITING_DISABLED = unchecked((int)0xC0090001);

		[Description("The SID filtering operation removed all SIDs.")]
		public const int ERROR_ALL_SIDS_FILTERED = unchecked((int)0xC0090002);

		[Description("Business rule scripts are disabled for the calling application.")]
		public const int ERROR_BIZRULES_NOT_ENABLED = unchecked((int)0xC0090003);

		[Description("There is no connection established with the Windows Media server. The operation failed.")]
		public const int NS_E_NOCONNECTION = unchecked((int)0xC00D0005);

		[Description("Unable to establish a connection to the server.")]
		public const int NS_E_CANNOTCONNECT = unchecked((int)0xC00D0006);

		[Description("Unable to destroy the title.")]
		public const int NS_E_CANNOTDESTROYTITLE = unchecked((int)0xC00D0007);

		[Description("Unable to rename the title.")]
		public const int NS_E_CANNOTRENAMETITLE = unchecked((int)0xC00D0008);

		[Description("Unable to offline disk.")]
		public const int NS_E_CANNOTOFFLINEDISK = unchecked((int)0xC00D0009);

		[Description("Unable to online disk.")]
		public const int NS_E_CANNOTONLINEDISK = unchecked((int)0xC00D000A);

		[Description("There is no file parser registered for this type of file.")]
		public const int NS_E_NOREGISTEREDWALKER = unchecked((int)0xC00D000B);

		[Description("There is no data connection established.")]
		public const int NS_E_NOFUNNEL = unchecked((int)0xC00D000C);

		[Description("Failed to load the local play DLL.")]
		public const int NS_E_NO_LOCALPLAY = unchecked((int)0xC00D000D);

		[Description("The network is busy.")]
		public const int NS_E_NETWORK_BUSY = unchecked((int)0xC00D000E);

		[Description("The server session limit was exceeded.")]
		public const int NS_E_TOO_MANY_SESS = unchecked((int)0xC00D000F);

		[Description("The network connection already exists.")]
		public const int NS_E_ALREADY_CONNECTED = unchecked((int)0xC00D0010);

		[Description("Index %1 is invalid.")]
		public const int NS_E_INVALID_INDEX = unchecked((int)0xC00D0011);

		[Description("There is no protocol or protocol version supported by both the client and the server.")]
		public const int NS_E_PROTOCOL_MISMATCH = unchecked((int)0xC00D0012);

		[Description("The server, a computer set up to offer multimedia content to other computers, could not handle your request for multimedia content in a timely manner. Please try again later.")]
		public const int NS_E_TIMEOUT = unchecked((int)0xC00D0013);

		[Description("Error writing to the network.")]
		public const int NS_E_NET_WRITE = unchecked((int)0xC00D0014);

		[Description("Error reading from the network.")]
		public const int NS_E_NET_READ = unchecked((int)0xC00D0015);

		[Description("Error writing to a disk.")]
		public const int NS_E_DISK_WRITE = unchecked((int)0xC00D0016);

		[Description("Error reading from a disk.")]
		public const int NS_E_DISK_READ = unchecked((int)0xC00D0017);

		[Description("Error writing to a file.")]
		public const int NS_E_FILE_WRITE = unchecked((int)0xC00D0018);

		[Description("Error reading from a file.")]
		public const int NS_E_FILE_READ = unchecked((int)0xC00D0019);

		[Description("The system cannot find the file specified.")]
		public const int NS_E_FILE_NOT_FOUND = unchecked((int)0xC00D001A);

		[Description("The file already exists.")]
		public const int NS_E_FILE_EXISTS = unchecked((int)0xC00D001B);

		[Description("The file name, directory name, or volume label syntax is incorrect.")]
		public const int NS_E_INVALID_NAME = unchecked((int)0xC00D001C);

		[Description("Failed to open a file.")]
		public const int NS_E_FILE_OPEN_FAILED = unchecked((int)0xC00D001D);

		[Description("Unable to allocate a file.")]
		public const int NS_E_FILE_ALLOCATION_FAILED = unchecked((int)0xC00D001E);

		[Description("Unable to initialize a file.")]
		public const int NS_E_FILE_INIT_FAILED = unchecked((int)0xC00D001F);

		[Description("Unable to play a file.")]
		public const int NS_E_FILE_PLAY_FAILED = unchecked((int)0xC00D0020);

		[Description("Could not set the disk UID.")]
		public const int NS_E_SET_DISK_UID_FAILED = unchecked((int)0xC00D0021);

		[Description("An error was induced for testing purposes.")]
		public const int NS_E_INDUCED = unchecked((int)0xC00D0022);

		[Description("Two Content Servers failed to communicate.")]
		public const int NS_E_CCLINK_DOWN = unchecked((int)0xC00D0023);

		[Description("An unknown error occurred.")]
		public const int NS_E_INTERNAL = unchecked((int)0xC00D0024);

		[Description("The requested resource is in use.")]
		public const int NS_E_BUSY = unchecked((int)0xC00D0025);

		[Description("The specified protocol is not recognized. Be sure that the file name and syntax, such as slashes, are correct for the protocol.")]
		public const int NS_E_UNRECOGNIZED_STREAM_TYPE = unchecked((int)0xC00D0026);

		[Description("The network service provider failed.")]
		public const int NS_E_NETWORK_SERVICE_FAILURE = unchecked((int)0xC00D0027);

		[Description("An attempt to acquire a network resource failed.")]
		public const int NS_E_NETWORK_RESOURCE_FAILURE = unchecked((int)0xC00D0028);

		[Description("The network connection has failed.")]
		public const int NS_E_CONNECTION_FAILURE = unchecked((int)0xC00D0029);

		[Description("The session is being terminated locally.")]
		public const int NS_E_SHUTDOWN = unchecked((int)0xC00D002A);

		[Description("The request is invalid in the current state.")]
		public const int NS_E_INVALID_REQUEST = unchecked((int)0xC00D002B);

		[Description("There is insufficient bandwidth available to fulfill the request.")]
		public const int NS_E_INSUFFICIENT_BANDWIDTH = unchecked((int)0xC00D002C);

		[Description("The disk is not rebuilding.")]
		public const int NS_E_NOT_REBUILDING = unchecked((int)0xC00D002D);

		[Description("An operation requested for a particular time could not be carried out on schedule.")]
		public const int NS_E_LATE_OPERATION = unchecked((int)0xC00D002E);

		[Description("Invalid or corrupt data was encountered.")]
		public const int NS_E_INVALID_DATA = unchecked((int)0xC00D002F);

		[Description("The bandwidth required to stream a file is higher than the maximum file bandwidth allowed on the server.")]
		public const int NS_E_FILE_BANDWIDTH_LIMIT = unchecked((int)0xC00D0030);

		[Description("The client cannot have any more files open simultaneously.")]
		public const int NS_E_OPEN_FILE_LIMIT = unchecked((int)0xC00D0031);

		[Description("The server received invalid data from the client on the control connection.")]
		public const int NS_E_BAD_CONTROL_DATA = unchecked((int)0xC00D0032);

		[Description("There is no stream available.")]
		public const int NS_E_NO_STREAM = unchecked((int)0xC00D0033);

		[Description("There is no more data in the stream.")]
		public const int NS_E_STREAM_END = unchecked((int)0xC00D0034);

		[Description("The specified server could not be found.")]
		public const int NS_E_SERVER_NOT_FOUND = unchecked((int)0xC00D0035);

		[Description("The specified name is already in use.")]
		public const int NS_E_DUPLICATE_NAME = unchecked((int)0xC00D0036);

		[Description("The specified address is already in use.")]
		public const int NS_E_DUPLICATE_ADDRESS = unchecked((int)0xC00D0037);

		[Description("The specified address is not a valid multicast address.")]
		public const int NS_E_BAD_MULTICAST_ADDRESS = unchecked((int)0xC00D0038);

		[Description("The specified adapter address is invalid.")]
		public const int NS_E_BAD_ADAPTER_ADDRESS = unchecked((int)0xC00D0039);

		[Description("The specified delivery mode is invalid.")]
		public const int NS_E_BAD_DELIVERY_MODE = unchecked((int)0xC00D003A);

		[Description("The specified station does not exist.")]
		public const int NS_E_INVALID_CHANNEL = unchecked((int)0xC00D003B);

		[Description("The specified stream does not exist.")]
		public const int NS_E_INVALID_STREAM = unchecked((int)0xC00D003C);

		[Description("The specified archive could not be opened.")]
		public const int NS_E_INVALID_ARCHIVE = unchecked((int)0xC00D003D);

		[Description("The system cannot find any titles on the server.")]
		public const int NS_E_NOTITLES = unchecked((int)0xC00D003E);

		[Description("The system cannot find the client specified.")]
		public const int NS_E_INVALID_CLIENT = unchecked((int)0xC00D003F);

		[Description("The Blackhole Address is not initialized.")]
		public const int NS_E_INVALID_BLACKHOLE_ADDRESS = unchecked((int)0xC00D0040);

		[Description("The station does not support the stream format.")]
		public const int NS_E_INCOMPATIBLE_FORMAT = unchecked((int)0xC00D0041);

		[Description("The specified key is not valid.")]
		public const int NS_E_INVALID_KEY = unchecked((int)0xC00D0042);

		[Description("The specified port is not valid.")]
		public const int NS_E_INVALID_PORT = unchecked((int)0xC00D0043);

		[Description("The specified TTL is not valid.")]
		public const int NS_E_INVALID_TTL = unchecked((int)0xC00D0044);

		[Description("The request to fast forward or rewind could not be fulfilled.")]
		public const int NS_E_STRIDE_REFUSED = unchecked((int)0xC00D0045);

		[Description("Unable to load the appropriate file parser.")]
		public const int NS_E_MMSAUTOSERVER_CANTFINDWALKER = unchecked((int)0xC00D0046);

		[Description("Cannot exceed the maximum bandwidth limit.")]
		public const int NS_E_MAX_BITRATE = unchecked((int)0xC00D0047);

		[Description("Invalid value for LogFilePeriod.")]
		public const int NS_E_LOGFILEPERIOD = unchecked((int)0xC00D0048);

		[Description("Cannot exceed the maximum client limit.")]
		public const int NS_E_MAX_CLIENTS = unchecked((int)0xC00D0049);

		[Description("The maximum log file size has been reached.")]
		public const int NS_E_LOG_FILE_SIZE = unchecked((int)0xC00D004A);

		[Description("Cannot exceed the maximum file rate.")]
		public const int NS_E_MAX_FILERATE = unchecked((int)0xC00D004B);

		[Description("Unknown file type.")]
		public const int NS_E_WALKER_UNKNOWN = unchecked((int)0xC00D004C);

		[Description("The specified file, %1, cannot be loaded onto the specified server, %2.")]
		public const int NS_E_WALKER_SERVER = unchecked((int)0xC00D004D);

		[Description("There was a usage error with file parser.")]
		public const int NS_E_WALKER_USAGE = unchecked((int)0xC00D004E);

		[Description("The Title Server %1 has failed.")]
		public const int NS_E_TIGER_FAIL = unchecked((int)0xC00D0050);

		[Description("Content Server %1 (%2) has failed.")]
		public const int NS_E_CUB_FAIL = unchecked((int)0xC00D0053);

		[Description("Disk %1 ( %2 ) on Content Server %3, has failed.")]
		public const int NS_E_DISK_FAIL = unchecked((int)0xC00D0055);

		[Description("The NetShow data stream limit of %1 streams was reached.")]
		public const int NS_E_MAX_FUNNELS_ALERT = unchecked((int)0xC00D0060);

		[Description("The NetShow Video Server was unable to allocate a %1 block file named %2.")]
		public const int NS_E_ALLOCATE_FILE_FAIL = unchecked((int)0xC00D0061);

		[Description("A Content Server was unable to page a block.")]
		public const int NS_E_PAGING_ERROR = unchecked((int)0xC00D0062);

		[Description("Disk %1 has unrecognized control block version %2.")]
		public const int NS_E_BAD_BLOCK0_VERSION = unchecked((int)0xC00D0063);

		[Description("Disk %1 has incorrect uid %2.")]
		public const int NS_E_BAD_DISK_UID = unchecked((int)0xC00D0064);

		[Description("Disk %1 has unsupported file system major version %2.")]
		public const int NS_E_BAD_FSMAJOR_VERSION = unchecked((int)0xC00D0065);

		[Description("Disk %1 has bad stamp number in control block.")]
		public const int NS_E_BAD_STAMPNUMBER = unchecked((int)0xC00D0066);

		[Description("Disk %1 is partially reconstructed.")]
		public const int NS_E_PARTIALLY_REBUILT_DISK = unchecked((int)0xC00D0067);

		[Description("EnactPlan gives up.")]
		public const int NS_E_ENACTPLAN_GIVEUP = unchecked((int)0xC00D0068);

		[Description("The key was not found in the registry.")]
		public const int MCMADM_E_REGKEY_NOT_FOUND = unchecked((int)0xC00D006A);

		[Description("The publishing point cannot be started because the server does not have the appropriate stream formats. Use the Multicast Announcement Wizard to create a new announcement for this publishing point.")]
		public const int NS_E_NO_FORMATS = unchecked((int)0xC00D006B);

		[Description("No reference URLs were found in an ASX file.")]
		public const int NS_E_NO_REFERENCES = unchecked((int)0xC00D006C);

		[Description("Error opening wave device, the device might be in use.")]
		public const int NS_E_WAVE_OPEN = unchecked((int)0xC00D006D);

		[Description("Unable to establish a connection to the NetShow event monitor service.")]
		public const int NS_E_CANNOTCONNECTEVENTS = unchecked((int)0xC00D006F);

		[Description("No device driver is present on the system.")]
		public const int NS_E_NO_DEVICE = unchecked((int)0xC00D0071);

		[Description("No specified device driver is present.")]
		public const int NS_E_NO_SPECIFIED_DEVICE = unchecked((int)0xC00D0072);

		[Description("Netshow Events Monitor is not operational and has been disconnected.")]
		public const int NS_E_MONITOR_GIVEUP = unchecked((int)0xC00D00C8);

		[Description("Disk %1 is remirrored.")]
		public const int NS_E_REMIRRORED_DISK = unchecked((int)0xC00D00C9);

		[Description("Insufficient data found.")]
		public const int NS_E_INSUFFICIENT_DATA = unchecked((int)0xC00D00CA);

		[Description("1 failed in file %2 line %3.")]
		public const int NS_E_ASSERT = unchecked((int)0xC00D00CB);

		[Description("The specified adapter name is invalid.")]
		public const int NS_E_BAD_ADAPTER_NAME = unchecked((int)0xC00D00CC);

		[Description("The application is not licensed for this feature.")]
		public const int NS_E_NOT_LICENSED = unchecked((int)0xC00D00CD);

		[Description("Unable to contact the server.")]
		public const int NS_E_NO_SERVER_CONTACT = unchecked((int)0xC00D00CE);

		[Description("Maximum number of titles exceeded.")]
		public const int NS_E_TOO_MANY_TITLES = unchecked((int)0xC00D00CF);

		[Description("Maximum size of a title exceeded.")]
		public const int NS_E_TITLE_SIZE_EXCEEDED = unchecked((int)0xC00D00D0);

		[Description("UDP protocol not enabled. Not trying %1!ls!.")]
		public const int NS_E_UDP_DISABLED = unchecked((int)0xC00D00D1);

		[Description("TCP protocol not enabled. Not trying %1!ls!.")]
		public const int NS_E_TCP_DISABLED = unchecked((int)0xC00D00D2);

		[Description("HTTP protocol not enabled. Not trying %1!ls!.")]
		public const int NS_E_HTTP_DISABLED = unchecked((int)0xC00D00D3);

		[Description("The product license has expired.")]
		public const int NS_E_LICENSE_EXPIRED = unchecked((int)0xC00D00D4);

		[Description("Source file exceeds the per title maximum bitrate. See NetShow Theater documentation for more information.")]
		public const int NS_E_TITLE_BITRATE = unchecked((int)0xC00D00D5);

		[Description("The program name cannot be empty.")]
		public const int NS_E_EMPTY_PROGRAM_NAME = unchecked((int)0xC00D00D6);

		[Description("Station %1 does not exist.")]
		public const int NS_E_MISSING_CHANNEL = unchecked((int)0xC00D00D7);

		[Description("You need to define at least one station before this operation can complete.")]
		public const int NS_E_NO_CHANNELS = unchecked((int)0xC00D00D8);

		[Description("The index specified is invalid.")]
		public const int NS_E_INVALID_INDEX2 = unchecked((int)0xC00D00D9);

		[Description("Content Server %1 (%2) has failed its link to Content Server %3.")]
		public const int NS_E_CUB_FAIL_LINK = unchecked((int)0xC00D0190);

		[Description("Content Server %1 (%2) has incorrect uid %3.")]
		public const int NS_E_BAD_CUB_UID = unchecked((int)0xC00D0192);

		[Description("Server unreliable because multiple components failed.")]
		public const int NS_E_GLITCH_MODE = unchecked((int)0xC00D0195);

		[Description("Content Server %1 (%2) is unable to communicate with the Media System Network Protocol.")]
		public const int NS_E_NO_MEDIA_PROTOCOL = unchecked((int)0xC00D019B);

		[Description("Nothing to do.")]
		public const int NS_E_NOTHING_TO_DO = unchecked((int)0xC00D07F1);

		[Description("Not receiving data from the server.")]
		public const int NS_E_NO_MULTICAST = unchecked((int)0xC00D07F2);

		[Description("The input media format is invalid.")]
		public const int NS_E_INVALID_INPUT_FORMAT = unchecked((int)0xC00D0BB8);

		[Description("The MSAudio codec is not installed on this system.")]
		public const int NS_E_MSAUDIO_NOT_INSTALLED = unchecked((int)0xC00D0BB9);

		[Description("An unexpected error occurred with the MSAudio codec.")]
		public const int NS_E_UNEXPECTED_MSAUDIO_ERROR = unchecked((int)0xC00D0BBA);

		[Description("The output media format is invalid.")]
		public const int NS_E_INVALID_OUTPUT_FORMAT = unchecked((int)0xC00D0BBB);

		[Description("The object must be fully configured before audio samples can be processed.")]
		public const int NS_E_NOT_CONFIGURED = unchecked((int)0xC00D0BBC);

		[Description("You need a license to perform the requested operation on this media file.")]
		public const int NS_E_PROTECTED_CONTENT = unchecked((int)0xC00D0BBD);

		[Description("You need a license to perform the requested operation on this media file.")]
		public const int NS_E_LICENSE_REQUIRED = unchecked((int)0xC00D0BBE);

		[Description("This media file is corrupted or invalid. Contact the content provider for a new file.")]
		public const int NS_E_TAMPERED_CONTENT = unchecked((int)0xC00D0BBF);

		[Description("The license for this media file has expired. Get a new license or contact the content provider for further assistance.")]
		public const int NS_E_LICENSE_OUTOFDATE = unchecked((int)0xC00D0BC0);

		[Description("You are not allowed to open this file. Contact the content provider for further assistance.")]
		public const int NS_E_LICENSE_INCORRECT_RIGHTS = unchecked((int)0xC00D0BC1);

		[Description("The requested audio codec is not installed on this system.")]
		public const int NS_E_AUDIO_CODEC_NOT_INSTALLED = unchecked((int)0xC00D0BC2);

		[Description("An unexpected error occurred with the audio codec.")]
		public const int NS_E_AUDIO_CODEC_ERROR = unchecked((int)0xC00D0BC3);

		[Description("The requested video codec is not installed on this system.")]
		public const int NS_E_VIDEO_CODEC_NOT_INSTALLED = unchecked((int)0xC00D0BC4);

		[Description("An unexpected error occurred with the video codec.")]
		public const int NS_E_VIDEO_CODEC_ERROR = unchecked((int)0xC00D0BC5);

		[Description("The Profile is invalid.")]
		public const int NS_E_INVALIDPROFILE = unchecked((int)0xC00D0BC6);

		[Description("A new version of the SDK is needed to play the requested content.")]
		public const int NS_E_INCOMPATIBLE_VERSION = unchecked((int)0xC00D0BC7);

		[Description("The requested URL is not available in offline mode.")]
		public const int NS_E_OFFLINE_MODE = unchecked((int)0xC00D0BCA);

		[Description("The requested URL cannot be accessed because there is no network connection.")]
		public const int NS_E_NOT_CONNECTED = unchecked((int)0xC00D0BCB);

		[Description("The encoding process was unable to keep up with the amount of supplied data.")]
		public const int NS_E_TOO_MUCH_DATA = unchecked((int)0xC00D0BCC);

		[Description("The given property is not supported.")]
		public const int NS_E_UNSUPPORTED_PROPERTY = unchecked((int)0xC00D0BCD);

		[Description("Windows Media Player cannot copy the files to the CD because they are 8-bit. Convert the files to 16-bit, 44-kHz stereo files by using Sound Recorder or another audio-processing program, and then try again.")]
		public const int NS_E_8BIT_WAVE_UNSUPPORTED = unchecked((int)0xC00D0BCE);

		[Description("There are no more samples in the current range.")]
		public const int NS_E_NO_MORE_SAMPLES = unchecked((int)0xC00D0BCF);

		[Description("The given sampling rate is invalid.")]
		public const int NS_E_INVALID_SAMPLING_RATE = unchecked((int)0xC00D0BD0);

		[Description("The given maximum packet size is too small to accommodate this profile.)")]
		public const int NS_E_MAX_PACKET_SIZE_TOO_SMALL = unchecked((int)0xC00D0BD1);

		[Description("The packet arrived too late to be of use.")]
		public const int NS_E_LATE_PACKET = unchecked((int)0xC00D0BD2);

		[Description("The packet is a duplicate of one received before.")]
		public const int NS_E_DUPLICATE_PACKET = unchecked((int)0xC00D0BD3);

		[Description("Supplied buffer is too small.")]
		public const int NS_E_SDK_BUFFERTOOSMALL = unchecked((int)0xC00D0BD4);

		[Description("The wrong number of preprocessing passes was used for the stream's output type.")]
		public const int NS_E_INVALID_NUM_PASSES = unchecked((int)0xC00D0BD5);

		[Description("An attempt was made to add, modify, or delete a read only attribute.")]
		public const int NS_E_ATTRIBUTE_READ_ONLY = unchecked((int)0xC00D0BD6);

		[Description("An attempt was made to add attribute that is not allowed for the given media type.")]
		public const int NS_E_ATTRIBUTE_NOT_ALLOWED = unchecked((int)0xC00D0BD7);

		[Description("The EDL provided is invalid.")]
		public const int NS_E_INVALID_EDL = unchecked((int)0xC00D0BD8);

		[Description("The Data Unit Extension data was too large to be used.")]
		public const int NS_E_DATA_UNIT_EXTENSION_TOO_LARGE = unchecked((int)0xC00D0BD9);

		[Description("An unexpected error occurred with a DMO codec.")]
		public const int NS_E_CODEC_DMO_ERROR = unchecked((int)0xC00D0BDA);

		[Description("This feature has been disabled by group policy.")]
		public const int NS_E_FEATURE_DISABLED_BY_GROUP_POLICY = unchecked((int)0xC00D0BDC);

		[Description("This feature is disabled in this SKU.")]
		public const int NS_E_FEATURE_DISABLED_IN_SKU = unchecked((int)0xC00D0BDD);

		[Description("There is no CD in the CD drive. Insert a CD, and then try again.")]
		public const int NS_E_NO_CD = unchecked((int)0xC00D0FA0);

		[Description("Windows Media Player could not use digital playback to play the CD. To switch to analog playback, on the Tools menu, click Options, and then click the Devices tab. Double-click the CD drive, and then in the Playback area, click Analog. For additional assistance, click Web Help.")]
		public const int NS_E_CANT_READ_DIGITAL = unchecked((int)0xC00D0FA1);

		[Description("Windows Media Player no longer detects a connected portable device. Reconnect your portable device, and then try synchronizing the file again.")]
		public const int NS_E_DEVICE_DISCONNECTED = unchecked((int)0xC00D0FA2);

		[Description("Windows Media Player cannot play the file. The portable device does not support the specified file type.")]
		public const int NS_E_DEVICE_NOT_SUPPORT_FORMAT = unchecked((int)0xC00D0FA3);

		[Description("Windows Media Player could not use digital playback to play the CD. The Player has automatically switched the CD drive to analog playback. To switch back to digital CD playback, use the Devices tab. For additional assistance, click Web Help.")]
		public const int NS_E_SLOW_READ_DIGITAL = unchecked((int)0xC00D0FA4);

		[Description("An invalid line error occurred in the mixer.")]
		public const int NS_E_MIXER_INVALID_LINE = unchecked((int)0xC00D0FA5);

		[Description("An invalid control error occurred in the mixer.")]
		public const int NS_E_MIXER_INVALID_CONTROL = unchecked((int)0xC00D0FA6);

		[Description("An invalid value error occurred in the mixer.")]
		public const int NS_E_MIXER_INVALID_VALUE = unchecked((int)0xC00D0FA7);

		[Description("An unrecognized MMRESULT occurred in the mixer.")]
		public const int NS_E_MIXER_UNKNOWN_MMRESULT = unchecked((int)0xC00D0FA8);

		[Description("User has stopped the operation.")]
		public const int NS_E_USER_STOP = unchecked((int)0xC00D0FA9);

		[Description("Windows Media Player cannot rip the track because a compatible MP3 encoder is not installed on your computer. Install a compatible MP3 encoder or choose a different format to rip to (such as Windows Media Audio).")]
		public const int NS_E_MP3_FORMAT_NOT_FOUND = unchecked((int)0xC00D0FAA);

		[Description("Windows Media Player cannot read the CD. The disc might be dirty or damaged. Turn on error correction, and then try again.")]
		public const int NS_E_CD_READ_ERROR_NO_CORRECTION = unchecked((int)0xC00D0FAB);

		[Description("Windows Media Player cannot read the CD. The disc might be dirty or damaged or the CD drive might be malfunctioning.")]
		public const int NS_E_CD_READ_ERROR = unchecked((int)0xC00D0FAC);

		[Description("For best performance, do not play CD tracks while ripping them.")]
		public const int NS_E_CD_SLOW_COPY = unchecked((int)0xC00D0FAD);

		[Description("It is not possible to directly burn tracks from one CD to another CD. You must first rip the tracks from the CD to your computer, and then burn the files to a blank CD.")]
		public const int NS_E_CD_COPYTO_CD = unchecked((int)0xC00D0FAE);

		[Description("Could not open a sound mixer driver.")]
		public const int NS_E_MIXER_NODRIVER = unchecked((int)0xC00D0FAF);

		[Description("Windows Media Player cannot rip tracks from the CD correctly because the CD drive settings in Device Manager do not match the CD drive settings in the Player.")]
		public const int NS_E_REDBOOK_ENABLED_WHILE_COPYING = unchecked((int)0xC00D0FB0);

		[Description("Windows Media Player is busy reading the CD.")]
		public const int NS_E_CD_REFRESH = unchecked((int)0xC00D0FB1);

		[Description("Windows Media Player could not use digital playback to play the CD. The Player has automatically switched the CD drive to analog playback. To switch back to digital CD playback, use the Devices tab. For additional assistance, click Web Help.")]
		public const int NS_E_CD_DRIVER_PROBLEM = unchecked((int)0xC00D0FB2);

		[Description("Windows Media Player could not use digital playback to play the CD. The Player has automatically switched the CD drive to analog playback. To switch back to digital CD playback, use the Devices tab. For additional assistance, click Web Help.")]
		public const int NS_E_WONT_DO_DIGITAL = unchecked((int)0xC00D0FB3);

		[Description("A call was made to GetParseError on the XML parser but there was no error to retrieve.")]
		public const int NS_E_WMPXML_NOERROR = unchecked((int)0xC00D0FB4);

		[Description("The XML Parser ran out of data while parsing.")]
		public const int NS_E_WMPXML_ENDOFDATA = unchecked((int)0xC00D0FB5);

		[Description("A generic parse error occurred in the XML parser but no information is available.")]
		public const int NS_E_WMPXML_PARSEERROR = unchecked((int)0xC00D0FB6);

		[Description("A call get GetNamedAttribute or GetNamedAttributeIndex on the XML parser resulted in the index not being found.")]
		public const int NS_E_WMPXML_ATTRIBUTENOTFOUND = unchecked((int)0xC00D0FB7);

		[Description("A call was made go GetNamedPI on the XML parser, but the requested Processing Instruction was not found.")]
		public const int NS_E_WMPXML_PINOTFOUND = unchecked((int)0xC00D0FB8);

		[Description("Persist was called on the XML parser, but the parser has no data to persist.")]
		public const int NS_E_WMPXML_EMPTYDOC = unchecked((int)0xC00D0FB9);

		[Description("This file path is already in the library.")]
		public const int NS_E_WMP_PATH_ALREADY_IN_LIBRARY = unchecked((int)0xC00D0FBA);

		[Description("Windows Media Player is already searching for files to add to your library. Wait for the current process to finish before attempting to search again.")]
		public const int NS_E_WMP_FILESCANALREADYSTARTED = unchecked((int)0xC00D0FBE);

		[Description("Windows Media Player is unable to find the media you are looking for.")]
		public const int NS_E_WMP_HME_INVALIDOBJECTID = unchecked((int)0xC00D0FBF);

		[Description("A component of Windows Media Player is out-of-date. If you are running a pre-release version of Windows, try upgrading to a more recent version.")]
		public const int NS_E_WMP_MF_CODE_EXPIRED = unchecked((int)0xC00D0FC0);

		[Description("This container does not support search on items.")]
		public const int NS_E_WMP_HME_NOTSEARCHABLEFORITEMS = unchecked((int)0xC00D0FC1);

		[Description("Windows Media Player encountered a problem while adding one or more files to the library. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_ADDTOLIBRARY_FAILED = unchecked((int)0xC00D0FC7);

		[Description("A Windows API call failed but no error information was available.")]
		public const int NS_E_WMP_WINDOWSAPIFAILURE = unchecked((int)0xC00D0FC8);

		[Description("This file does not have burn rights. If you obtained this file from an online store, go to the online store to get burn rights.")]
		public const int NS_E_WMP_RECORDING_NOT_ALLOWED = unchecked((int)0xC00D0FC9);

		[Description("Windows Media Player no longer detects a connected portable device. Reconnect your portable device, and then try to sync the file again.")]
		public const int NS_E_DEVICE_NOT_READY = unchecked((int)0xC00D0FCA);

		[Description("Windows Media Player cannot play the file because it is corrupted.")]
		public const int NS_E_DAMAGED_FILE = unchecked((int)0xC00D0FCB);

		[Description("Windows Media Player encountered an error while attempting to access information in the library. Try restarting the Player.")]
		public const int NS_E_MPDB_GENERIC = unchecked((int)0xC00D0FCC);

		[Description("The file cannot be added to the library because it is smaller than the \"Skip files smaller than\" setting. To add the file, change the setting on the Library tab. For additional assistance, click Web Help.")]
		public const int NS_E_FILE_FAILED_CHECKS = unchecked((int)0xC00D0FCD);

		[Description("Windows Media Player cannot create the library. You must be logged on as an administrator or a member of the Administrators group to install the Player. For more information, contact your system administrator.")]
		public const int NS_E_MEDIA_LIBRARY_FAILED = unchecked((int)0xC00D0FCE);

		[Description("The file is already in use. Close other programs that might be using the file, or stop playing the file, and then try again.")]
		public const int NS_E_SHARING_VIOLATION = unchecked((int)0xC00D0FCF);

		[Description("Windows Media Player has encountered an unknown error.")]
		public const int NS_E_NO_ERROR_STRING_FOUND = unchecked((int)0xC00D0FD0);

		[Description("The Windows Media Player ActiveX control cannot connect to remote media services, but will continue with local media services.")]
		public const int NS_E_WMPOCX_NO_REMOTE_CORE = unchecked((int)0xC00D0FD1);

		[Description("The requested method or property is not available because the Windows Media Player ActiveX control has not been properly activated.")]
		public const int NS_E_WMPOCX_NO_ACTIVE_CORE = unchecked((int)0xC00D0FD2);

		[Description("The Windows Media Player ActiveX control is not running in remote mode.")]
		public const int NS_E_WMPOCX_NOT_RUNNING_REMOTELY = unchecked((int)0xC00D0FD3);

		[Description("An error occurred while trying to get the remote Windows Media Player window.")]
		public const int NS_E_WMPOCX_NO_REMOTE_WINDOW = unchecked((int)0xC00D0FD4);

		[Description("Windows Media Player has encountered an unknown error.")]
		public const int NS_E_WMPOCX_ERRORMANAGERNOTAVAILABLE = unchecked((int)0xC00D0FD5);

		[Description("Windows Media Player was not closed properly. A damaged or incompatible plug-in might have caused the problem to occur. As a precaution, all optional plug-ins have been disabled.")]
		public const int NS_E_PLUGIN_NOTSHUTDOWN = unchecked((int)0xC00D0FD6);

		[Description("Windows Media Player cannot find the specified path. Verify that the path is typed correctly. If it is, the path does not exist in the specified location, or the computer where the path is located is not available.")]
		public const int NS_E_WMP_CANNOT_FIND_FOLDER = unchecked((int)0xC00D0FD7);

		[Description("Windows Media Player cannot save a file that is being streamed.")]
		public const int NS_E_WMP_STREAMING_RECORDING_NOT_ALLOWED = unchecked((int)0xC00D0FD8);

		[Description("Windows Media Player cannot find the selected plug-in. The Player will try to remove it from the menu. To use this plug-in, install it again.")]
		public const int NS_E_WMP_PLUGINDLL_NOTFOUND = unchecked((int)0xC00D0FD9);

		[Description("Action requires input from the user.")]
		public const int NS_E_NEED_TO_ASK_USER = unchecked((int)0xC00D0FDA);

		[Description("The Windows Media Player ActiveX control must be in a docked state for this action to be performed.")]
		public const int NS_E_WMPOCX_PLAYER_NOT_DOCKED = unchecked((int)0xC00D0FDB);

		[Description("The Windows Media Player external object is not ready.")]
		public const int NS_E_WMP_EXTERNAL_NOTREADY = unchecked((int)0xC00D0FDC);

		[Description("Windows Media Player cannot perform the requested action. Your computer's time and date might not be set correctly.")]
		public const int NS_E_WMP_MLS_STALE_DATA = unchecked((int)0xC00D0FDD);

		[Description("The control (%s) does not support creation of sub-controls, yet (%d) sub-controls have been specified.")]
		public const int NS_E_WMP_UI_SUBCONTROLSNOTSUPPORTED = unchecked((int)0xC00D0FDE);

		[Description("Version mismatch: (%.1f required, %.1f found).")]
		public const int NS_E_WMP_UI_VERSIONMISMATCH = unchecked((int)0xC00D0FDF);

		[Description("The layout manager was given valid XML that wasn't a theme file.")]
		public const int NS_E_WMP_UI_NOTATHEMEFILE = unchecked((int)0xC00D0FE0);

		[Description("The %s subelement could not be found on the %s object.")]
		public const int NS_E_WMP_UI_SUBELEMENTNOTFOUND = unchecked((int)0xC00D0FE1);

		[Description("An error occurred parsing the version tag. Valid version tags are of the form: <?wmp version='1.0'?>.")]
		public const int NS_E_WMP_UI_VERSIONPARSE = unchecked((int)0xC00D0FE2);

		[Description("The view specified in for the 'currentViewID' property (%s) was not found in this theme file.")]
		public const int NS_E_WMP_UI_VIEWIDNOTFOUND = unchecked((int)0xC00D0FE3);

		[Description("This error used internally for hit testing.")]
		public const int NS_E_WMP_UI_PASSTHROUGH = unchecked((int)0xC00D0FE4);

		[Description("Attributes were specified for the %s object, but the object was not available to send them to.")]
		public const int NS_E_WMP_UI_OBJECTNOTFOUND = unchecked((int)0xC00D0FE5);

		[Description("The %s event already has a handler, the second handler was ignored.")]
		public const int NS_E_WMP_UI_SECONDHANDLER = unchecked((int)0xC00D0FE6);

		[Description("No .wms file found in skin archive.")]
		public const int NS_E_WMP_UI_NOSKININZIP = unchecked((int)0xC00D0FE7);

		[Description("Windows Media Player encountered a problem while downloading the file. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_URLDOWNLOADFAILED = unchecked((int)0xC00D0FEA);

		[Description("The Windows Media Player ActiveX control cannot load the requested uiMode and cannot roll back to the existing uiMode.")]
		public const int NS_E_WMPOCX_UNABLE_TO_LOAD_SKIN = unchecked((int)0xC00D0FEB);

		[Description("Windows Media Player encountered a problem with the skin file. The skin file might not be valid.")]
		public const int NS_E_WMP_INVALID_SKIN = unchecked((int)0xC00D0FEC);

		[Description("Windows Media Player cannot send the link because your email program is not responding. Verify that your email program is configured properly, and then try again. For more information about email, see Windows Help.")]
		public const int NS_E_WMP_SENDMAILFAILED = unchecked((int)0xC00D0FED);

		[Description("Windows Media Player cannot switch to full mode because your computer administrator has locked this skin.")]
		public const int NS_E_WMP_LOCKEDINSKINMODE = unchecked((int)0xC00D0FEE);

		[Description("Windows Media Player encountered a problem while saving the file. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_FAILED_TO_SAVE_FILE = unchecked((int)0xC00D0FEF);

		[Description("Windows Media Player cannot overwrite a read-only file. Try using a different file name.")]
		public const int NS_E_WMP_SAVEAS_READONLY = unchecked((int)0xC00D0FF0);

		[Description("Windows Media Player encountered a problem while creating or saving the playlist. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_FAILED_TO_SAVE_PLAYLIST = unchecked((int)0xC00D0FF1);

		[Description("Windows Media Player cannot open the Windows Media Download file. The file might be damaged.")]
		public const int NS_E_WMP_FAILED_TO_OPEN_WMD = unchecked((int)0xC00D0FF2);

		[Description("The file cannot be added to the library because it is a protected DVR-MS file. This content cannot be played back by Windows Media Player.")]
		public const int NS_E_WMP_CANT_PLAY_PROTECTED = unchecked((int)0xC00D0FF3);

		[Description("Media sharing has been turned off because a required Windows setting or component has changed. For additional assistance, click Web Help.")]
		public const int NS_E_SHARING_STATE_OUT_OF_SYNC = unchecked((int)0xC00D0FF4);

		[Description("Exclusive Services launch failed because the Windows Media Player is already running.")]
		public const int NS_E_WMPOCX_REMOTE_PLAYER_ALREADY_RUNNING = unchecked((int)0xC00D0FFA);

		[Description("JPG Images are not recommended for use as a mappingImage.")]
		public const int NS_E_WMP_RBC_JPGMAPPINGIMAGE = unchecked((int)0xC00D1004);

		[Description("JPG Images are not recommended when using a transparencyColor.")]
		public const int NS_E_WMP_JPGTRANSPARENCY = unchecked((int)0xC00D1005);

		[Description("The Max property cannot be less than Min property.")]
		public const int NS_E_WMP_INVALID_MAX_VAL = unchecked((int)0xC00D1009);

		[Description("The Min property cannot be greater than Max property.")]
		public const int NS_E_WMP_INVALID_MIN_VAL = unchecked((int)0xC00D100A);

		[Description("JPG Images are not recommended for use as a positionImage.")]
		public const int NS_E_WMP_CS_JPGPOSITIONIMAGE = unchecked((int)0xC00D100E);

		[Description("The (%s) image's size is not evenly divisible by the positionImage's size.")]
		public const int NS_E_WMP_CS_NOTEVENLYDIVISIBLE = unchecked((int)0xC00D100F);

		[Description("The ZIP reader opened a file and its signature did not match that of the ZIP files.")]
		public const int NS_E_WMPZIP_NOTAZIPFILE = unchecked((int)0xC00D1018);

		[Description("The ZIP reader has detected that the file is corrupted.")]
		public const int NS_E_WMPZIP_CORRUPT = unchecked((int)0xC00D1019);

		[Description("GetFileStream, SaveToFile, or SaveTemp file was called on the ZIP reader with a file name that was not found in the ZIP file.")]
		public const int NS_E_WMPZIP_FILENOTFOUND = unchecked((int)0xC00D101A);

		[Description("Image type not supported.")]
		public const int NS_E_WMP_IMAGE_FILETYPE_UNSUPPORTED = unchecked((int)0xC00D1022);

		[Description("Image file might be corrupt.")]
		public const int NS_E_WMP_IMAGE_INVALID_FORMAT = unchecked((int)0xC00D1023);

		[Description("Unexpected end of file. GIF file might be corrupt.")]
		public const int NS_E_WMP_GIF_UNEXPECTED_ENDOFFILE = unchecked((int)0xC00D1024);

		[Description("Invalid GIF file.")]
		public const int NS_E_WMP_GIF_INVALID_FORMAT = unchecked((int)0xC00D1025);

		[Description("Invalid GIF version. Only 87a or 89a supported.")]
		public const int NS_E_WMP_GIF_BAD_VERSION_NUMBER = unchecked((int)0xC00D1026);

		[Description("No images found in GIF file.")]
		public const int NS_E_WMP_GIF_NO_IMAGE_IN_FILE = unchecked((int)0xC00D1027);

		[Description("Invalid PNG image file format.")]
		public const int NS_E_WMP_PNG_INVALIDFORMAT = unchecked((int)0xC00D1028);

		[Description("PNG bitdepth not supported.")]
		public const int NS_E_WMP_PNG_UNSUPPORTED_BITDEPTH = unchecked((int)0xC00D1029);

		[Description("Compression format defined in PNG file not supported,")]
		public const int NS_E_WMP_PNG_UNSUPPORTED_COMPRESSION = unchecked((int)0xC00D102A);

		[Description("Filter method defined in PNG file not supported.")]
		public const int NS_E_WMP_PNG_UNSUPPORTED_FILTER = unchecked((int)0xC00D102B);

		[Description("Interlace method defined in PNG file not supported.")]
		public const int NS_E_WMP_PNG_UNSUPPORTED_INTERLACE = unchecked((int)0xC00D102C);

		[Description("Bad CRC in PNG file.")]
		public const int NS_E_WMP_PNG_UNSUPPORTED_BAD_CRC = unchecked((int)0xC00D102D);

		[Description("Invalid bitmask in BMP file.")]
		public const int NS_E_WMP_BMP_INVALID_BITMASK = unchecked((int)0xC00D102E);

		[Description("Topdown DIB not supported.")]
		public const int NS_E_WMP_BMP_TOPDOWN_DIB_UNSUPPORTED = unchecked((int)0xC00D102F);

		[Description("Bitmap could not be created.")]
		public const int NS_E_WMP_BMP_BITMAP_NOT_CREATED = unchecked((int)0xC00D1030);

		[Description("Compression format defined in BMP not supported.")]
		public const int NS_E_WMP_BMP_COMPRESSION_UNSUPPORTED = unchecked((int)0xC00D1031);

		[Description("Invalid Bitmap format.")]
		public const int NS_E_WMP_BMP_INVALID_FORMAT = unchecked((int)0xC00D1032);

		[Description("JPEG Arithmetic coding not supported.")]
		public const int NS_E_WMP_JPG_JERR_ARITHCODING_NOTIMPL = unchecked((int)0xC00D1033);

		[Description("Invalid JPEG format.")]
		public const int NS_E_WMP_JPG_INVALID_FORMAT = unchecked((int)0xC00D1034);

		[Description("Invalid JPEG format.")]
		public const int NS_E_WMP_JPG_BAD_DCTSIZE = unchecked((int)0xC00D1035);

		[Description("Internal version error. Unexpected JPEG library version.")]
		public const int NS_E_WMP_JPG_BAD_VERSION_NUMBER = unchecked((int)0xC00D1036);

		[Description("Internal JPEG Library error. Unsupported JPEG data precision.")]
		public const int NS_E_WMP_JPG_BAD_PRECISION = unchecked((int)0xC00D1037);

		[Description("JPEG CCIR601 not supported.")]
		public const int NS_E_WMP_JPG_CCIR601_NOTIMPL = unchecked((int)0xC00D1038);

		[Description("No image found in JPEG file.")]
		public const int NS_E_WMP_JPG_NO_IMAGE_IN_FILE = unchecked((int)0xC00D1039);

		[Description("Could not read JPEG file.")]
		public const int NS_E_WMP_JPG_READ_ERROR = unchecked((int)0xC00D103A);

		[Description("JPEG Fractional sampling not supported.")]
		public const int NS_E_WMP_JPG_FRACT_SAMPLE_NOTIMPL = unchecked((int)0xC00D103B);

		[Description("JPEG image too large. Maximum image size supported is 65500 X 65500.")]
		public const int NS_E_WMP_JPG_IMAGE_TOO_BIG = unchecked((int)0xC00D103C);

		[Description("Unexpected end of file reached in JPEG file.")]
		public const int NS_E_WMP_JPG_UNEXPECTED_ENDOFFILE = unchecked((int)0xC00D103D);

		[Description("Unsupported JPEG SOF marker found.")]
		public const int NS_E_WMP_JPG_SOF_UNSUPPORTED = unchecked((int)0xC00D103E);

		[Description("Unknown JPEG marker found.")]
		public const int NS_E_WMP_JPG_UNKNOWN_MARKER = unchecked((int)0xC00D103F);

		[Description("Windows Media Player cannot display the picture file. The player either does not support the picture type or the picture is corrupted.")]
		public const int NS_E_WMP_FAILED_TO_OPEN_IMAGE = unchecked((int)0xC00D1044);

		[Description("Windows Media Player cannot compute a Digital Audio Id for the song. It is too short.")]
		public const int NS_E_WMP_DAI_SONGTOOSHORT = unchecked((int)0xC00D1049);

		[Description("Windows Media Player cannot play the file at the requested speed.")]
		public const int NS_E_WMG_RATEUNAVAILABLE = unchecked((int)0xC00D104A);

		[Description("The rendering or digital signal processing plug-in cannot be instantiated.")]
		public const int NS_E_WMG_PLUGINUNAVAILABLE = unchecked((int)0xC00D104B);

		[Description("The file cannot be queued for seamless playback.")]
		public const int NS_E_WMG_CANNOTQUEUE = unchecked((int)0xC00D104C);

		[Description("Windows Media Player cannot download media usage rights for a file in the playlist.")]
		public const int NS_E_WMG_PREROLLLICENSEACQUISITIONNOTALLOWED = unchecked((int)0xC00D104D);

		[Description("Windows Media Player encountered an error while trying to queue a file.")]
		public const int NS_E_WMG_UNEXPECTEDPREROLLSTATUS = unchecked((int)0xC00D104E);

		[Description("Windows Media Player cannot play the protected file. The Player cannot verify that the connection to your video card is secure. Try installing an updated device driver for your video card.")]
		public const int NS_E_WMG_INVALID_COPP_CERTIFICATE = unchecked((int)0xC00D1051);

		[Description("Windows Media Player cannot play the protected file. The Player detected that the connection to your hardware might not be secure.")]
		public const int NS_E_WMG_COPP_SECURITY_INVALID = unchecked((int)0xC00D1052);

		[Description("Windows Media Player output link protection is unsupported on this system.")]
		public const int NS_E_WMG_COPP_UNSUPPORTED = unchecked((int)0xC00D1053);

		[Description("Operation attempted in an invalid graph state.")]
		public const int NS_E_WMG_INVALIDSTATE = unchecked((int)0xC00D1054);

		[Description("A renderer cannot be inserted in a stream while one already exists.")]
		public const int NS_E_WMG_SINKALREADYEXISTS = unchecked((int)0xC00D1055);

		[Description("The Windows Media SDK interface needed to complete the operation does not exist at this time.")]
		public const int NS_E_WMG_NOSDKINTERFACE = unchecked((int)0xC00D1056);

		[Description("Windows Media Player cannot play a portion of the file because it requires a codec that either could not be downloaded or that is not supported by the Player.")]
		public const int NS_E_WMG_NOTALLOUTPUTSRENDERED = unchecked((int)0xC00D1057);

		[Description("File transfer streams are not allowed in the standalone Player.")]
		public const int NS_E_WMG_FILETRANSFERNOTALLOWED = unchecked((int)0xC00D1058);

		[Description("Windows Media Player cannot play the file. The Player does not support the format you are trying to play.")]
		public const int NS_E_WMR_UNSUPPORTEDSTREAM = unchecked((int)0xC00D1059);

		[Description("An operation was attempted on a pin that does not exist in the DirectShow filter graph.")]
		public const int NS_E_WMR_PINNOTFOUND = unchecked((int)0xC00D105A);

		[Description("Specified operation cannot be completed while waiting for a media format change from the SDK.")]
		public const int NS_E_WMR_WAITINGONFORMATSWITCH = unchecked((int)0xC00D105B);

		[Description("Specified operation cannot be completed because the source filter does not exist.")]
		public const int NS_E_WMR_NOSOURCEFILTER = unchecked((int)0xC00D105C);

		[Description("The specified type does not match this pin.")]
		public const int NS_E_WMR_PINTYPENOMATCH = unchecked((int)0xC00D105D);

		[Description("The WMR Source Filter does not have a callback available.")]
		public const int NS_E_WMR_NOCALLBACKAVAILABLE = unchecked((int)0xC00D105E);

		[Description("The specified property has not been set on this sample.")]
		public const int NS_E_WMR_SAMPLEPROPERTYNOTSET = unchecked((int)0xC00D1062);

		[Description("A plug-in is required to correctly play the file. To determine if the plug-in is available to download, click Web Help.")]
		public const int NS_E_WMR_CANNOT_RENDER_BINARY_STREAM = unchecked((int)0xC00D1063);

		[Description("Windows Media Player cannot play the file because your media usage rights are corrupted. If you previously backed up your media usage rights, try restoring them.")]
		public const int NS_E_WMG_LICENSE_TAMPERED = unchecked((int)0xC00D1064);

		[Description("Windows Media Player cannot play protected files that contain binary streams.")]
		public const int NS_E_WMR_WILLNOT_RENDER_BINARY_STREAM = unchecked((int)0xC00D1065);

		[Description("Windows Media Player cannot play the playlist because it is not valid.")]
		public const int NS_E_WMX_UNRECOGNIZED_PLAYLIST_FORMAT = unchecked((int)0xC00D1068);

		[Description("Windows Media Player cannot play the playlist because it is not valid.")]
		public const int NS_E_ASX_INVALIDFORMAT = unchecked((int)0xC00D1069);

		[Description("A later version of Windows Media Player might be required to play this playlist.")]
		public const int NS_E_ASX_INVALIDVERSION = unchecked((int)0xC00D106A);

		[Description("The format of a REPEAT loop within the current playlist file is not valid.")]
		public const int NS_E_ASX_INVALID_REPEAT_BLOCK = unchecked((int)0xC00D106B);

		[Description("Windows Media Player cannot save the playlist because it does not contain any items.")]
		public const int NS_E_ASX_NOTHING_TO_WRITE = unchecked((int)0xC00D106C);

		[Description("Windows Media Player cannot play the playlist because it is not valid.")]
		public const int NS_E_URLLIST_INVALIDFORMAT = unchecked((int)0xC00D106D);

		[Description("The specified attribute does not exist.")]
		public const int NS_E_WMX_ATTRIBUTE_DOES_NOT_EXIST = unchecked((int)0xC00D106E);

		[Description("The specified attribute already exists.")]
		public const int NS_E_WMX_ATTRIBUTE_ALREADY_EXISTS = unchecked((int)0xC00D106F);

		[Description("Cannot retrieve the specified attribute.")]
		public const int NS_E_WMX_ATTRIBUTE_UNRETRIEVABLE = unchecked((int)0xC00D1070);

		[Description("The specified item does not exist in the current playlist.")]
		public const int NS_E_WMX_ITEM_DOES_NOT_EXIST = unchecked((int)0xC00D1071);

		[Description("Items of the specified type cannot be created within the current playlist.")]
		public const int NS_E_WMX_ITEM_TYPE_ILLEGAL = unchecked((int)0xC00D1072);

		[Description("The specified item cannot be set in the current playlist.")]
		public const int NS_E_WMX_ITEM_UNSETTABLE = unchecked((int)0xC00D1073);

		[Description("Windows Media Player cannot perform the requested action because the playlist does not contain any items.")]
		public const int NS_E_WMX_PLAYLIST_EMPTY = unchecked((int)0xC00D1074);

		[Description("The specified auto playlist contains a filter type that is either not valid or is not installed on this computer.")]
		public const int NS_E_MLS_SMARTPLAYLIST_FILTER_NOT_REGISTERED = unchecked((int)0xC00D1075);

		[Description("Windows Media Player cannot play the file because the associated playlist contains too many nested playlists.")]
		public const int NS_E_WMX_INVALID_FORMAT_OVER_NESTING = unchecked((int)0xC00D1076);

		[Description("Windows Media Player cannot find the file. Verify that the path is typed correctly. If it is, the file might not exist in the specified location, or the computer where the file is stored might not be available.")]
		public const int NS_E_WMPCORE_NOSOURCEURLSTRING = unchecked((int)0xC00D107C);

		[Description("Failed to create the Global Interface Table.")]
		public const int NS_E_WMPCORE_COCREATEFAILEDFORGITOBJECT = unchecked((int)0xC00D107D);

		[Description("Failed to get the marshaled graph event handler interface.")]
		public const int NS_E_WMPCORE_FAILEDTOGETMARSHALLEDEVENTHANDLERINTERFACE = unchecked((int)0xC00D107E);

		[Description("Buffer is too small for copying media type.")]
		public const int NS_E_WMPCORE_BUFFERTOOSMALL = unchecked((int)0xC00D107F);

		[Description("The current state of the Player does not allow this operation.")]
		public const int NS_E_WMPCORE_UNAVAILABLE = unchecked((int)0xC00D1080);

		[Description("The playlist manager does not understand the current play mode (for example, shuffle or normal).")]
		public const int NS_E_WMPCORE_INVALIDPLAYLISTMODE = unchecked((int)0xC00D1081);

		[Description("Windows Media Player cannot play the file because it is not in the current playlist.")]
		public const int NS_E_WMPCORE_ITEMNOTINPLAYLIST = unchecked((int)0xC00D1086);

		[Description("There are no items in the playlist. Add items to the playlist, and then try again.")]
		public const int NS_E_WMPCORE_PLAYLISTEMPTY = unchecked((int)0xC00D1087);

		[Description("The web page cannot be displayed because no web browser is installed on your computer.")]
		public const int NS_E_WMPCORE_NOBROWSER = unchecked((int)0xC00D1088);

		[Description("Windows Media Player cannot find the specified file. Verify the path is typed correctly. If it is, the file does not exist in the specified location, or the computer where the file is stored is not available.")]
		public const int NS_E_WMPCORE_UNRECOGNIZED_MEDIA_URL = unchecked((int)0xC00D1089);

		[Description("Graph with the specified URL was not found in the prerolled graph list.")]
		public const int NS_E_WMPCORE_GRAPH_NOT_IN_LIST = unchecked((int)0xC00D108A);

		[Description("Windows Media Player cannot perform the requested operation because there is only one item in the playlist.")]
		public const int NS_E_WMPCORE_PLAYLIST_EMPTY_OR_SINGLE_MEDIA = unchecked((int)0xC00D108B);

		[Description("An error sink was never registered for the calling object.")]
		public const int NS_E_WMPCORE_ERRORSINKNOTREGISTERED = unchecked((int)0xC00D108C);

		[Description("The error manager is not available to respond to errors.")]
		public const int NS_E_WMPCORE_ERRORMANAGERNOTAVAILABLE = unchecked((int)0xC00D108D);

		[Description("The Web Help URL cannot be opened.")]
		public const int NS_E_WMPCORE_WEBHELPFAILED = unchecked((int)0xC00D108E);

		[Description("Could not resume playing next item in playlist.")]
		public const int NS_E_WMPCORE_MEDIA_ERROR_RESUME_FAILED = unchecked((int)0xC00D108F);

		[Description("Windows Media Player cannot play the file because the associated playlist does not contain any items or the playlist is not valid.")]
		public const int NS_E_WMPCORE_NO_REF_IN_ENTRY = unchecked((int)0xC00D1090);

		[Description("An empty string for playlist attribute name was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ATTRIBUTE_NAME_EMPTY = unchecked((int)0xC00D1091);

		[Description("A playlist attribute name that is not valid was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ATTRIBUTE_NAME_ILLEGAL = unchecked((int)0xC00D1092);

		[Description("An empty string for a playlist attribute value was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ATTRIBUTE_VALUE_EMPTY = unchecked((int)0xC00D1093);

		[Description("An illegal value for a playlist attribute was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ATTRIBUTE_VALUE_ILLEGAL = unchecked((int)0xC00D1094);

		[Description("An empty string for a playlist item attribute name was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ITEM_ATTRIBUTE_NAME_EMPTY = unchecked((int)0xC00D1095);

		[Description("An illegal value for a playlist item attribute name was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ITEM_ATTRIBUTE_NAME_ILLEGAL = unchecked((int)0xC00D1096);

		[Description("An illegal value for a playlist item attribute was found.")]
		public const int NS_E_WMPCORE_WMX_LIST_ITEM_ATTRIBUTE_VALUE_EMPTY = unchecked((int)0xC00D1097);

		[Description("The playlist does not contain any items.")]
		public const int NS_E_WMPCORE_LIST_ENTRY_NO_REF = unchecked((int)0xC00D1098);

		[Description("Windows Media Player cannot play the file. The file is either corrupted or the Player does not support the format you are trying to play.")]
		public const int NS_E_WMPCORE_MISNAMED_FILE = unchecked((int)0xC00D1099);

		[Description("The codec downloaded for this file does not appear to be properly signed, so it cannot be installed.")]
		public const int NS_E_WMPCORE_CODEC_NOT_TRUSTED = unchecked((int)0xC00D109A);

		[Description("Windows Media Player cannot play the file. One or more codecs required to play the file could not be found.")]
		public const int NS_E_WMPCORE_CODEC_NOT_FOUND = unchecked((int)0xC00D109B);

		[Description("Windows Media Player cannot play the file because a required codec is not installed on your computer. To try downloading the codec, turn on the \"Download codecs automatically\" option.")]
		public const int NS_E_WMPCORE_CODEC_DOWNLOAD_NOT_ALLOWED = unchecked((int)0xC00D109C);

		[Description("Windows Media Player encountered a problem while downloading the playlist. For additional assistance, click Web Help.")]
		public const int NS_E_WMPCORE_ERROR_DOWNLOADING_PLAYLIST = unchecked((int)0xC00D109D);

		[Description("Failed to build the playlist.")]
		public const int NS_E_WMPCORE_FAILED_TO_BUILD_PLAYLIST = unchecked((int)0xC00D109E);

		[Description("Playlist has no alternates to switch into.")]
		public const int NS_E_WMPCORE_PLAYLIST_ITEM_ALTERNATE_NONE = unchecked((int)0xC00D109F);

		[Description("No more playlist alternates available to switch to.")]
		public const int NS_E_WMPCORE_PLAYLIST_ITEM_ALTERNATE_EXHAUSTED = unchecked((int)0xC00D10A0);

		[Description("Could not find the name of the alternate playlist to switch into.")]
		public const int NS_E_WMPCORE_PLAYLIST_ITEM_ALTERNATE_NAME_NOT_FOUND = unchecked((int)0xC00D10A1);

		[Description("Failed to switch to an alternate for this media.")]
		public const int NS_E_WMPCORE_PLAYLIST_ITEM_ALTERNATE_MORPH_FAILED = unchecked((int)0xC00D10A2);

		[Description("Failed to initialize an alternate for the media.")]
		public const int NS_E_WMPCORE_PLAYLIST_ITEM_ALTERNATE_INIT_FAILED = unchecked((int)0xC00D10A3);

		[Description("No URL specified for the roll over Refs in the playlist file.")]
		public const int NS_E_WMPCORE_MEDIA_ALTERNATE_REF_EMPTY = unchecked((int)0xC00D10A4);

		[Description("Encountered a playlist with no name.")]
		public const int NS_E_WMPCORE_PLAYLIST_NO_EVENT_NAME = unchecked((int)0xC00D10A5);

		[Description("A required attribute in the event block of the playlist was not found.")]
		public const int NS_E_WMPCORE_PLAYLIST_EVENT_ATTRIBUTE_ABSENT = unchecked((int)0xC00D10A6);

		[Description("No items were found in the event block of the playlist.")]
		public const int NS_E_WMPCORE_PLAYLIST_EVENT_EMPTY = unchecked((int)0xC00D10A7);

		[Description("No playlist was found while returning from a nested playlist.")]
		public const int NS_E_WMPCORE_PLAYLIST_STACK_EMPTY = unchecked((int)0xC00D10A8);

		[Description("The media item is not active currently.")]
		public const int NS_E_WMPCORE_CURRENT_MEDIA_NOT_ACTIVE = unchecked((int)0xC00D10A9);

		[Description("Windows Media Player cannot perform the requested action because you chose to cancel it.")]
		public const int NS_E_WMPCORE_USER_CANCEL = unchecked((int)0xC00D10AB);

		[Description("Windows Media Player encountered a problem with the playlist. The format of the playlist is not valid.")]
		public const int NS_E_WMPCORE_PLAYLIST_REPEAT_EMPTY = unchecked((int)0xC00D10AC);

		[Description("Media object corresponding to start of a playlist repeat block was not found.")]
		public const int NS_E_WMPCORE_PLAYLIST_REPEAT_START_MEDIA_NONE = unchecked((int)0xC00D10AD);

		[Description("Media object corresponding to the end of a playlist repeat block was not found.")]
		public const int NS_E_WMPCORE_PLAYLIST_REPEAT_END_MEDIA_NONE = unchecked((int)0xC00D10AE);

		[Description("The playlist URL supplied to the playlist manager is not valid.")]
		public const int NS_E_WMPCORE_INVALID_PLAYLIST_URL = unchecked((int)0xC00D10AF);

		[Description("Windows Media Player cannot play the file because it is corrupted.")]
		public const int NS_E_WMPCORE_MISMATCHED_RUNTIME = unchecked((int)0xC00D10B0);

		[Description("Windows Media Player cannot add the playlist to the library because the playlist does not contain any items.")]
		public const int NS_E_WMPCORE_PLAYLIST_IMPORT_FAILED_NO_ITEMS = unchecked((int)0xC00D10B1);

		[Description("An error has occurred that could prevent the changing of the video contrast on this media.")]
		public const int NS_E_WMPCORE_VIDEO_TRANSFORM_FILTER_INSERTION = unchecked((int)0xC00D10B2);

		[Description("Windows Media Player cannot play the file. If the file is located on the Internet, connect to the Internet. If the file is located on a removable storage card, insert the storage card.")]
		public const int NS_E_WMPCORE_MEDIA_UNAVAILABLE = unchecked((int)0xC00D10B3);

		[Description("The playlist contains an ENTRYREF for which no href was parsed. Check the syntax of playlist file.")]
		public const int NS_E_WMPCORE_WMX_ENTRYREF_NO_REF = unchecked((int)0xC00D10B4);

		[Description("Windows Media Player cannot play any items in the playlist. To find information about the problem, click the Now Playing tab, and then click the icon next to each file in the List pane.")]
		public const int NS_E_WMPCORE_NO_PLAYABLE_MEDIA_IN_PLAYLIST = unchecked((int)0xC00D10B5);

		[Description("Windows Media Player cannot play some or all of the items in the playlist because the playlist is nested.")]
		public const int NS_E_WMPCORE_PLAYLIST_EMPTY_NESTED_PLAYLIST_SKIPPED_ITEMS = unchecked((int)0xC00D10B6);

		[Description("Windows Media Player cannot play the file at this time. Try again later.")]
		public const int NS_E_WMPCORE_BUSY = unchecked((int)0xC00D10B7);

		[Description("There is no child playlist available for this media item at this time.")]
		public const int NS_E_WMPCORE_MEDIA_CHILD_PLAYLIST_UNAVAILABLE = unchecked((int)0xC00D10B8);

		[Description("There is no child playlist for this media item.")]
		public const int NS_E_WMPCORE_MEDIA_NO_CHILD_PLAYLIST = unchecked((int)0xC00D10B9);

		[Description("Windows Media Player cannot find the file. The link from the item in the library to its associated digital media file might be broken. To fix the problem, try repairing the link or removing the item from the library.")]
		public const int NS_E_WMPCORE_FILE_NOT_FOUND = unchecked((int)0xC00D10BA);

		[Description("The temporary file was not found.")]
		public const int NS_E_WMPCORE_TEMP_FILE_NOT_FOUND = unchecked((int)0xC00D10BB);

		[Description("Windows Media Player cannot sync the file because the device needs to be updated.")]
		public const int NS_E_WMDM_REVOKED = unchecked((int)0xC00D10BC);

		[Description("Windows Media Player cannot play the video because there is a problem with your video card.")]
		public const int NS_E_DDRAW_GENERIC = unchecked((int)0xC00D10BD);

		[Description("Windows Media Player failed to change the screen mode for full-screen video playback.")]
		public const int NS_E_DISPLAY_MODE_CHANGE_FAILED = unchecked((int)0xC00D10BE);

		[Description("Windows Media Player cannot play one or more files. For additional information, right-click an item that cannot be played, and then click Error Details.")]
		public const int NS_E_PLAYLIST_CONTAINS_ERRORS = unchecked((int)0xC00D10BF);

		[Description("Cannot change the proxy name if the proxy setting is not set to custom.")]
		public const int NS_E_CHANGING_PROXY_NAME = unchecked((int)0xC00D10C0);

		[Description("Cannot change the proxy port if the proxy setting is not set to custom.")]
		public const int NS_E_CHANGING_PROXY_PORT = unchecked((int)0xC00D10C1);

		[Description("Cannot change the proxy exception list if the proxy setting is not set to custom.")]
		public const int NS_E_CHANGING_PROXY_EXCEPTIONLIST = unchecked((int)0xC00D10C2);

		[Description("Cannot change the proxy bypass flag if the proxy setting is not set to custom.")]
		public const int NS_E_CHANGING_PROXYBYPASS = unchecked((int)0xC00D10C3);

		[Description("Cannot find the specified protocol.")]
		public const int NS_E_CHANGING_PROXY_PROTOCOL_NOT_FOUND = unchecked((int)0xC00D10C4);

		[Description("Cannot change the language settings. Either the graph has no audio or the audio only supports one language.")]
		public const int NS_E_GRAPH_NOAUDIOLANGUAGE = unchecked((int)0xC00D10C5);

		[Description("The graph has no audio language selected.")]
		public const int NS_E_GRAPH_NOAUDIOLANGUAGESELECTED = unchecked((int)0xC00D10C6);

		[Description("This is not a media CD.")]
		public const int NS_E_CORECD_NOTAMEDIACD = unchecked((int)0xC00D10C7);

		[Description("Windows Media Player cannot play the file because the URL is too long.")]
		public const int NS_E_WMPCORE_MEDIA_URL_TOO_LONG = unchecked((int)0xC00D10C8);

		[Description("To play the selected item, you must install the Macromedia Flash Player. To download the Macromedia Flash Player, go to the Adobe website.")]
		public const int NS_E_WMPFLASH_CANT_FIND_COM_SERVER = unchecked((int)0xC00D10C9);

		[Description("To play the selected item, you must install a later version of the Macromedia Flash Player. To download the Macromedia Flash Player, go to the Adobe website.")]
		public const int NS_E_WMPFLASH_INCOMPATIBLEVERSION = unchecked((int)0xC00D10CA);

		[Description("Windows Media Player cannot play the file because your Internet security settings prohibit the use of ActiveX controls.")]
		public const int NS_E_WMPOCXGRAPH_IE_DISALLOWS_ACTIVEX_CONTROLS = unchecked((int)0xC00D10CB);

		[Description("The use of this method requires an existing reference to the Player object.")]
		public const int NS_E_NEED_CORE_REFERENCE = unchecked((int)0xC00D10CC);

		[Description("Windows Media Player cannot play the CD. The disc might be dirty or damaged.")]
		public const int NS_E_MEDIACD_READ_ERROR = unchecked((int)0xC00D10CD);

		[Description("Windows Media Player cannot play the file because your Internet security settings prohibit the use of ActiveX controls.")]
		public const int NS_E_IE_DISALLOWS_ACTIVEX_CONTROLS = unchecked((int)0xC00D10CE);

		[Description("Flash playback has been turned off in Windows Media Player.")]
		public const int NS_E_FLASH_PLAYBACK_NOT_ALLOWED = unchecked((int)0xC00D10CF);

		[Description("Windows Media Player cannot rip the CD because a valid rip location cannot be created.")]
		public const int NS_E_UNABLE_TO_CREATE_RIP_LOCATION = unchecked((int)0xC00D10D0);

		[Description("Windows Media Player cannot play the file because a required codec is not installed on your computer.")]
		public const int NS_E_WMPCORE_SOME_CODECS_MISSING = unchecked((int)0xC00D10D1);

		[Description("Windows Media Player cannot rip one or more tracks from the CD.")]
		public const int NS_E_WMP_RIP_FAILED = unchecked((int)0xC00D10D2);

		[Description("Windows Media Player encountered a problem while ripping the track from the CD. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_FAILED_TO_RIP_TRACK = unchecked((int)0xC00D10D3);

		[Description("Windows Media Player encountered a problem while erasing the disc. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_ERASE_FAILED = unchecked((int)0xC00D10D4);

		[Description("Windows Media Player encountered a problem while formatting the device. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_FORMAT_FAILED = unchecked((int)0xC00D10D5);

		[Description("This file cannot be burned to a CD because it is not located on your computer.")]
		public const int NS_E_WMP_CANNOT_BURN_NON_LOCAL_FILE = unchecked((int)0xC00D10D6);

		[Description("It is not possible to burn this file type to an audio CD. Windows Media Player can burn the following file types to an audio CD: WMA, MP3, or WAV.")]
		public const int NS_E_WMP_FILE_TYPE_CANNOT_BURN_TO_AUDIO_CD = unchecked((int)0xC00D10D7);

		[Description("This file is too large to fit on a disc.")]
		public const int NS_E_WMP_FILE_DOES_NOT_FIT_ON_CD = unchecked((int)0xC00D10D8);

		[Description("It is not possible to determine if this file can fit on a disc because Windows Media Player cannot detect the length of the file. Playing the file before burning might enable the Player to detect the file length.")]
		public const int NS_E_WMP_FILE_NO_DURATION = unchecked((int)0xC00D10D9);

		[Description("Windows Media Player encountered a problem while burning the file to the disc. For additional assistance, click Web Help.")]
		public const int NS_E_PDA_FAILED_TO_BURN = unchecked((int)0xC00D10DA);

		[Description("Windows Media Player cannot burn the audio CD because some items in the list that you chose to buy could not be downloaded from the online store.")]
		public const int NS_E_FAILED_DOWNLOAD_ABORT_BURN = unchecked((int)0xC00D10DC);

		[Description("Windows Media Player cannot play the file. Try using Windows Update or Device Manager to update the device drivers for your audio and video cards. For information about using Windows Update or Device Manager, see Windows Help.")]
		public const int NS_E_WMPCORE_DEVICE_DRIVERS_MISSING = unchecked((int)0xC00D10DD);

		[Description("Windows Media Player has detected that you are not connected to the Internet. Connect to the Internet, and then try again.")]
		public const int NS_E_WMPIM_USEROFFLINE = unchecked((int)0xC00D1126);

		[Description("The attempt to connect to the Internet was canceled.")]
		public const int NS_E_WMPIM_USERCANCELED = unchecked((int)0xC00D1127);

		[Description("The attempt to connect to the Internet failed.")]
		public const int NS_E_WMPIM_DIALUPFAILED = unchecked((int)0xC00D1128);

		[Description("Windows Media Player has encountered an unknown network error.")]
		public const int NS_E_WINSOCK_ERROR_STRING = unchecked((int)0xC00D1129);

		[Description("No window is currently listening to Backup and Restore events.")]
		public const int NS_E_WMPBR_NOLISTENER = unchecked((int)0xC00D1130);

		[Description("Your media usage rights were not backed up because the backup was canceled.")]
		public const int NS_E_WMPBR_BACKUPCANCEL = unchecked((int)0xC00D1131);

		[Description("Your media usage rights were not restored because the restoration was canceled.")]
		public const int NS_E_WMPBR_RESTORECANCEL = unchecked((int)0xC00D1132);

		[Description("An error occurred while backing up or restoring your media usage rights. A required web page cannot be displayed.")]
		public const int NS_E_WMPBR_ERRORWITHURL = unchecked((int)0xC00D1133);

		[Description("Your media usage rights were not backed up because the backup was canceled.")]
		public const int NS_E_WMPBR_NAMECOLLISION = unchecked((int)0xC00D1134);

		[Description("Windows Media Player cannot restore your media usage rights from the specified location. Choose another location, and then try again.")]
		public const int NS_E_WMPBR_DRIVE_INVALID = unchecked((int)0xC00D1137);

		[Description("Windows Media Player cannot backup or restore your media usage rights.")]
		public const int NS_E_WMPBR_BACKUPRESTOREFAILED = unchecked((int)0xC00D1138);

		[Description("Windows Media Player cannot add the file to the library.")]
		public const int NS_E_WMP_CONVERT_FILE_FAILED = unchecked((int)0xC00D1158);

		[Description("Windows Media Player cannot add the file to the library because the content provider prohibits it. For assistance, contact the company that provided the file.")]
		public const int NS_E_WMP_CONVERT_NO_RIGHTS_ERRORURL = unchecked((int)0xC00D1159);

		[Description("Windows Media Player cannot add the file to the library because the content provider prohibits it. For assistance, contact the company that provided the file.")]
		public const int NS_E_WMP_CONVERT_NO_RIGHTS_NOERRORURL = unchecked((int)0xC00D115A);

		[Description("Windows Media Player cannot add the file to the library. The file might not be valid.")]
		public const int NS_E_WMP_CONVERT_FILE_CORRUPT = unchecked((int)0xC00D115B);

		[Description("Windows Media Player cannot add the file to the library. The plug-in required to add the file is not installed properly. For assistance, click Web Help to display the website of the company that provided the file.")]
		public const int NS_E_WMP_CONVERT_PLUGIN_UNAVAILABLE_ERRORURL = unchecked((int)0xC00D115C);

		[Description("Windows Media Player cannot add the file to the library. The plug-in required to add the file is not installed properly. For assistance, contact the company that provided the file.")]
		public const int NS_E_WMP_CONVERT_PLUGIN_UNAVAILABLE_NOERRORURL = unchecked((int)0xC00D115D);

		[Description("Windows Media Player cannot add the file to the library. The plug-in required to add the file is not installed properly. For assistance, contact the company that provided the file.")]
		public const int NS_E_WMP_CONVERT_PLUGIN_UNKNOWN_FILE_OWNER = unchecked((int)0xC00D115E);

		[Description("Windows Media Player cannot play this DVD. Try installing an updated driver for your video card or obtaining a newer video card.")]
		public const int NS_E_DVD_DISC_COPY_PROTECT_OUTPUT_NS = unchecked((int)0xC00D1160);

		[Description("This DVD's resolution exceeds the maximum allowed by your component video outputs. Try reducing your screen resolution to 640 x 480, or turn off analog component outputs and use a VGA connection to your monitor.")]
		public const int NS_E_DVD_DISC_COPY_PROTECT_OUTPUT_FAILED = unchecked((int)0xC00D1161);

		[Description("Windows Media Player cannot display subtitles or highlights in DVD menus. Reinstall the DVD decoder or contact the DVD drive manufacturer to obtain an updated decoder.")]
		public const int NS_E_DVD_NO_SUBPICTURE_STREAM = unchecked((int)0xC00D1162);

		[Description("Windows Media Player cannot play this DVD because there is a problem with digital copy protection between your DVD drive, decoder, and video card. Try installing an updated driver for your video card.")]
		public const int NS_E_DVD_COPY_PROTECT = unchecked((int)0xC00D1163);

		[Description("Windows Media Player cannot play the DVD. The disc was created in a manner that the Player does not support.")]
		public const int NS_E_DVD_AUTHORING_PROBLEM = unchecked((int)0xC00D1164);

		[Description("Windows Media Player cannot play the DVD because the disc prohibits playback in your region of the world. You must obtain a disc that is intended for your geographic region.")]
		public const int NS_E_DVD_INVALID_DISC_REGION = unchecked((int)0xC00D1165);

		[Description("Windows Media Player cannot play the DVD because your video card does not support DVD playback.")]
		public const int NS_E_DVD_COMPATIBLE_VIDEO_CARD = unchecked((int)0xC00D1166);

		[Description("Windows Media Player cannot play this DVD because it is not possible to turn on analog copy protection on the output display. Try installing an updated driver for your video card.")]
		public const int NS_E_DVD_MACROVISION = unchecked((int)0xC00D1167);

		[Description("Windows Media Player cannot play the DVD because the region assigned to your DVD drive does not match the region assigned to your DVD decoder.")]
		public const int NS_E_DVD_SYSTEM_DECODER_REGION = unchecked((int)0xC00D1168);

		[Description("Windows Media Player cannot play the DVD because the disc prohibits playback in your region of the world. You must obtain a disc that is intended for your geographic region.")]
		public const int NS_E_DVD_DISC_DECODER_REGION = unchecked((int)0xC00D1169);

		[Description("Windows Media Player cannot play DVD video. You might need to adjust your Windows display settings. Open display settings in Control Panel, and then try lowering your screen resolution and color quality settings.")]
		public const int NS_E_DVD_NO_VIDEO_STREAM = unchecked((int)0xC00D116A);

		[Description("Windows Media Player cannot play DVD audio. Verify that your sound card is set up correctly, and then try again.")]
		public const int NS_E_DVD_NO_AUDIO_STREAM = unchecked((int)0xC00D116B);

		[Description("Windows Media Player cannot play DVD video. Close any open files and quit any other programs, and then try again. If the problem persists, restart your computer.")]
		public const int NS_E_DVD_GRAPH_BUILDING = unchecked((int)0xC00D116C);

		[Description("Windows Media Player cannot play the DVD because a compatible DVD decoder is not installed on your computer.")]
		public const int NS_E_DVD_NO_DECODER = unchecked((int)0xC00D116D);

		[Description("Windows Media Player cannot play the scene because it has a parental rating higher than the rating that you are authorized to view.")]
		public const int NS_E_DVD_PARENTAL = unchecked((int)0xC00D116E);

		[Description("Windows Media Player cannot skip to the requested location on the DVD.")]
		public const int NS_E_DVD_CANNOT_JUMP = unchecked((int)0xC00D116F);

		[Description("Windows Media Player cannot play the DVD because it is currently in use by another program. Quit the other program that is using the DVD, and then try again.")]
		public const int NS_E_DVD_DEVICE_CONTENTION = unchecked((int)0xC00D1170);

		[Description("Windows Media Player cannot play DVD video. You might need to adjust your Windows display settings. Open display settings in Control Panel, and then try lowering your screen resolution and color quality settings.")]
		public const int NS_E_DVD_NO_VIDEO_MEMORY = unchecked((int)0xC00D1171);

		[Description("Windows Media Player cannot rip the DVD because it is copy protected.")]
		public const int NS_E_DVD_CANNOT_COPY_PROTECTED = unchecked((int)0xC00D1172);

		[Description("One of more of the required properties has not been set.")]
		public const int NS_E_DVD_REQUIRED_PROPERTY_NOT_SET = unchecked((int)0xC00D1173);

		[Description("The specified title and/or chapter number does not exist on this DVD.")]
		public const int NS_E_DVD_INVALID_TITLE_CHAPTER = unchecked((int)0xC00D1174);

		[Description("Windows Media Player cannot burn the files because the Player cannot find a burner. If the burner is connected properly, try using Windows Update to install the latest device driver.")]
		public const int NS_E_NO_CD_BURNER = unchecked((int)0xC00D1176);

		[Description("Windows Media Player does not detect storage media in the selected device. Insert storage media into the device, and then try again.")]
		public const int NS_E_DEVICE_IS_NOT_READY = unchecked((int)0xC00D1177);

		[Description("Windows Media Player cannot sync this file. The Player might not support the file type.")]
		public const int NS_E_PDA_UNSUPPORTED_FORMAT = unchecked((int)0xC00D1178);

		[Description("Windows Media Player does not detect a portable device. Connect your portable device, and then try again.")]
		public const int NS_E_NO_PDA = unchecked((int)0xC00D1179);

		[Description("Windows Media Player encountered an error while communicating with the device. The storage card on the device might be full, the device might be turned off, or the device might not allow playlists or folders to be created on it.")]
		public const int NS_E_PDA_UNSPECIFIED_ERROR = unchecked((int)0xC00D117A);

		[Description("Windows Media Player encountered an error while burning a CD.")]
		public const int NS_E_MEMSTORAGE_BAD_DATA = unchecked((int)0xC00D117B);

		[Description("Windows Media Player encountered an error while communicating with a portable device or CD drive.")]
		public const int NS_E_PDA_FAIL_SELECT_DEVICE = unchecked((int)0xC00D117C);

		[Description("Windows Media Player cannot open the WAV file.")]
		public const int NS_E_PDA_FAIL_READ_WAVE_FILE = unchecked((int)0xC00D117D);

		[Description("Windows Media Player failed to burn all the files to the CD. Select a slower recording speed, and then try again.")]
		public const int NS_E_IMAPI_LOSSOFSTREAMING = unchecked((int)0xC00D117E);

		[Description("There is not enough storage space on the portable device to complete this operation. Delete some unneeded files on the portable device, and then try again.")]
		public const int NS_E_PDA_DEVICE_FULL = unchecked((int)0xC00D117F);

		[Description("Windows Media Player cannot burn the files. Verify that your burner is connected properly, and then try again. If the problem persists, reinstall the Player.")]
		public const int NS_E_FAIL_LAUNCH_ROXIO_PLUGIN = unchecked((int)0xC00D1180);

		[Description("Windows Media Player did not sync some files to the device because there is not enough storage space on the device.")]
		public const int NS_E_PDA_DEVICE_FULL_IN_SESSION = unchecked((int)0xC00D1181);

		[Description("The disc in the burner is not valid. Insert a blank disc into the burner, and then try again.")]
		public const int NS_E_IMAPI_MEDIUM_INVALIDTYPE = unchecked((int)0xC00D1182);

		[Description("Windows Media Player cannot perform the requested action because the device does not support sync.")]
		public const int NS_E_PDA_MANUALDEVICE = unchecked((int)0xC00D1183);

		[Description("To perform the requested action, you must first set up sync with the device.")]
		public const int NS_E_PDA_PARTNERSHIPNOTEXIST = unchecked((int)0xC00D1184);

		[Description("You have already created sync partnerships with 16 devices. To create a new sync partnership, you must first end an existing partnership.")]
		public const int NS_E_PDA_CANNOT_CREATE_ADDITIONAL_SYNC_RELATIONSHIP = unchecked((int)0xC00D1185);

		[Description("Windows Media Player cannot sync the file because protected files cannot be converted to the required quality level or file format.")]
		public const int NS_E_PDA_NO_TRANSCODE_OF_DRM = unchecked((int)0xC00D1186);

		[Description("The folder that stores converted files is full. Either empty the folder or increase its size, and then try again.")]
		public const int NS_E_PDA_TRANSCODECACHEFULL = unchecked((int)0xC00D1187);

		[Description("There are too many files with the same name in the folder on the device. Change the file name or sync to a different folder.")]
		public const int NS_E_PDA_TOO_MANY_FILE_COLLISIONS = unchecked((int)0xC00D1188);

		[Description("Windows Media Player cannot convert the file to the format required by the device.")]
		public const int NS_E_PDA_CANNOT_TRANSCODE = unchecked((int)0xC00D1189);

		[Description("You have reached the maximum number of files your device allows in a folder. If your device supports playback from subfolders, try creating subfolders on the device and storing some files in them.")]
		public const int NS_E_PDA_TOO_MANY_FILES_IN_DIRECTORY = unchecked((int)0xC00D118A);

		[Description("Windows Media Player is already trying to start the Device Setup Wizard.")]
		public const int NS_E_PROCESSINGSHOWSYNCWIZARD = unchecked((int)0xC00D118B);

		[Description("Windows Media Player cannot convert this file format. If an updated version of the codec used to compress this file is available, install it and then try to sync the file again.")]
		public const int NS_E_PDA_TRANSCODE_NOT_PERMITTED = unchecked((int)0xC00D118C);

		[Description("Windows Media Player is busy setting up devices. Try again later.")]
		public const int NS_E_PDA_INITIALIZINGDEVICES = unchecked((int)0xC00D118D);

		[Description("Your device is using an outdated driver that is no longer supported by Windows Media Player. For additional assistance, click Web Help.")]
		public const int NS_E_PDA_OBSOLETE_SP = unchecked((int)0xC00D118E);

		[Description("Windows Media Player cannot sync the file because a file with the same name already exists on the device. Change the file name or try to sync the file to a different folder.")]
		public const int NS_E_PDA_TITLE_COLLISION = unchecked((int)0xC00D118F);

		[Description("Automatic and manual sync have been turned off temporarily. To sync to a device, restart Windows Media Player.")]
		public const int NS_E_PDA_DEVICESUPPORTDISABLED = unchecked((int)0xC00D1190);

		[Description("This device is not available. Connect the device to the computer, and then try again.")]
		public const int NS_E_PDA_NO_LONGER_AVAILABLE = unchecked((int)0xC00D1191);

		[Description("Windows Media Player cannot sync the file because an error occurred while converting the file to another quality level or format. If the problem persists, remove the file from the list of files to sync.")]
		public const int NS_E_PDA_ENCODER_NOT_RESPONDING = unchecked((int)0xC00D1192);

		[Description("Windows Media Player cannot sync the file to your device. The file might be stored in a location that is not supported. Copy the file from its current location to your hard disk, add it to your library, and then try to sync the file again.")]
		public const int NS_E_PDA_CANNOT_SYNC_FROM_LOCATION = unchecked((int)0xC00D1193);

		[Description("Windows Media Player cannot open the specified URL. Verify that the Player is configured to use all available protocols, and then try again.")]
		public const int NS_E_WMP_PROTOCOL_PROBLEM = unchecked((int)0xC00D1194);

		[Description("Windows Media Player cannot perform the requested action because there is not enough storage space on your computer. Delete some unneeded files on your hard disk, and then try again.")]
		public const int NS_E_WMP_NO_DISK_SPACE = unchecked((int)0xC00D1195);

		[Description("The server denied access to the file. Verify that you are using the correct user name and password.")]
		public const int NS_E_WMP_LOGON_FAILURE = unchecked((int)0xC00D1196);

		[Description("Windows Media Player cannot find the file. If you are trying to play, burn, or sync an item that is in your library, the item might point to a file that has been moved, renamed, or deleted.")]
		public const int NS_E_WMP_CANNOT_FIND_FILE = unchecked((int)0xC00D1197);

		[Description("Windows Media Player cannot connect to the server. The server name might not be correct, the server might not be available, or your proxy settings might not be correct.")]
		public const int NS_E_WMP_SERVER_INACCESSIBLE = unchecked((int)0xC00D1198);

		[Description("Windows Media Player cannot play the file. The Player might not support the file type or might not support the codec that was used to compress the file.")]
		public const int NS_E_WMP_UNSUPPORTED_FORMAT = unchecked((int)0xC00D1199);

		[Description("Windows Media Player cannot play the file. The Player might not support the file type or a required codec might not be installed on your computer.")]
		public const int NS_E_WMP_DSHOW_UNSUPPORTED_FORMAT = unchecked((int)0xC00D119A);

		[Description("Windows Media Player cannot create the playlist because the name already exists. Type a different playlist name.")]
		public const int NS_E_WMP_PLAYLIST_EXISTS = unchecked((int)0xC00D119B);

		[Description("Windows Media Player cannot delete the playlist because it contains items that are not digital media files. Any digital media files in the playlist were deleted.")]
		public const int NS_E_WMP_NONMEDIA_FILES = unchecked((int)0xC00D119C);

		[Description("The playlist cannot be opened because it is stored in a shared folder on another computer. If possible, move the playlist to the playlists folder on your computer.")]
		public const int NS_E_WMP_INVALID_ASX = unchecked((int)0xC00D119D);

		[Description("Windows Media Player is already in use. Stop playing any items, close all Player dialog boxes, and then try again.")]
		public const int NS_E_WMP_ALREADY_IN_USE = unchecked((int)0xC00D119E);

		[Description("Windows Media Player encountered an error while burning. Verify that the burner is connected properly and that the disc is clean and not damaged.")]
		public const int NS_E_WMP_IMAPI_FAILURE = unchecked((int)0xC00D119F);

		[Description("Windows Media Player has encountered an unknown error with your portable device. Reconnect your portable device, and then try again.")]
		public const int NS_E_WMP_WMDM_FAILURE = unchecked((int)0xC00D11A0);

		[Description("A codec is required to play this file. To determine if this codec is available to download from the web, click Web Help.")]
		public const int NS_E_WMP_CODEC_NEEDED_WITH_4CC = unchecked((int)0xC00D11A1);

		[Description("An audio codec is needed to play this file. To determine if this codec is available to download from the web, click Web Help.")]
		public const int NS_E_WMP_CODEC_NEEDED_WITH_FORMATTAG = unchecked((int)0xC00D11A2);

		[Description("To play the file, you must install the latest Windows service pack. To install the service pack from the Windows Update website, click Web Help.")]
		public const int NS_E_WMP_MSSAP_NOT_AVAILABLE = unchecked((int)0xC00D11A3);

		[Description("Windows Media Player no longer detects a portable device. Reconnect your portable device, and then try again.")]
		public const int NS_E_WMP_WMDM_INTERFACEDEAD = unchecked((int)0xC00D11A4);

		[Description("Windows Media Player cannot sync the file because the portable device does not support protected files.")]
		public const int NS_E_WMP_WMDM_NOTCERTIFIED = unchecked((int)0xC00D11A5);

		[Description("This file does not have sync rights. If you obtained this file from an online store, go to the online store to get sync rights.")]
		public const int NS_E_WMP_WMDM_LICENSE_NOTEXIST = unchecked((int)0xC00D11A6);

		[Description("Windows Media Player cannot sync the file because the sync rights have expired. Go to the content provider's online store to get new sync rights.")]
		public const int NS_E_WMP_WMDM_LICENSE_EXPIRED = unchecked((int)0xC00D11A7);

		[Description("The portable device is already in use. Wait until the current task finishes or quit other programs that might be using the portable device, and then try again.")]
		public const int NS_E_WMP_WMDM_BUSY = unchecked((int)0xC00D11A8);

		[Description("Windows Media Player cannot sync the file because the content provider or device prohibits it. You might be able to resolve this problem by going to the content provider's online store to get sync rights.")]
		public const int NS_E_WMP_WMDM_NORIGHTS = unchecked((int)0xC00D11A9);

		[Description("The content provider has not granted you the right to sync this file. Go to the content provider's online store to get sync rights.")]
		public const int NS_E_WMP_WMDM_INCORRECT_RIGHTS = unchecked((int)0xC00D11AA);

		[Description("Windows Media Player cannot burn the files to the CD. Verify that the disc is clean and not damaged. If necessary, select a slower recording speed or try a different brand of blank discs.")]
		public const int NS_E_WMP_IMAPI_GENERIC = unchecked((int)0xC00D11AB);

		[Description("Windows Media Player cannot burn the files. Verify that the burner is connected properly, and then try again.")]
		public const int NS_E_WMP_IMAPI_DEVICE_NOTPRESENT = unchecked((int)0xC00D11AD);

		[Description("Windows Media Player cannot burn the files. Verify that the burner is connected properly and that the disc is clean and not damaged. If the burner is already in use, wait until the current task finishes or quit other programs that might be using the burner.")]
		public const int NS_E_WMP_IMAPI_DEVICE_BUSY = unchecked((int)0xC00D11AE);

		[Description("Windows Media Player cannot burn the files to the CD.")]
		public const int NS_E_WMP_IMAPI_LOSS_OF_STREAMING = unchecked((int)0xC00D11AF);

		[Description("Windows Media Player cannot play the file. The server might not be available or there might be a problem with your network or firewall settings.")]
		public const int NS_E_WMP_SERVER_UNAVAILABLE = unchecked((int)0xC00D11B0);

		[Description("Windows Media Player encountered a problem while playing the file. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_FILE_OPEN_FAILED = unchecked((int)0xC00D11B1);

		[Description("Windows Media Player must connect to the Internet to verify the file's media usage rights. Connect to the Internet, and then try again.")]
		public const int NS_E_WMP_VERIFY_ONLINE = unchecked((int)0xC00D11B2);

		[Description("Windows Media Player cannot play the file because a network error occurred. The server might not be available. Verify that you are connected to the network and that your proxy settings are correct.")]
		public const int NS_E_WMP_SERVER_NOT_RESPONDING = unchecked((int)0xC00D11B3);

		[Description("Windows Media Player cannot restore your media usage rights because it could not find any backed up rights on your computer.")]
		public const int NS_E_WMP_DRM_CORRUPT_BACKUP = unchecked((int)0xC00D11B4);

		[Description("Windows Media Player cannot download media usage rights because the server is not available (for example, the server might be busy or not online).")]
		public const int NS_E_WMP_DRM_LICENSE_SERVER_UNAVAILABLE = unchecked((int)0xC00D11B5);

		[Description("Windows Media Player cannot play the file. A network firewall might be preventing the Player from opening the file by using the UDP transport protocol. If you typed a URL in the Open URL dialog box, try using a different transport protocol (for example, \"http:\").")]
		public const int NS_E_WMP_NETWORK_FIREWALL = unchecked((int)0xC00D11B6);

		[Description("Insert the removable media, and then try again.")]
		public const int NS_E_WMP_NO_REMOVABLE_MEDIA = unchecked((int)0xC00D11B7);

		[Description("Windows Media Player cannot play the file because the proxy server is not responding. The proxy server might be temporarily unavailable or your Player proxy settings might not be valid.")]
		public const int NS_E_WMP_PROXY_CONNECT_TIMEOUT = unchecked((int)0xC00D11B8);

		[Description("To play the file, you might need to install a later version of Windows Media Player. On the Help menu, click Check for Updates, and then follow the instructions. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_NEED_UPGRADE = unchecked((int)0xC00D11B9);

		[Description("Windows Media Player cannot play the file because there is a problem with your sound device. There might not be a sound device installed on your computer, it might be in use by another program, or it might not be functioning properly.")]
		public const int NS_E_WMP_AUDIO_HW_PROBLEM = unchecked((int)0xC00D11BA);

		[Description("Windows Media Player cannot play the file because the specified protocol is not supported. If you typed a URL in the Open URL dialog box, try using a different transport protocol (for example, \"http:\" or \"rtsp:\").")]
		public const int NS_E_WMP_INVALID_PROTOCOL = unchecked((int)0xC00D11BB);

		[Description("Windows Media Player cannot add the file to the library because the file format is not supported.")]
		public const int NS_E_WMP_INVALID_LIBRARY_ADD = unchecked((int)0xC00D11BC);

		[Description("Windows Media Player cannot play the file because the specified protocol is not supported. If you typed a URL in the Open URL dialog box, try using a different transport protocol (for example, \"mms:\").")]
		public const int NS_E_WMP_MMS_NOT_SUPPORTED = unchecked((int)0xC00D11BD);

		[Description("Windows Media Player cannot play the file because there are no streaming protocols selected. Select one or more protocols, and then try again.")]
		public const int NS_E_WMP_NO_PROTOCOLS_SELECTED = unchecked((int)0xC00D11BE);

		[Description("Windows Media Player cannot switch to Full Screen. You might need to adjust your Windows display settings. Open display settings in Control Panel, and then try setting Hardware acceleration to Full.")]
		public const int NS_E_WMP_GOFULLSCREEN_FAILED = unchecked((int)0xC00D11BF);

		[Description("Windows Media Player cannot play the file because a network error occurred. The server might not be available (for example, the server is busy or not online) or you might not be connected to the network.")]
		public const int NS_E_WMP_NETWORK_ERROR = unchecked((int)0xC00D11C0);

		[Description("Windows Media Player cannot play the file because the server is not responding. Verify that you are connected to the network, and then try again later.")]
		public const int NS_E_WMP_CONNECT_TIMEOUT = unchecked((int)0xC00D11C1);

		[Description("Windows Media Player cannot play the file because the multicast protocol is not enabled. On the Tools menu, click Options, click the Network tab, and then select the Multicast check box. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_MULTICAST_DISABLED = unchecked((int)0xC00D11C2);

		[Description("Windows Media Player cannot play the file because a network problem occurred. Verify that you are connected to the network, and then try again later.")]
		public const int NS_E_WMP_SERVER_DNS_TIMEOUT = unchecked((int)0xC00D11C3);

		[Description("Windows Media Player cannot play the file because the network proxy server cannot be found. Verify that your proxy settings are correct, and then try again.")]
		public const int NS_E_WMP_PROXY_NOT_FOUND = unchecked((int)0xC00D11C4);

		[Description("Windows Media Player cannot play the file because it is corrupted.")]
		public const int NS_E_WMP_TAMPERED_CONTENT = unchecked((int)0xC00D11C5);

		[Description("Your computer is running low on memory. Quit other programs, and then try again.")]
		public const int NS_E_WMP_OUTOFMEMORY = unchecked((int)0xC00D11C6);

		[Description("Windows Media Player cannot play, burn, rip, or sync the file because a required audio codec is not installed on your computer.")]
		public const int NS_E_WMP_AUDIO_CODEC_NOT_INSTALLED = unchecked((int)0xC00D11C7);

		[Description("Windows Media Player cannot play the file because the required video codec is not installed on your computer.")]
		public const int NS_E_WMP_VIDEO_CODEC_NOT_INSTALLED = unchecked((int)0xC00D11C8);

		[Description("Windows Media Player cannot burn the files. If the burner is busy, wait for the current task to finish. If necessary, verify that the burner is connected properly and that you have installed the latest device driver.")]
		public const int NS_E_WMP_IMAPI_DEVICE_INVALIDTYPE = unchecked((int)0xC00D11C9);

		[Description("Windows Media Player cannot play the protected file because there is a problem with your sound device. Try installing a new device driver or use a different sound device.")]
		public const int NS_E_WMP_DRM_DRIVER_AUTH_FAILURE = unchecked((int)0xC00D11CA);

		[Description("Windows Media Player encountered a network error. Restart the Player.")]
		public const int NS_E_WMP_NETWORK_RESOURCE_FAILURE = unchecked((int)0xC00D11CB);

		[Description("Windows Media Player is not installed properly. Reinstall the Player.")]
		public const int NS_E_WMP_UPGRADE_APPLICATION = unchecked((int)0xC00D11CC);

		[Description("Windows Media Player encountered an unknown error. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_UNKNOWN_ERROR = unchecked((int)0xC00D11CD);

		[Description("Windows Media Player cannot play the file because the required codec is not valid.")]
		public const int NS_E_WMP_INVALID_KEY = unchecked((int)0xC00D11CE);

		[Description("The CD drive is in use by another user. Wait for the task to complete, and then try again.")]
		public const int NS_E_WMP_CD_ANOTHER_USER = unchecked((int)0xC00D11CF);

		[Description("Windows Media Player cannot play, sync, or burn the protected file because a problem occurred with the Windows Media Digital Rights Management (DRM) system. You might need to connect to the Internet to update your DRM components. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_DRM_NEEDS_AUTHORIZATION = unchecked((int)0xC00D11D0);

		[Description("Windows Media Player cannot play the file because there might be a problem with your sound or video device. Try installing an updated device driver.")]
		public const int NS_E_WMP_BAD_DRIVER = unchecked((int)0xC00D11D1);

		[Description("Windows Media Player cannot access the file. The file might be in use, you might not have access to the computer where the file is stored, or your proxy settings might not be correct.")]
		public const int NS_E_WMP_ACCESS_DENIED = unchecked((int)0xC00D11D2);

		[Description("The content provider prohibits this action. Go to the content provider's online store to get new media usage rights.")]
		public const int NS_E_WMP_LICENSE_RESTRICTS = unchecked((int)0xC00D11D3);

		[Description("Windows Media Player cannot perform the requested action at this time.")]
		public const int NS_E_WMP_INVALID_REQUEST = unchecked((int)0xC00D11D4);

		[Description("Windows Media Player cannot burn the files because there is not enough free disk space to store the temporary files. Delete some unneeded files on your hard disk, and then try again.")]
		public const int NS_E_WMP_CD_STASH_NO_SPACE = unchecked((int)0xC00D11D5);

		[Description("Your media usage rights have become corrupted or are no longer valid. This might happen if you have replaced hardware components in your computer.")]
		public const int NS_E_WMP_DRM_NEW_HARDWARE = unchecked((int)0xC00D11D6);

		[Description("The required Windows Media Digital Rights Management (DRM) component cannot be validated. You might be able resolve the problem by reinstalling the Player.")]
		public const int NS_E_WMP_DRM_INVALID_SIG = unchecked((int)0xC00D11D7);

		[Description("You have exceeded your restore limit for the day. Try restoring your media usage rights tomorrow.")]
		public const int NS_E_WMP_DRM_CANNOT_RESTORE = unchecked((int)0xC00D11D8);

		[Description("Some files might not fit on the CD. The required space cannot be calculated accurately because some files might be missing duration information. To ensure the calculation is accurate, play the files that are missing duration information.")]
		public const int NS_E_WMP_BURN_DISC_OVERFLOW = unchecked((int)0xC00D11D9);

		[Description("Windows Media Player cannot verify the file's media usage rights. If you obtained this file from an online store, go to the online store to get the necessary rights.")]
		public const int NS_E_WMP_DRM_GENERIC_LICENSE_FAILURE = unchecked((int)0xC00D11DA);

		[Description("It is not possible to sync because this device's internal clock is not set correctly. To set the clock, select the option to set the device clock on the Privacy tab of the Options dialog box, connect to the Internet, and then sync the device again. For additional assistance, click Web Help.")]
		public const int NS_E_WMP_DRM_NO_SECURE_CLOCK = unchecked((int)0xC00D11DB);

		[Description("Windows Media Player cannot play, burn, rip, or sync the protected file because you do not have the appropriate rights.")]
		public const int NS_E_WMP_DRM_NO_RIGHTS = unchecked((int)0xC00D11DC);

		[Description("Windows Media Player encountered an error during upgrade.")]
		public const int NS_E_WMP_DRM_INDIV_FAILED = unchecked((int)0xC00D11DD);

		[Description("Windows Media Player cannot connect to the server because it is not accepting any new connections. This could be because it has reached its maximum connection limit. Please try again later.")]
		public const int NS_E_WMP_SERVER_NONEWCONNECTIONS = unchecked((int)0xC00D11DE);

		[Description("A number of queued files cannot be played. To find information about the problem, click the Now Playing tab, and then click the icon next to each file in the List pane.")]
		public const int NS_E_WMP_MULTIPLE_ERROR_IN_PLAYLIST = unchecked((int)0xC00D11DF);

		[Description("Windows Media Player encountered an error while erasing the rewritable CD or DVD. Verify that the CD or DVD burner is connected properly and that the disc is clean and not damaged.")]
		public const int NS_E_WMP_IMAPI2_ERASE_FAIL = unchecked((int)0xC00D11E0);

		[Description("Windows Media Player cannot erase the rewritable CD or DVD. Verify that the CD or DVD burner is connected properly and that the disc is clean and not damaged. If the burner is already in use, wait until the current task finishes or quit other programs that might be using the burner.")]
		public const int NS_E_WMP_IMAPI2_ERASE_DEVICE_BUSY = unchecked((int)0xC00D11E1);

		[Description("A Windows Media Digital Rights Management (DRM) component encountered a problem. If you are trying to use a file that you obtained from an online store, try going to the online store and getting the appropriate usage rights.")]
		public const int NS_E_WMP_DRM_COMPONENT_FAILURE = unchecked((int)0xC00D11E2);

		[Description("It is not possible to obtain device's certificate. Please contact the device manufacturer for a firmware update or for other steps to resolve this problem.")]
		public const int NS_E_WMP_DRM_NO_DEVICE_CERT = unchecked((int)0xC00D11E3);

		[Description("Windows Media Player encountered an error when connecting to the server. The security information from the server could not be validated.")]
		public const int NS_E_WMP_SERVER_SECURITY_ERROR = unchecked((int)0xC00D11E4);

		[Description("An audio device was disconnected or reconfigured. Verify that the audio device is connected, and then try to play the item again.")]
		public const int NS_E_WMP_AUDIO_DEVICE_LOST = unchecked((int)0xC00D11E5);

		[Description("Windows Media Player could not complete burning because the disc is not compatible with your drive. Try inserting a different kind of recordable media or use a disc that supports a write speed that is compatible with your drive.")]
		public const int NS_E_WMP_IMAPI_MEDIA_INCOMPATIBLE = unchecked((int)0xC00D11E6);

		[Description("Windows Media Player cannot save the sync settings because your device is full. Delete some unneeded files on your device and then try again.")]
		public const int NS_E_SYNCWIZ_DEVICE_FULL = unchecked((int)0xC00D11EE);

		[Description("It is not possible to change sync settings at this time. Try again later.")]
		public const int NS_E_SYNCWIZ_CANNOT_CHANGE_SETTINGS = unchecked((int)0xC00D11EF);

		[Description("Windows Media Player cannot delete these files currently. If the Player is synchronizing, wait until it is complete and then try again.")]
		public const int NS_E_TRANSCODE_DELETECACHEERROR = unchecked((int)0xC00D11F0);

		[Description("Windows Media Player could not use digital mode to read the CD. The Player has automatically switched the CD drive to analog mode. To switch back to digital mode, use the Devices tab. For additional assistance, click Web Help.")]
		public const int NS_E_CD_NO_BUFFERS_READ = unchecked((int)0xC00D11F8);

		[Description("No CD track was specified for playback.")]
		public const int NS_E_CD_EMPTY_TRACK_QUEUE = unchecked((int)0xC00D11F9);

		[Description("The CD filter was not able to create the CD reader.")]
		public const int NS_E_CD_NO_READER = unchecked((int)0xC00D11FA);

		[Description("Invalid ISRC code.")]
		public const int NS_E_CD_ISRC_INVALID = unchecked((int)0xC00D11FB);

		[Description("Invalid Media Catalog Number.")]
		public const int NS_E_CD_MEDIA_CATALOG_NUMBER_INVALID = unchecked((int)0xC00D11FC);

		[Description("Windows Media Player cannot play audio CDs correctly because the CD drive is slow and error correction is turned on. To increase performance, turn off playback error correction for this drive.")]
		public const int NS_E_SLOW_READ_DIGITAL_WITH_ERRORCORRECTION = unchecked((int)0xC00D11FD);

		[Description("Windows Media Player cannot estimate the CD drive's playback speed because the CD track is too short.")]
		public const int NS_E_CD_SPEEDDETECT_NOT_ENOUGH_READS = unchecked((int)0xC00D11FE);

		[Description("Cannot queue the CD track because queuing is not enabled.")]
		public const int NS_E_CD_QUEUEING_DISABLED = unchecked((int)0xC00D11FF);

		[Description("Windows Media Player cannot download additional media usage rights until the current download is complete.")]
		public const int NS_E_WMP_DRM_ACQUIRING_LICENSE = unchecked((int)0xC00D1202);

		[Description("The media usage rights for this file have expired or are no longer valid. If you obtained the file from an online store, sign in to the store, and then try again.")]
		public const int NS_E_WMP_DRM_LICENSE_EXPIRED = unchecked((int)0xC00D1203);

		[Description("Windows Media Player cannot download the media usage rights for the file. If you obtained the file from an online store, sign in to the store, and then try again.")]
		public const int NS_E_WMP_DRM_LICENSE_NOTACQUIRED = unchecked((int)0xC00D1204);

		[Description("The media usage rights for this file are not yet valid. To see when they will become valid, right-click the file in the library, click Properties, and then click the Media Usage Rights tab.")]
		public const int NS_E_WMP_DRM_LICENSE_NOTENABLED = unchecked((int)0xC00D1205);

		[Description("The media usage rights for this file are not valid. If you obtained this file from an online store, contact the store for assistance.")]
		public const int NS_E_WMP_DRM_LICENSE_UNUSABLE = unchecked((int)0xC00D1206);

		[Description("The content provider has revoked the media usage rights for this file. If you obtained this file from an online store, ask the store if a new version of the file is available.")]
		public const int NS_E_WMP_DRM_LICENSE_CONTENT_REVOKED = unchecked((int)0xC00D1207);

		[Description("The media usage rights for this file require a feature that is not supported in your current version of Windows Media Player or your current version of Windows. Try installing the latest version of the Player. If you obtained this file from an online store, contact the store for further assistance.")]
		public const int NS_E_WMP_DRM_LICENSE_NOSAP = unchecked((int)0xC00D1208);

		[Description("Windows Media Player cannot download media usage rights at this time. Try again later.")]
		public const int NS_E_WMP_DRM_UNABLE_TO_ACQUIRE_LICENSE = unchecked((int)0xC00D1209);

		[Description("Windows Media Player cannot play, burn, or sync the file because the media usage rights are missing. If you obtained the file from an online store, sign in to the store, and then try again.")]
		public const int NS_E_WMP_LICENSE_REQUIRED = unchecked((int)0xC00D120A);

		[Description("Windows Media Player cannot play, burn, or sync the file because the media usage rights are missing. If you obtained the file from an online store, sign in to the store, and then try again.")]
		public const int NS_E_WMP_PROTECTED_CONTENT = unchecked((int)0xC00D120B);

		[Description("Windows Media Player cannot read a policy. This can occur when the policy does not exist in the registry or when the registry cannot be read.")]
		public const int NS_E_WMP_POLICY_VALUE_NOT_CONFIGURED = unchecked((int)0xC00D122A);

		[Description("Windows Media Player cannot sync content streamed directly from the Internet. If possible, download the file to your computer, and then try to sync the file.")]
		public const int NS_E_PDA_CANNOT_SYNC_FROM_INTERNET = unchecked((int)0xC00D1234);

		[Description("This playlist is not valid or is corrupted. Create a new playlist using Windows Media Player, then sync the new playlist instead.")]
		public const int NS_E_PDA_CANNOT_SYNC_INVALID_PLAYLIST = unchecked((int)0xC00D1235);

		[Description("Windows Media Player encountered a problem while synchronizing the file to the device. For additional assistance, click Web Help.")]
		public const int NS_E_PDA_FAILED_TO_SYNCHRONIZE_FILE = unchecked((int)0xC00D1236);

		[Description("Windows Media Player encountered an error while synchronizing to the device.")]
		public const int NS_E_PDA_SYNC_FAILED = unchecked((int)0xC00D1237);

		[Description("Windows Media Player cannot delete a file from the device.")]
		public const int NS_E_PDA_DELETE_FAILED = unchecked((int)0xC00D1238);

		[Description("Windows Media Player cannot copy a file from the device to your library.")]
		public const int NS_E_PDA_FAILED_TO_RETRIEVE_FILE = unchecked((int)0xC00D1239);

		[Description("Windows Media Player cannot communicate with the device because the device is not responding. Try reconnecting the device, resetting the device, or contacting the device manufacturer for updated firmware.")]
		public const int NS_E_PDA_DEVICE_NOT_RESPONDING = unchecked((int)0xC00D123A);

		[Description("Windows Media Player cannot sync the picture to the device because a problem occurred while converting the file to another quality level or format. The original file might be damaged or corrupted.")]
		public const int NS_E_PDA_FAILED_TO_TRANSCODE_PHOTO = unchecked((int)0xC00D123B);

		[Description("Windows Media Player cannot convert the file. The file might have been encrypted by the Encrypted File System (EFS). Try decrypting the file first and then synchronizing it. For information about how to decrypt a file, see Windows Help and Support.")]
		public const int NS_E_PDA_FAILED_TO_ENCRYPT_TRANSCODED_FILE = unchecked((int)0xC00D123C);

		[Description("Your device requires that this file be converted in order to play on the device. However, the device either does not support playing audio, or Windows Media Player cannot convert the file to an audio format that is supported by the device.")]
		public const int NS_E_PDA_CANNOT_TRANSCODE_TO_AUDIO = unchecked((int)0xC00D123D);

		[Description("Your device requires that this file be converted in order to play on the device. However, the device either does not support playing video, or Windows Media Player cannot convert the file to a video format that is supported by the device.")]
		public const int NS_E_PDA_CANNOT_TRANSCODE_TO_VIDEO = unchecked((int)0xC00D123E);

		[Description("Your device requires that this file be converted in order to play on the device. However, the device either does not support displaying pictures, or Windows Media Player cannot convert the file to a picture format that is supported by the device.")]
		public const int NS_E_PDA_CANNOT_TRANSCODE_TO_IMAGE = unchecked((int)0xC00D123F);

		[Description("Windows Media Player cannot sync the file to your computer because the file name is too long. Try renaming the file on the device.")]
		public const int NS_E_PDA_RETRIEVED_FILE_FILENAME_TOO_LONG = unchecked((int)0xC00D1240);

		[Description("Windows Media Player cannot sync the file because the device is not responding. This typically occurs when there is a problem with the device firmware. For additional assistance, click Web Help.")]
		public const int NS_E_PDA_CEWMDM_DRM_ERROR = unchecked((int)0xC00D1241);

		[Description("Incomplete playlist.")]
		public const int NS_E_INCOMPLETE_PLAYLIST = unchecked((int)0xC00D1242);

		[Description("It is not possible to perform the requested action because sync is in progress. You can either stop sync or wait for it to complete, and then try again.")]
		public const int NS_E_PDA_SYNC_RUNNING = unchecked((int)0xC00D1243);

		[Description("Windows Media Player cannot sync the subscription content because you are not signed in to the online store that provided it. Sign in to the online store, and then try again.")]
		public const int NS_E_PDA_SYNC_LOGIN_ERROR = unchecked((int)0xC00D1244);

		[Description("Windows Media Player cannot convert the file to the format required by the device. One or more codecs required to convert the file could not be found.")]
		public const int NS_E_PDA_TRANSCODE_CODEC_NOT_FOUND = unchecked((int)0xC00D1245);

		[Description("It is not possible to sync subscription files to this device.")]
		public const int NS_E_CANNOT_SYNC_DRM_TO_NON_JANUS_DEVICE = unchecked((int)0xC00D1246);

		[Description("Your device is operating slowly or is not responding. Until the device responds, it is not possible to sync again. To return the device to normal operation, try disconnecting it from the computer or resetting it.")]
		public const int NS_E_CANNOT_SYNC_PREVIOUS_SYNC_RUNNING = unchecked((int)0xC00D1247);

		[Description("The Windows Media Player download manager cannot function properly because the Player main window cannot be found. Try restarting the Player.")]
		public const int NS_E_WMP_HWND_NOTFOUND = unchecked((int)0xC00D125C);

		[Description("Windows Media Player encountered a download that has the wrong number of files. This might occur if another program is trying to create jobs with the same signature as the Player.")]
		public const int NS_E_BKGDOWNLOAD_WRONG_NO_FILES = unchecked((int)0xC00D125D);

		[Description("Windows Media Player tried to complete a download that was already canceled. The file will not be available.")]
		public const int NS_E_BKGDOWNLOAD_COMPLETECANCELLEDJOB = unchecked((int)0xC00D125E);

		[Description("Windows Media Player tried to cancel a download that was already completed. The file will not be removed.")]
		public const int NS_E_BKGDOWNLOAD_CANCELCOMPLETEDJOB = unchecked((int)0xC00D125F);

		[Description("Windows Media Player is trying to access a download that is not valid.")]
		public const int NS_E_BKGDOWNLOAD_NOJOBPOINTER = unchecked((int)0xC00D1260);

		[Description("This download was not created by Windows Media Player.")]
		public const int NS_E_BKGDOWNLOAD_INVALIDJOBSIGNATURE = unchecked((int)0xC00D1261);

		[Description("The Windows Media Player download manager cannot create a temporary file name. This might occur if the path is not valid or if the disk is full.")]
		public const int NS_E_BKGDOWNLOAD_FAILED_TO_CREATE_TEMPFILE = unchecked((int)0xC00D1262);

		[Description("The Windows Media Player download manager plug-in cannot start. This might occur if the system is out of resources.")]
		public const int NS_E_BKGDOWNLOAD_PLUGIN_FAILEDINITIALIZE = unchecked((int)0xC00D1263);

		[Description("The Windows Media Player download manager cannot move the file.")]
		public const int NS_E_BKGDOWNLOAD_PLUGIN_FAILEDTOMOVEFILE = unchecked((int)0xC00D1264);

		[Description("The Windows Media Player download manager cannot perform a task because the system has no resources to allocate.")]
		public const int NS_E_BKGDOWNLOAD_CALLFUNCFAILED = unchecked((int)0xC00D1265);

		[Description("The Windows Media Player download manager cannot perform a task because the task took too long to run.")]
		public const int NS_E_BKGDOWNLOAD_CALLFUNCTIMEOUT = unchecked((int)0xC00D1266);

		[Description("The Windows Media Player download manager cannot perform a task because the Player is terminating the service. The task will be recovered when the Player restarts.")]
		public const int NS_E_BKGDOWNLOAD_CALLFUNCENDED = unchecked((int)0xC00D1267);

		[Description("The Windows Media Player download manager cannot expand a WMD file. The file will be deleted and the operation will not be completed successfully.")]
		public const int NS_E_BKGDOWNLOAD_WMDUNPACKFAILED = unchecked((int)0xC00D1268);

		[Description("The Windows Media Player download manager cannot start. This might occur if the system is out of resources.")]
		public const int NS_E_BKGDOWNLOAD_FAILEDINITIALIZE = unchecked((int)0xC00D1269);

		[Description("Windows Media Player cannot access a required functionality. This might occur if the wrong system files or Player DLLs are loaded.")]
		public const int NS_E_INTERFACE_NOT_REGISTERED_IN_GIT = unchecked((int)0xC00D126A);

		[Description("Windows Media Player cannot get the file name of the requested download. The requested download will be canceled.")]
		public const int NS_E_BKGDOWNLOAD_INVALID_FILE_NAME = unchecked((int)0xC00D126B);

		[Description("Windows Media Player encountered an error while downloading an image.")]
		public const int NS_E_IMAGE_DOWNLOAD_FAILED = unchecked((int)0xC00D128E);

		[Description("Windows Media Player cannot update your media usage rights because the Player cannot verify the list of activated users of this computer.")]
		public const int NS_E_WMP_UDRM_NOUSERLIST = unchecked((int)0xC00D12C0);

		[Description("Windows Media Player is trying to acquire media usage rights for a file that is no longer being used. Rights acquisition will stop.")]
		public const int NS_E_WMP_DRM_NOT_ACQUIRING = unchecked((int)0xC00D12C1);

		[Description("The parameter is not valid.")]
		public const int NS_E_WMP_BSTR_TOO_LONG = unchecked((int)0xC00D12F2);

		[Description("The state is not valid for this request.")]
		public const int NS_E_WMP_AUTOPLAY_INVALID_STATE = unchecked((int)0xC00D12FC);

		[Description("Windows Media Player cannot play this file until you complete the software component upgrade. After the component has been upgraded, try to play the file again.")]
		public const int NS_E_WMP_COMPONENT_REVOKED = unchecked((int)0xC00D1306);

		[Description("The URL is not safe for the operation specified.")]
		public const int NS_E_CURL_NOTSAFE = unchecked((int)0xC00D1324);

		[Description("The URL contains one or more characters that are not valid.")]
		public const int NS_E_CURL_INVALIDCHAR = unchecked((int)0xC00D1325);

		[Description("The URL contains a host name that is not valid.")]
		public const int NS_E_CURL_INVALIDHOSTNAME = unchecked((int)0xC00D1326);

		[Description("The URL contains a path that is not valid.")]
		public const int NS_E_CURL_INVALIDPATH = unchecked((int)0xC00D1327);

		[Description("The URL contains a scheme that is not valid.")]
		public const int NS_E_CURL_INVALIDSCHEME = unchecked((int)0xC00D1328);

		[Description("The URL is not valid.")]
		public const int NS_E_CURL_INVALIDURL = unchecked((int)0xC00D1329);

		[Description("Windows Media Player cannot play the file. If you clicked a link on a web page, the link might not be valid.")]
		public const int NS_E_CURL_CANTWALK = unchecked((int)0xC00D132B);

		[Description("The URL port is not valid.")]
		public const int NS_E_CURL_INVALIDPORT = unchecked((int)0xC00D132C);

		[Description("The URL is not a directory.")]
		public const int NS_E_CURLHELPER_NOTADIRECTORY = unchecked((int)0xC00D132D);

		[Description("The URL is not a file.")]
		public const int NS_E_CURLHELPER_NOTAFILE = unchecked((int)0xC00D132E);

		[Description("The URL contains characters that cannot be decoded. The URL might be truncated or incomplete.")]
		public const int NS_E_CURL_CANTDECODE = unchecked((int)0xC00D132F);

		[Description("The specified URL is not a relative URL.")]
		public const int NS_E_CURLHELPER_NOTRELATIVE = unchecked((int)0xC00D1330);

		[Description("The buffer is smaller than the size specified.")]
		public const int NS_E_CURL_INVALIDBUFFERSIZE = unchecked((int)0xC00D1331);

		[Description("The content provider has not granted you the right to play this file. Go to the content provider's online store to get play rights.")]
		public const int NS_E_SUBSCRIPTIONSERVICE_PLAYBACK_DISALLOWED = unchecked((int)0xC00D1356);

		[Description("Windows Media Player cannot purchase or download content from multiple online stores.")]
		public const int NS_E_CANNOT_BUY_OR_DOWNLOAD_FROM_MULTIPLE_SERVICES = unchecked((int)0xC00D1357);

		[Description("The file cannot be purchased or downloaded. The file might not be available from the online store.")]
		public const int NS_E_CANNOT_BUY_OR_DOWNLOAD_CONTENT = unchecked((int)0xC00D1358);

		[Description("The provider of this file cannot be identified.")]
		public const int NS_E_NOT_CONTENT_PARTNER_TRACK = unchecked((int)0xC00D135A);

		[Description("The file is only available for download when you buy the entire album.")]
		public const int NS_E_TRACK_DOWNLOAD_REQUIRES_ALBUM_PURCHASE = unchecked((int)0xC00D135B);

		[Description("You must buy the file before you can download it.")]
		public const int NS_E_TRACK_DOWNLOAD_REQUIRES_PURCHASE = unchecked((int)0xC00D135C);

		[Description("You have exceeded the maximum number of files that can be purchased in a single transaction.")]
		public const int NS_E_TRACK_PURCHASE_MAXIMUM_EXCEEDED = unchecked((int)0xC00D135D);

		[Description("Windows Media Player cannot sign in to the online store. Verify that you are using the correct user name and password. If the problem persists, the store might be temporarily unavailable.")]
		public const int NS_E_SUBSCRIPTIONSERVICE_LOGIN_FAILED = unchecked((int)0xC00D135F);

		[Description("Windows Media Player cannot download this item because the server is not responding. The server might be temporarily unavailable or the Internet connection might be lost.")]
		public const int NS_E_SUBSCRIPTIONSERVICE_DOWNLOAD_TIMEOUT = unchecked((int)0xC00D1360);

		[Description("Content Partner still initializing.")]
		public const int NS_E_CONTENT_PARTNER_STILL_INITIALIZING = unchecked((int)0xC00D1362);

		[Description("The folder could not be opened. The folder might have been moved or deleted.")]
		public const int NS_E_OPEN_CONTAINING_FOLDER_FAILED = unchecked((int)0xC00D1363);

		[Description("Windows Media Player could not add all of the images to the file because the images exceeded the 7 megabyte (MB) limit.")]
		public const int NS_E_ADVANCEDEDIT_TOO_MANY_PICTURES = unchecked((int)0xC00D136A);

		[Description("The client redirected to another server.")]
		public const int NS_E_REDIRECT = unchecked((int)0xC00D1388);

		[Description("The streaming media description is no longer current.")]
		public const int NS_E_STALE_PRESENTATION = unchecked((int)0xC00D1389);

		[Description("It is not possible to create a persistent namespace node under a transient parent node.")]
		public const int NS_E_NAMESPACE_WRONG_PERSIST = unchecked((int)0xC00D138A);

		[Description("It is not possible to store a value in a namespace node that has a different value type.")]
		public const int NS_E_NAMESPACE_WRONG_TYPE = unchecked((int)0xC00D138B);

		[Description("It is not possible to remove the root namespace node.")]
		public const int NS_E_NAMESPACE_NODE_CONFLICT = unchecked((int)0xC00D138C);

		[Description("The specified namespace node could not be found.")]
		public const int NS_E_NAMESPACE_NODE_NOT_FOUND = unchecked((int)0xC00D138D);

		[Description("The buffer supplied to hold namespace node string is too small.")]
		public const int NS_E_NAMESPACE_BUFFER_TOO_SMALL = unchecked((int)0xC00D138E);

		[Description("The callback list on a namespace node is at the maximum size.")]
		public const int NS_E_NAMESPACE_TOO_MANY_CALLBACKS = unchecked((int)0xC00D138F);

		[Description("It is not possible to register an already-registered callback on a namespace node.")]
		public const int NS_E_NAMESPACE_DUPLICATE_CALLBACK = unchecked((int)0xC00D1390);

		[Description("Cannot find the callback in the namespace when attempting to remove the callback.")]
		public const int NS_E_NAMESPACE_CALLBACK_NOT_FOUND = unchecked((int)0xC00D1391);

		[Description("The namespace node name exceeds the allowed maximum length.")]
		public const int NS_E_NAMESPACE_NAME_TOO_LONG = unchecked((int)0xC00D1392);

		[Description("Cannot create a namespace node that already exists.")]
		public const int NS_E_NAMESPACE_DUPLICATE_NAME = unchecked((int)0xC00D1393);

		[Description("The namespace node name cannot be a null string.")]
		public const int NS_E_NAMESPACE_EMPTY_NAME = unchecked((int)0xC00D1394);

		[Description("Finding a child namespace node by index failed because the index exceeded the number of children.")]
		public const int NS_E_NAMESPACE_INDEX_TOO_LARGE = unchecked((int)0xC00D1395);

		[Description("The namespace node name is invalid.")]
		public const int NS_E_NAMESPACE_BAD_NAME = unchecked((int)0xC00D1396);

		[Description("It is not possible to store a value in a namespace node that has a different security type.")]
		public const int NS_E_NAMESPACE_WRONG_SECURITY = unchecked((int)0xC00D1397);

		[Description("The archive request conflicts with other requests in progress.")]
		public const int NS_E_CACHE_ARCHIVE_CONFLICT = unchecked((int)0xC00D13EC);

		[Description("The specified origin server cannot be found.")]
		public const int NS_E_CACHE_ORIGIN_SERVER_NOT_FOUND = unchecked((int)0xC00D13ED);

		[Description("The specified origin server is not responding.")]
		public const int NS_E_CACHE_ORIGIN_SERVER_TIMEOUT = unchecked((int)0xC00D13EE);

		[Description("The internal code for HTTP status code 412 Precondition Failed due to not broadcast type.")]
		public const int NS_E_CACHE_NOT_BROADCAST = unchecked((int)0xC00D13EF);

		[Description("The internal code for HTTP status code 403 Forbidden due to not cacheable.")]
		public const int NS_E_CACHE_CANNOT_BE_CACHED = unchecked((int)0xC00D13F0);

		[Description("The internal code for HTTP status code 304 Not Modified.")]
		public const int NS_E_CACHE_NOT_MODIFIED = unchecked((int)0xC00D13F1);

		[Description("It is not possible to remove a cache or proxy publishing point.")]
		public const int NS_E_CANNOT_REMOVE_PUBLISHING_POINT = unchecked((int)0xC00D1450);

		[Description("It is not possible to remove the last instance of a type of plug-in.")]
		public const int NS_E_CANNOT_REMOVE_PLUGIN = unchecked((int)0xC00D1451);

		[Description("Cache and proxy publishing points do not support this property or method.")]
		public const int NS_E_WRONG_PUBLISHING_POINT_TYPE = unchecked((int)0xC00D1452);

		[Description("The plug-in does not support the specified load type.")]
		public const int NS_E_UNSUPPORTED_LOAD_TYPE = unchecked((int)0xC00D1453);

		[Description("The plug-in does not support any load types. The plug-in must support at least one load type.")]
		public const int NS_E_INVALID_PLUGIN_LOAD_TYPE_CONFIGURATION = unchecked((int)0xC00D1454);

		[Description("The publishing point name is invalid.")]
		public const int NS_E_INVALID_PUBLISHING_POINT_NAME = unchecked((int)0xC00D1455);

		[Description("Only one multicast data writer plug-in can be enabled for a publishing point.")]
		public const int NS_E_TOO_MANY_MULTICAST_SINKS = unchecked((int)0xC00D1456);

		[Description("The requested operation cannot be completed while the publishing point is started.")]
		public const int NS_E_PUBLISHING_POINT_INVALID_REQUEST_WHILE_STARTED = unchecked((int)0xC00D1457);

		[Description("A multicast data writer plug-in must be enabled in order for this operation to be completed.")]
		public const int NS_E_MULTICAST_PLUGIN_NOT_ENABLED = unchecked((int)0xC00D1458);

		[Description("This feature requires Windows Server 2003, Enterprise Edition.")]
		public const int NS_E_INVALID_OPERATING_SYSTEM_VERSION = unchecked((int)0xC00D1459);

		[Description("The requested operation cannot be completed because the specified publishing point has been removed.")]
		public const int NS_E_PUBLISHING_POINT_REMOVED = unchecked((int)0xC00D145A);

		[Description("Push publishing points are started when the encoder starts pushing the stream. This publishing point cannot be started by the server administrator.")]
		public const int NS_E_INVALID_PUSH_PUBLISHING_POINT_START_REQUEST = unchecked((int)0xC00D145B);

		[Description("The specified language is not supported.")]
		public const int NS_E_UNSUPPORTED_LANGUAGE = unchecked((int)0xC00D145C);

		[Description("Windows Media Services will only run on Windows Server 2003, Standard Edition and Windows Server 2003, Enterprise Edition.")]
		public const int NS_E_WRONG_OS_VERSION = unchecked((int)0xC00D145D);

		[Description("The operation cannot be completed because the publishing point has been stopped.")]
		public const int NS_E_PUBLISHING_POINT_STOPPED = unchecked((int)0xC00D145E);

		[Description("The playlist entry is already playing.")]
		public const int NS_E_PLAYLIST_ENTRY_ALREADY_PLAYING = unchecked((int)0xC00D14B4);

		[Description("The playlist or directory you are requesting does not contain content.")]
		public const int NS_E_EMPTY_PLAYLIST = unchecked((int)0xC00D14B5);

		[Description("The server was unable to parse the requested playlist file.")]
		public const int NS_E_PLAYLIST_PARSE_FAILURE = unchecked((int)0xC00D14B6);

		[Description("The requested operation is not supported for this type of playlist entry.")]
		public const int NS_E_PLAYLIST_UNSUPPORTED_ENTRY = unchecked((int)0xC00D14B7);

		[Description("Cannot jump to a playlist entry that is not inserted in the playlist.")]
		public const int NS_E_PLAYLIST_ENTRY_NOT_IN_PLAYLIST = unchecked((int)0xC00D14B8);

		[Description("Cannot seek to the desired playlist entry.")]
		public const int NS_E_PLAYLIST_ENTRY_SEEK = unchecked((int)0xC00D14B9);

		[Description("Cannot play recursive playlist.")]
		public const int NS_E_PLAYLIST_RECURSIVE_PLAYLISTS = unchecked((int)0xC00D14BA);

		[Description("The number of nested playlists exceeded the limit the server can handle.")]
		public const int NS_E_PLAYLIST_TOO_MANY_NESTED_PLAYLISTS = unchecked((int)0xC00D14BB);

		[Description("Cannot execute the requested operation because the playlist has been shut down by the Media Server.")]
		public const int NS_E_PLAYLIST_SHUTDOWN = unchecked((int)0xC00D14BC);

		[Description("The playlist has ended while receding.")]
		public const int NS_E_PLAYLIST_END_RECEDING = unchecked((int)0xC00D14BD);

		[Description("The data path does not have an associated data writer plug-in.")]
		public const int NS_E_DATAPATH_NO_SINK = unchecked((int)0xC00D1518);

		[Description("The specified push template is invalid.")]
		public const int NS_E_INVALID_PUSH_TEMPLATE = unchecked((int)0xC00D151A);

		[Description("The specified push publishing point is invalid.")]
		public const int NS_E_INVALID_PUSH_PUBLISHING_POINT = unchecked((int)0xC00D151B);

		[Description("The requested operation cannot be performed because the server or publishing point is in a critical error state.")]
		public const int NS_E_CRITICAL_ERROR = unchecked((int)0xC00D151C);

		[Description("The content cannot be played because the server is not currently accepting connections. Try connecting at a later time.")]
		public const int NS_E_NO_NEW_CONNECTIONS = unchecked((int)0xC00D151D);

		[Description("The version of this playlist is not supported by the server.")]
		public const int NS_E_WSX_INVALID_VERSION = unchecked((int)0xC00D151E);

		[Description("The command does not apply to the current media header user by a server component.")]
		public const int NS_E_HEADER_MISMATCH = unchecked((int)0xC00D151F);

		[Description("The specified publishing point name is already in use.")]
		public const int NS_E_PUSH_DUPLICATE_PUBLISHING_POINT_NAME = unchecked((int)0xC00D1520);

		[Description("There is no script engine available for this file.")]
		public const int NS_E_NO_SCRIPT_ENGINE = unchecked((int)0xC00D157C);

		[Description("The plug-in has reported an error. See the Troubleshooting tab or the NT Application Event Log for details.")]
		public const int NS_E_PLUGIN_ERROR_REPORTED = unchecked((int)0xC00D157D);

		[Description("No enabled data source plug-in is available to access the requested content.")]
		public const int NS_E_SOURCE_PLUGIN_NOT_FOUND = unchecked((int)0xC00D157E);

		[Description("No enabled playlist parser plug-in is available to access the requested content.")]
		public const int NS_E_PLAYLIST_PLUGIN_NOT_FOUND = unchecked((int)0xC00D157F);

		[Description("The data source plug-in does not support enumeration.")]
		public const int NS_E_DATA_SOURCE_ENUMERATION_NOT_SUPPORTED = unchecked((int)0xC00D1580);

		[Description("The server cannot stream the selected file because it is either damaged or corrupt. Select a different file.")]
		public const int NS_E_MEDIA_PARSER_INVALID_FORMAT = unchecked((int)0xC00D1581);

		[Description("The plug-in cannot be enabled because a compatible script debugger is not installed on this system. Install a script debugger, or disable the script debugger option on the general tab of the plug-in's properties page and try again.")]
		public const int NS_E_SCRIPT_DEBUGGER_NOT_INSTALLED = unchecked((int)0xC00D1582);

		[Description("The plug-in cannot be loaded because it requires Windows Server 2003, Enterprise Edition.")]
		public const int NS_E_FEATURE_REQUIRES_ENTERPRISE_SERVER = unchecked((int)0xC00D1583);

		[Description("Another wizard is currently running. Please close the other wizard or wait until it finishes before attempting to run this wizard again.")]
		public const int NS_E_WIZARD_RUNNING = unchecked((int)0xC00D1584);

		[Description("Invalid log URL. Multicast logging URL must look like \"http://servername/isapibackend.dll\".")]
		public const int NS_E_INVALID_LOG_URL = unchecked((int)0xC00D1585);

		[Description("Invalid MTU specified. The valid range for maximum packet size is between 36 and 65507 bytes.")]
		public const int NS_E_INVALID_MTU_RANGE = unchecked((int)0xC00D1586);

		[Description("Invalid play statistics for logging.")]
		public const int NS_E_INVALID_PLAY_STATISTICS = unchecked((int)0xC00D1587);

		[Description("The log needs to be skipped.")]
		public const int NS_E_LOG_NEED_TO_BE_SKIPPED = unchecked((int)0xC00D1588);

		[Description("The size of the data exceeded the limit the WMS HTTP Download Data Source plugin can handle.")]
		public const int NS_E_HTTP_TEXT_DATACONTAINER_SIZE_LIMIT_EXCEEDED = unchecked((int)0xC00D1589);

		[Description("One usage of each socket address (protocol/network address/port) is permitted. Verify that other services or applications are not attempting to use the same port and then try to enable the plug-in again.")]
		public const int NS_E_PORT_IN_USE = unchecked((int)0xC00D158A);

		[Description("One usage of each socket address (protocol/network address/port) is permitted. Verify that other services (such as IIS) or applications are not attempting to use the same port and then try to enable the plug-in again.")]
		public const int NS_E_PORT_IN_USE_HTTP = unchecked((int)0xC00D158B);

		[Description("The WMS HTTP Download Data Source plugin was unable to receive the remote server's response.")]
		public const int NS_E_HTTP_TEXT_DATACONTAINER_INVALID_SERVER_RESPONSE = unchecked((int)0xC00D158C);

		[Description("The archive plug-in has reached its quota.")]
		public const int NS_E_ARCHIVE_REACH_QUOTA = unchecked((int)0xC00D158D);

		[Description("The archive plug-in aborted because the source was from broadcast.")]
		public const int NS_E_ARCHIVE_ABORT_DUE_TO_BCAST = unchecked((int)0xC00D158E);

		[Description("The archive plug-in detected an interrupt in the source.")]
		public const int NS_E_ARCHIVE_GAP_DETECTED = unchecked((int)0xC00D158F);

		[Description("The system cannot find the file specified.")]
		public const int NS_E_AUTHORIZATION_FILE_NOT_FOUND = unchecked((int)0xC00D1590);

		[Description("The mark-in time should be greater than 0 and less than the mark-out time.")]
		public const int NS_E_BAD_MARKIN = unchecked((int)0xC00D1B58);

		[Description("The mark-out time should be greater than the mark-in time and less than the file duration.")]
		public const int NS_E_BAD_MARKOUT = unchecked((int)0xC00D1B59);

		[Description("No matching media type is found in the source %1.")]
		public const int NS_E_NOMATCHING_MEDIASOURCE = unchecked((int)0xC00D1B5A);

		[Description("The specified source type is not supported.")]
		public const int NS_E_UNSUPPORTED_SOURCETYPE = unchecked((int)0xC00D1B5B);

		[Description("It is not possible to specify more than one audio input.")]
		public const int NS_E_TOO_MANY_AUDIO = unchecked((int)0xC00D1B5C);

		[Description("It is not possible to specify more than two video inputs.")]
		public const int NS_E_TOO_MANY_VIDEO = unchecked((int)0xC00D1B5D);

		[Description("No matching element is found in the list.")]
		public const int NS_E_NOMATCHING_ELEMENT = unchecked((int)0xC00D1B5E);

		[Description("The profile's media types must match the media types defined for the session.")]
		public const int NS_E_MISMATCHED_MEDIACONTENT = unchecked((int)0xC00D1B5F);

		[Description("It is not possible to remove an active source while encoding.")]
		public const int NS_E_CANNOT_DELETE_ACTIVE_SOURCEGROUP = unchecked((int)0xC00D1B60);

		[Description("It is not possible to open the specified audio capture device because it is currently in use.")]
		public const int NS_E_AUDIODEVICE_BUSY = unchecked((int)0xC00D1B61);

		[Description("It is not possible to open the specified audio capture device because an unexpected error has occurred.")]
		public const int NS_E_AUDIODEVICE_UNEXPECTED = unchecked((int)0xC00D1B62);

		[Description("The audio capture device does not support the specified audio format.")]
		public const int NS_E_AUDIODEVICE_BADFORMAT = unchecked((int)0xC00D1B63);

		[Description("It is not possible to open the specified video capture device because it is currently in use.")]
		public const int NS_E_VIDEODEVICE_BUSY = unchecked((int)0xC00D1B64);

		[Description("It is not possible to open the specified video capture device because an unexpected error has occurred.")]
		public const int NS_E_VIDEODEVICE_UNEXPECTED = unchecked((int)0xC00D1B65);

		[Description("This operation is not allowed while encoding.")]
		public const int NS_E_INVALIDCALL_WHILE_ENCODER_RUNNING = unchecked((int)0xC00D1B66);

		[Description("No profile is set for the source.")]
		public const int NS_E_NO_PROFILE_IN_SOURCEGROUP = unchecked((int)0xC00D1B67);

		[Description("The video capture driver returned an unrecoverable error. It is now in an unstable state.")]
		public const int NS_E_VIDEODRIVER_UNSTABLE = unchecked((int)0xC00D1B68);

		[Description("It was not possible to start the video device.")]
		public const int NS_E_VIDCAPSTARTFAILED = unchecked((int)0xC00D1B69);

		[Description("The video source does not support the requested output format or color depth.")]
		public const int NS_E_VIDSOURCECOMPRESSION = unchecked((int)0xC00D1B6A);

		[Description("The video source does not support the requested capture size.")]
		public const int NS_E_VIDSOURCESIZE = unchecked((int)0xC00D1B6B);

		[Description("It was not possible to obtain output information from the video compressor.")]
		public const int NS_E_ICMQUERYFORMAT = unchecked((int)0xC00D1B6C);

		[Description("It was not possible to create a video capture window.")]
		public const int NS_E_VIDCAPCREATEWINDOW = unchecked((int)0xC00D1B6D);

		[Description("There is already a stream active on this video device.")]
		public const int NS_E_VIDCAPDRVINUSE = unchecked((int)0xC00D1B6E);

		[Description("No media format is set in source.")]
		public const int NS_E_NO_MEDIAFORMAT_IN_SOURCE = unchecked((int)0xC00D1B6F);

		[Description("Cannot find a valid output stream from the source.")]
		public const int NS_E_NO_VALID_OUTPUT_STREAM = unchecked((int)0xC00D1B70);

		[Description("It was not possible to find a valid source plug-in for the specified source.")]
		public const int NS_E_NO_VALID_SOURCE_PLUGIN = unchecked((int)0xC00D1B71);

		[Description("No source is currently active.")]
		public const int NS_E_NO_ACTIVE_SOURCEGROUP = unchecked((int)0xC00D1B72);

		[Description("No script stream is set in the current source.")]
		public const int NS_E_NO_SCRIPT_STREAM = unchecked((int)0xC00D1B73);

		[Description("This operation is not allowed while archiving.")]
		public const int NS_E_INVALIDCALL_WHILE_ARCHIVAL_RUNNING = unchecked((int)0xC00D1B74);

		[Description("The setting for the maximum packet size is not valid.")]
		public const int NS_E_INVALIDPACKETSIZE = unchecked((int)0xC00D1B75);

		[Description("The plug-in CLSID specified is not valid.")]
		public const int NS_E_PLUGIN_CLSID_INVALID = unchecked((int)0xC00D1B76);

		[Description("This archive type is not supported.")]
		public const int NS_E_UNSUPPORTED_ARCHIVETYPE = unchecked((int)0xC00D1B77);

		[Description("This archive operation is not supported.")]
		public const int NS_E_UNSUPPORTED_ARCHIVEOPERATION = unchecked((int)0xC00D1B78);

		[Description("The local archive file name was not set.")]
		public const int NS_E_ARCHIVE_FILENAME_NOTSET = unchecked((int)0xC00D1B79);

		[Description("The source is not yet prepared.")]
		public const int NS_E_SOURCEGROUP_NOTPREPARED = unchecked((int)0xC00D1B7A);

		[Description("Profiles on the sources do not match.")]
		public const int NS_E_PROFILE_MISMATCH = unchecked((int)0xC00D1B7B);

		[Description("The specified crop values are not valid.")]
		public const int NS_E_INCORRECTCLIPSETTINGS = unchecked((int)0xC00D1B7C);

		[Description("No statistics are available at this time.")]
		public const int NS_E_NOSTATSAVAILABLE = unchecked((int)0xC00D1B7D);

		[Description("The encoder is not archiving.")]
		public const int NS_E_NOTARCHIVING = unchecked((int)0xC00D1B7E);

		[Description("This operation is only allowed during encoding.")]
		public const int NS_E_INVALIDCALL_WHILE_ENCODER_STOPPED = unchecked((int)0xC00D1B7F);

		[Description("This SourceGroupCollection doesn't contain any SourceGroups.")]
		public const int NS_E_NOSOURCEGROUPS = unchecked((int)0xC00D1B80);

		[Description("This source does not have a frame rate of 30 fps. Therefore, it is not possible to apply the inverse telecine filter to the source.")]
		public const int NS_E_INVALIDINPUTFPS = unchecked((int)0xC00D1B81);

		[Description("It is not possible to display your source or output video in the Video panel.")]
		public const int NS_E_NO_DATAVIEW_SUPPORT = unchecked((int)0xC00D1B82);

		[Description("One or more codecs required to open this content could not be found.")]
		public const int NS_E_CODEC_UNAVAILABLE = unchecked((int)0xC00D1B83);

		[Description("The archive file has the same name as an input file. Change one of the names before continuing.")]
		public const int NS_E_ARCHIVE_SAME_AS_INPUT = unchecked((int)0xC00D1B84);

		[Description("The source has not been set up completely.")]
		public const int NS_E_SOURCE_NOTSPECIFIED = unchecked((int)0xC00D1B85);

		[Description("It is not possible to apply time compression to a broadcast session.")]
		public const int NS_E_NO_REALTIME_TIMECOMPRESSION = unchecked((int)0xC00D1B86);

		[Description("It is not possible to open this device.")]
		public const int NS_E_UNSUPPORTED_ENCODER_DEVICE = unchecked((int)0xC00D1B87);

		[Description("It is not possible to start encoding because the display size or color has changed since the current session was defined. Restore the previous settings or create a new session.")]
		public const int NS_E_UNEXPECTED_DISPLAY_SETTINGS = unchecked((int)0xC00D1B88);

		[Description("No audio data has been received for several seconds. Check the audio source and restart the encoder.")]
		public const int NS_E_NO_AUDIODATA = unchecked((int)0xC00D1B89);

		[Description("One or all of the specified sources are not working properly. Check that the sources are configured correctly.")]
		public const int NS_E_INPUTSOURCE_PROBLEM = unchecked((int)0xC00D1B8A);

		[Description("The supplied configuration file is not supported by this version of the encoder.")]
		public const int NS_E_WME_VERSION_MISMATCH = unchecked((int)0xC00D1B8B);

		[Description("It is not possible to use image preprocessing with live encoding.")]
		public const int NS_E_NO_REALTIME_PREPROCESS = unchecked((int)0xC00D1B8C);

		[Description("It is not possible to use two-pass encoding when the source is set to loop.")]
		public const int NS_E_NO_REPEAT_PREPROCESS = unchecked((int)0xC00D1B8D);

		[Description("It is not possible to pause encoding during a broadcast.")]
		public const int NS_E_CANNOT_PAUSE_LIVEBROADCAST = unchecked((int)0xC00D1B8E);

		[Description("A DRM profile has not been set for the current session.")]
		public const int NS_E_DRM_PROFILE_NOT_SET = unchecked((int)0xC00D1B8F);

		[Description("The profile ID is already used by a DRM profile. Specify a different profile ID.")]
		public const int NS_E_DUPLICATE_DRMPROFILE = unchecked((int)0xC00D1B90);

		[Description("The setting of the selected device does not support control for playing back tapes.")]
		public const int NS_E_INVALID_DEVICE = unchecked((int)0xC00D1B91);

		[Description("You must specify a mixed voice and audio mode in order to use an optimization definition file.")]
		public const int NS_E_SPEECHEDL_ON_NON_MIXEDMODE = unchecked((int)0xC00D1B92);

		[Description("The specified password is too long. Type a password with fewer than 8 characters.")]
		public const int NS_E_DRM_PASSWORD_TOO_LONG = unchecked((int)0xC00D1B93);

		[Description("It is not possible to seek to the specified mark-in point.")]
		public const int NS_E_DEVCONTROL_FAILED_SEEK = unchecked((int)0xC00D1B94);

		[Description("When you choose to maintain the interlacing in your video, the output video size must match the input video size.")]
		public const int NS_E_INTERLACE_REQUIRE_SAMESIZE = unchecked((int)0xC00D1B95);

		[Description("Only one device control plug-in can control a device.")]
		public const int NS_E_TOO_MANY_DEVICECONTROL = unchecked((int)0xC00D1B96);

		[Description("You must also enable storing content to hard disk temporarily in order to use two-pass encoding with the input device.")]
		public const int NS_E_NO_MULTIPASS_FOR_LIVEDEVICE = unchecked((int)0xC00D1B97);

		[Description("An audience is missing from the output stream configuration.")]
		public const int NS_E_MISSING_AUDIENCE = unchecked((int)0xC00D1B98);

		[Description("All audiences in the output tree must have the same content type.")]
		public const int NS_E_AUDIENCE_CONTENTTYPE_MISMATCH = unchecked((int)0xC00D1B99);

		[Description("A source index is missing from the output stream configuration.")]
		public const int NS_E_MISSING_SOURCE_INDEX = unchecked((int)0xC00D1B9A);

		[Description("The same source index in different audiences should have the same number of languages.")]
		public const int NS_E_NUM_LANGUAGE_MISMATCH = unchecked((int)0xC00D1B9B);

		[Description("The same source index in different audiences should have the same languages.")]
		public const int NS_E_LANGUAGE_MISMATCH = unchecked((int)0xC00D1B9C);

		[Description("The same source index in different audiences should use the same VBR encoding mode.")]
		public const int NS_E_VBRMODE_MISMATCH = unchecked((int)0xC00D1B9D);

		[Description("The bit rate index specified is not valid.")]
		public const int NS_E_INVALID_INPUT_AUDIENCE_INDEX = unchecked((int)0xC00D1B9E);

		[Description("The specified language is not valid.")]
		public const int NS_E_INVALID_INPUT_LANGUAGE = unchecked((int)0xC00D1B9F);

		[Description("The specified source type is not valid.")]
		public const int NS_E_INVALID_INPUT_STREAM = unchecked((int)0xC00D1BA0);

		[Description("The source must be a mono channel .wav file.")]
		public const int NS_E_EXPECT_MONO_WAV_INPUT = unchecked((int)0xC00D1BA1);

		[Description("All the source .wav files must have the same format.")]
		public const int NS_E_INPUT_WAVFORMAT_MISMATCH = unchecked((int)0xC00D1BA2);

		[Description("The hard disk being used for temporary storage of content has reached the minimum allowed disk space. Create more space on the hard disk and restart encoding.")]
		public const int NS_E_RECORDQ_DISK_FULL = unchecked((int)0xC00D1BA3);

		[Description("It is not possible to apply the inverse telecine feature to PAL content.")]
		public const int NS_E_NO_PAL_INVERSE_TELECINE = unchecked((int)0xC00D1BA4);

		[Description("A capture device in the current active source is no longer available.")]
		public const int NS_E_ACTIVE_SG_DEVICE_DISCONNECTED = unchecked((int)0xC00D1BA5);

		[Description("A device used in the current active source for device control is no longer available.")]
		public const int NS_E_ACTIVE_SG_DEVICE_CONTROL_DISCONNECTED = unchecked((int)0xC00D1BA6);

		[Description("No frames have been submitted to the analyzer for analysis.")]
		public const int NS_E_NO_FRAMES_SUBMITTED_TO_ANALYZER = unchecked((int)0xC00D1BA7);

		[Description("The source video does not support time codes.")]
		public const int NS_E_INPUT_DOESNOT_SUPPORT_SMPTE = unchecked((int)0xC00D1BA8);

		[Description("It is not possible to generate a time code when there are multiple sources in a session.")]
		public const int NS_E_NO_SMPTE_WITH_MULTIPLE_SOURCEGROUPS = unchecked((int)0xC00D1BA9);

		[Description("The voice codec optimization definition file cannot be found or is corrupted.")]
		public const int NS_E_BAD_CONTENTEDL = unchecked((int)0xC00D1BAA);

		[Description("The same source index in different audiences should have the same interlace mode.")]
		public const int NS_E_INTERLACEMODE_MISMATCH = unchecked((int)0xC00D1BAB);

		[Description("The same source index in different audiences should have the same nonsquare pixel mode.")]
		public const int NS_E_NONSQUAREPIXELMODE_MISMATCH = unchecked((int)0xC00D1BAC);

		[Description("The same source index in different audiences should have the same time code mode.")]
		public const int NS_E_SMPTEMODE_MISMATCH = unchecked((int)0xC00D1BAD);

		[Description("Either the end of the tape has been reached or there is no tape. Check the device and tape.")]
		public const int NS_E_END_OF_TAPE = unchecked((int)0xC00D1BAE);

		[Description("No audio or video input has been specified.")]
		public const int NS_E_NO_MEDIA_IN_AUDIENCE = unchecked((int)0xC00D1BAF);

		[Description("The profile must contain a bit rate.")]
		public const int NS_E_NO_AUDIENCES = unchecked((int)0xC00D1BB0);

		[Description("You must specify at least one audio stream to be compatible with Windows Media Player 7.1.")]
		public const int NS_E_NO_AUDIO_COMPAT = unchecked((int)0xC00D1BB1);

		[Description("Using a VBR encoding mode is not compatible with Windows Media Player 7.1.")]
		public const int NS_E_INVALID_VBR_COMPAT = unchecked((int)0xC00D1BB2);

		[Description("You must specify a profile name.")]
		public const int NS_E_NO_PROFILE_NAME = unchecked((int)0xC00D1BB3);

		[Description("It is not possible to use a VBR encoding mode with uncompressed audio or video.")]
		public const int NS_E_INVALID_VBR_WITH_UNCOMP = unchecked((int)0xC00D1BB4);

		[Description("It is not possible to use MBR encoding with VBR encoding.")]
		public const int NS_E_MULTIPLE_VBR_AUDIENCES = unchecked((int)0xC00D1BB5);

		[Description("It is not possible to mix uncompressed and compressed content in a session.")]
		public const int NS_E_UNCOMP_COMP_COMBINATION = unchecked((int)0xC00D1BB6);

		[Description("All audiences must use the same audio codec.")]
		public const int NS_E_MULTIPLE_AUDIO_CODECS = unchecked((int)0xC00D1BB7);

		[Description("All audiences should use the same audio format to be compatible with Windows Media Player 7.1.")]
		public const int NS_E_MULTIPLE_AUDIO_FORMATS = unchecked((int)0xC00D1BB8);

		[Description("The audio bit rate for an audience with a higher total bit rate must be greater than one with a lower total bit rate.")]
		public const int NS_E_AUDIO_BITRATE_STEPDOWN = unchecked((int)0xC00D1BB9);

		[Description("The audio peak bit rate setting is not valid.")]
		public const int NS_E_INVALID_AUDIO_PEAKRATE = unchecked((int)0xC00D1BBA);

		[Description("The audio peak bit rate setting must be greater than the audio bit rate setting.")]
		public const int NS_E_INVALID_AUDIO_PEAKRATE_2 = unchecked((int)0xC00D1BBB);

		[Description("The setting for the maximum buffer size for audio is not valid.")]
		public const int NS_E_INVALID_AUDIO_BUFFERMAX = unchecked((int)0xC00D1BBC);

		[Description("All audiences must use the same video codec.")]
		public const int NS_E_MULTIPLE_VIDEO_CODECS = unchecked((int)0xC00D1BBD);

		[Description("All audiences should use the same video size to be compatible with Windows Media Player 7.1.")]
		public const int NS_E_MULTIPLE_VIDEO_SIZES = unchecked((int)0xC00D1BBE);

		[Description("The video bit rate setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_BITRATE = unchecked((int)0xC00D1BBF);

		[Description("The video bit rate for an audience with a higher total bit rate must be greater than one with a lower total bit rate.")]
		public const int NS_E_VIDEO_BITRATE_STEPDOWN = unchecked((int)0xC00D1BC0);

		[Description("The video peak bit rate setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_PEAKRATE = unchecked((int)0xC00D1BC1);

		[Description("The video peak bit rate setting must be greater than the video bit rate setting.")]
		public const int NS_E_INVALID_VIDEO_PEAKRATE_2 = unchecked((int)0xC00D1BC2);

		[Description("The video width setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_WIDTH = unchecked((int)0xC00D1BC3);

		[Description("The video height setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_HEIGHT = unchecked((int)0xC00D1BC4);

		[Description("The video frame rate setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_FPS = unchecked((int)0xC00D1BC5);

		[Description("The video key frame setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_KEYFRAME = unchecked((int)0xC00D1BC6);

		[Description("The video image quality setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_IQUALITY = unchecked((int)0xC00D1BC7);

		[Description("The video codec quality setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_CQUALITY = unchecked((int)0xC00D1BC8);

		[Description("The video buffer setting is not valid.")]
		public const int NS_E_INVALID_VIDEO_BUFFER = unchecked((int)0xC00D1BC9);

		[Description("The setting for the maximum buffer size for video is not valid.")]
		public const int NS_E_INVALID_VIDEO_BUFFERMAX = unchecked((int)0xC00D1BCA);

		[Description("The value of the video maximum buffer size setting must be greater than the video buffer size setting.")]
		public const int NS_E_INVALID_VIDEO_BUFFERMAX_2 = unchecked((int)0xC00D1BCB);

		[Description("The alignment of the video width is not valid.")]
		public const int NS_E_INVALID_VIDEO_WIDTH_ALIGN = unchecked((int)0xC00D1BCC);

		[Description("The alignment of the video height is not valid.")]
		public const int NS_E_INVALID_VIDEO_HEIGHT_ALIGN = unchecked((int)0xC00D1BCD);

		[Description("All bit rates must have the same script bit rate.")]
		public const int NS_E_MULTIPLE_SCRIPT_BITRATES = unchecked((int)0xC00D1BCE);

		[Description("The script bit rate specified is not valid.")]
		public const int NS_E_INVALID_SCRIPT_BITRATE = unchecked((int)0xC00D1BCF);

		[Description("All bit rates must have the same file transfer bit rate.")]
		public const int NS_E_MULTIPLE_FILE_BITRATES = unchecked((int)0xC00D1BD0);

		[Description("The file transfer bit rate is not valid.")]
		public const int NS_E_INVALID_FILE_BITRATE = unchecked((int)0xC00D1BD1);

		[Description("All audiences in a profile should either be same as input or have video width and height specified.")]
		public const int NS_E_SAME_AS_INPUT_COMBINATION = unchecked((int)0xC00D1BD2);

		[Description("This source type does not support looping.")]
		public const int NS_E_SOURCE_CANNOT_LOOP = unchecked((int)0xC00D1BD3);

		[Description("The fold-down value needs to be between -144 and 0.")]
		public const int NS_E_INVALID_FOLDDOWN_COEFFICIENTS = unchecked((int)0xC00D1BD4);

		[Description("The specified DRM profile does not exist in the system.")]
		public const int NS_E_DRMPROFILE_NOTFOUND = unchecked((int)0xC00D1BD5);

		[Description("The specified time code is not valid.")]
		public const int NS_E_INVALID_TIMECODE = unchecked((int)0xC00D1BD6);

		[Description("It is not possible to apply time compression to a video-only session.")]
		public const int NS_E_NO_AUDIO_TIMECOMPRESSION = unchecked((int)0xC00D1BD7);

		[Description("It is not possible to apply time compression to a session that is using two-pass encoding.")]
		public const int NS_E_NO_TWOPASS_TIMECOMPRESSION = unchecked((int)0xC00D1BD8);

		[Description("It is not possible to generate a time code for an audio-only session.")]
		public const int NS_E_TIMECODE_REQUIRES_VIDEOSTREAM = unchecked((int)0xC00D1BD9);

		[Description("It is not possible to generate a time code when you are encoding content at multiple bit rates.")]
		public const int NS_E_NO_MBR_WITH_TIMECODE = unchecked((int)0xC00D1BDA);

		[Description("The video codec selected does not support maintaining interlacing in video.")]
		public const int NS_E_INVALID_INTERLACEMODE = unchecked((int)0xC00D1BDB);

		[Description("Maintaining interlacing in video is not compatible with Windows Media Player 7.1.")]
		public const int NS_E_INVALID_INTERLACE_COMPAT = unchecked((int)0xC00D1BDC);

		[Description("Allowing nonsquare pixel output is not compatible with Windows Media Player 7.1.")]
		public const int NS_E_INVALID_NONSQUAREPIXEL_COMPAT = unchecked((int)0xC00D1BDD);

		[Description("Only capture devices can be used with device control.")]
		public const int NS_E_INVALID_SOURCE_WITH_DEVICE_CONTROL = unchecked((int)0xC00D1BDE);

		[Description("It is not possible to generate the stream format file if you are using quality-based VBR encoding for the audio or video stream. Instead use the Windows Media file generated after encoding to create the announcement file.")]
		public const int NS_E_CANNOT_GENERATE_BROADCAST_INFO_FOR_QUALITYVBR = unchecked((int)0xC00D1BDF);

		[Description("It is not possible to create a DRM profile because the maximum number of profiles has been reached. You must delete some DRM profiles before creating new ones.")]
		public const int NS_E_EXCEED_MAX_DRM_PROFILE_LIMIT = unchecked((int)0xC00D1BE0);

		[Description("The device is in an unstable state. Check that the device is functioning properly and a tape is in place.")]
		public const int NS_E_DEVICECONTROL_UNSTABLE = unchecked((int)0xC00D1BE1);

		[Description("The pixel aspect ratio value must be between 1 and 255.")]
		public const int NS_E_INVALID_PIXEL_ASPECT_RATIO = unchecked((int)0xC00D1BE2);

		[Description("All streams with different languages in the same audience must have same properties.")]
		public const int NS_E_AUDIENCE__LANGUAGE_CONTENTTYPE_MISMATCH = unchecked((int)0xC00D1BE3);

		[Description("The profile must contain at least one audio or video stream.")]
		public const int NS_E_INVALID_PROFILE_CONTENTTYPE = unchecked((int)0xC00D1BE4);

		[Description("The transform plug-in could not be found.")]
		public const int NS_E_TRANSFORM_PLUGIN_NOT_FOUND = unchecked((int)0xC00D1BE5);

		[Description("The transform plug-in is not valid. It might be damaged or you might not have the required permissions to access the plug-in.")]
		public const int NS_E_TRANSFORM_PLUGIN_INVALID = unchecked((int)0xC00D1BE6);

		[Description("To use two-pass encoding, you must enable device control and setup an edit decision list (EDL) that has at least one entry.")]
		public const int NS_E_EDL_REQUIRED_FOR_DEVICE_MULTIPASS = unchecked((int)0xC00D1BE7);

		[Description("When you choose to maintain the interlacing in your video, the output video size must be a multiple of 4.")]
		public const int NS_E_INVALID_VIDEO_WIDTH_FOR_INTERLACED_ENCODING = unchecked((int)0xC00D1BE8);

		[Description("Markin/Markout is unsupported with this source type.")]
		public const int NS_E_MARKIN_UNSUPPORTED = unchecked((int)0xC00D1BE9);

		[Description("A problem has occurred in the Digital Rights Management component. Contact product support for this application.")]
		public const int NS_E_DRM_INVALID_APPLICATION = unchecked((int)0xC00D2711);

		[Description("License storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_LICENSE_STORE_ERROR = unchecked((int)0xC00D2712);

		[Description("Secure storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_SECURE_STORE_ERROR = unchecked((int)0xC00D2713);

		[Description("License acquisition did not work. Acquire a new license or contact the content provider for further assistance.")]
		public const int NS_E_DRM_LICENSE_STORE_SAVE_ERROR = unchecked((int)0xC00D2714);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_SECURE_STORE_UNLOCK_ERROR = unchecked((int)0xC00D2715);

		[Description("The media file is corrupted. Contact the content provider to get a new file.")]
		public const int NS_E_DRM_INVALID_CONTENT = unchecked((int)0xC00D2716);

		[Description("The license is corrupted. Acquire a new license.")]
		public const int NS_E_DRM_UNABLE_TO_OPEN_LICENSE = unchecked((int)0xC00D2717);

		[Description("The license is corrupted or invalid. Acquire a new license")]
		public const int NS_E_DRM_INVALID_LICENSE = unchecked((int)0xC00D2718);

		[Description("Licenses cannot be copied from one computer to another. Use License Management to transfer licenses, or get a new license for the media file.")]
		public const int NS_E_DRM_INVALID_MACHINE = unchecked((int)0xC00D2719);

		[Description("License storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_ENUM_LICENSE_FAILED = unchecked((int)0xC00D271B);

		[Description("The media file is corrupted. Contact the content provider to get a new file.")]
		public const int NS_E_DRM_INVALID_LICENSE_REQUEST = unchecked((int)0xC00D271C);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_INITIALIZE = unchecked((int)0xC00D271D);

		[Description("The license could not be acquired. Try again later.")]
		public const int NS_E_DRM_UNABLE_TO_ACQUIRE_LICENSE = unchecked((int)0xC00D271E);

		[Description("License acquisition did not work. Acquire a new license or contact the content provider for further assistance.")]
		public const int NS_E_DRM_INVALID_LICENSE_ACQUIRED = unchecked((int)0xC00D271F);

		[Description("The requested operation cannot be performed on this file.")]
		public const int NS_E_DRM_NO_RIGHTS = unchecked((int)0xC00D2720);

		[Description("The requested action cannot be performed because a problem occurred with the Windows Media Digital Rights Management (DRM) components on your computer.")]
		public const int NS_E_DRM_KEY_ERROR = unchecked((int)0xC00D2721);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_ENCRYPT_ERROR = unchecked((int)0xC00D2722);

		[Description("The media file is corrupted. Contact the content provider to get a new file.")]
		public const int NS_E_DRM_DECRYPT_ERROR = unchecked((int)0xC00D2723);

		[Description("The license is corrupted. Acquire a new license.")]
		public const int NS_E_DRM_LICENSE_INVALID_XML = unchecked((int)0xC00D2725);

		[Description("A security upgrade is required to perform the operation on this media file.")]
		public const int NS_E_DRM_NEEDS_INDIVIDUALIZATION = unchecked((int)0xC00D2728);

		[Description("You already have the latest security components. No upgrade is necessary at this time.")]
		public const int NS_E_DRM_ALREADY_INDIVIDUALIZED = unchecked((int)0xC00D2729);

		[Description("The application cannot perform this action. Contact product support for this application.")]
		public const int NS_E_DRM_ACTION_NOT_QUERIED = unchecked((int)0xC00D272A);

		[Description("You cannot begin a new license acquisition process until the current one has been completed.")]
		public const int NS_E_DRM_ACQUIRING_LICENSE = unchecked((int)0xC00D272B);

		[Description("You cannot begin a new security upgrade until the current one has been completed.")]
		public const int NS_E_DRM_INDIVIDUALIZING = unchecked((int)0xC00D272C);

		[Description("Failure in Backup-Restore.")]
		public const int NS_E_BACKUP_RESTORE_FAILURE = unchecked((int)0xC00D272D);

		[Description("Bad Request ID in Backup-Restore.")]
		public const int NS_E_BACKUP_RESTORE_BAD_REQUEST_ID = unchecked((int)0xC00D272E);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_PARAMETERS_MISMATCHED = unchecked((int)0xC00D272F);

		[Description("A license cannot be created for this media file. Reinstall the application.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_LICENSE_OBJECT = unchecked((int)0xC00D2730);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_INDI_OBJECT = unchecked((int)0xC00D2731);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_ENCRYPT_OBJECT = unchecked((int)0xC00D2732);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_DECRYPT_OBJECT = unchecked((int)0xC00D2733);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_PROPERTIES_OBJECT = unchecked((int)0xC00D2734);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_BACKUP_OBJECT = unchecked((int)0xC00D2735);

		[Description("The security upgrade failed. Try again later.")]
		public const int NS_E_DRM_INDIVIDUALIZE_ERROR = unchecked((int)0xC00D2736);

		[Description("License storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_LICENSE_OPEN_ERROR = unchecked((int)0xC00D2737);

		[Description("License storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_LICENSE_CLOSE_ERROR = unchecked((int)0xC00D2738);

		[Description("License storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_GET_LICENSE_ERROR = unchecked((int)0xC00D2739);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_QUERY_ERROR = unchecked((int)0xC00D273A);

		[Description("A problem has occurred in the Digital Rights Management component. Contact product support for this application.")]
		public const int NS_E_DRM_REPORT_ERROR = unchecked((int)0xC00D273B);

		[Description("License storage is not working. Contact Microsoft product support.")]
		public const int NS_E_DRM_GET_LICENSESTRING_ERROR = unchecked((int)0xC00D273C);

		[Description("The media file is corrupted. Contact the content provider to get a new file.")]
		public const int NS_E_DRM_GET_CONTENTSTRING_ERROR = unchecked((int)0xC00D273D);

		[Description("A problem has occurred in the Digital Rights Management component. Try again later.")]
		public const int NS_E_DRM_MONITOR_ERROR = unchecked((int)0xC00D273E);

		[Description("The application has made an invalid call to the Digital Rights Management component. Contact product support for this application.")]
		public const int NS_E_DRM_UNABLE_TO_SET_PARAMETER = unchecked((int)0xC00D273F);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_INVALID_APPDATA = unchecked((int)0xC00D2740);

		[Description("A problem has occurred in the Digital Rights Management component. Contact product support for this application.")]
		public const int NS_E_DRM_INVALID_APPDATA_VERSION = unchecked((int)0xC00D2741);

		[Description("Licenses are already backed up in this location.")]
		public const int NS_E_DRM_BACKUP_EXISTS = unchecked((int)0xC00D2742);

		[Description("One or more backed-up licenses are missing or corrupt.")]
		public const int NS_E_DRM_BACKUP_CORRUPT = unchecked((int)0xC00D2743);

		[Description("You cannot begin a new backup process until the current process has been completed.")]
		public const int NS_E_DRM_BACKUPRESTORE_BUSY = unchecked((int)0xC00D2744);

		[Description("Bad Data sent to Backup-Restore.")]
		public const int NS_E_BACKUP_RESTORE_BAD_DATA = unchecked((int)0xC00D2745);

		[Description("The license is invalid. Contact the content provider for further assistance.")]
		public const int NS_E_DRM_LICENSE_UNUSABLE = unchecked((int)0xC00D2748);

		[Description("A required property was not set by the application. Contact product support for this application.")]
		public const int NS_E_DRM_INVALID_PROPERTY = unchecked((int)0xC00D2749);

		[Description("A problem has occurred in the Digital Rights Management component of this application. Try to acquire a license again.")]
		public const int NS_E_DRM_SECURE_STORE_NOT_FOUND = unchecked((int)0xC00D274A);

		[Description("A license cannot be found for this media file. Use License Management to transfer a license for this file from the original computer, or acquire a new license.")]
		public const int NS_E_DRM_CACHED_CONTENT_ERROR = unchecked((int)0xC00D274B);

		[Description("A problem occurred during the security upgrade. Try again later.")]
		public const int NS_E_DRM_INDIVIDUALIZATION_INCOMPLETE = unchecked((int)0xC00D274C);

		[Description("Certified driver components are required to play this media file. Contact Windows Update to see whether updated drivers are available for your hardware.")]
		public const int NS_E_DRM_DRIVER_AUTH_FAILURE = unchecked((int)0xC00D274D);

		[Description("One or more of the Secure Audio Path components were not found or an entry point in those components was not found.")]
		public const int NS_E_DRM_NEED_UPGRADE_MSSAP = unchecked((int)0xC00D274E);

		[Description("Status message: Reopen the file.")]
		public const int NS_E_DRM_REOPEN_CONTENT = unchecked((int)0xC00D274F);

		[Description("Certain driver functionality is required to play this media file. Contact Windows Update to see whether updated drivers are available for your hardware.")]
		public const int NS_E_DRM_DRIVER_DIGIOUT_FAILURE = unchecked((int)0xC00D2750);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_INVALID_SECURESTORE_PASSWORD = unchecked((int)0xC00D2751);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_APPCERT_REVOKED = unchecked((int)0xC00D2752);

		[Description("You cannot restore your license(s).")]
		public const int NS_E_DRM_RESTORE_FRAUD = unchecked((int)0xC00D2753);

		[Description("The licenses for your media files are corrupted. Contact Microsoft product support.")]
		public const int NS_E_DRM_HARDWARE_INCONSISTENT = unchecked((int)0xC00D2754);

		[Description("To transfer this media file, you must upgrade the application.")]
		public const int NS_E_DRM_SDMI_TRIGGER = unchecked((int)0xC00D2755);

		[Description("You cannot make any more copies of this media file.")]
		public const int NS_E_DRM_SDMI_NOMORECOPIES = unchecked((int)0xC00D2756);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_HEADER_OBJECT = unchecked((int)0xC00D2757);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_KEYS_OBJECT = unchecked((int)0xC00D2758);

		[Description("Unable to obtain license.")]
		public const int NS_E_DRM_LICENSE_NOTACQUIRED = unchecked((int)0xC00D2759);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_CODING_OBJECT = unchecked((int)0xC00D275A);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_STATE_DATA_OBJECT = unchecked((int)0xC00D275B);

		[Description("The buffer supplied is not sufficient.")]
		public const int NS_E_DRM_BUFFER_TOO_SMALL = unchecked((int)0xC00D275C);

		[Description("The property requested is not supported.")]
		public const int NS_E_DRM_UNSUPPORTED_PROPERTY = unchecked((int)0xC00D275D);

		[Description("The specified server cannot perform the requested operation.")]
		public const int NS_E_DRM_ERROR_BAD_NET_RESP = unchecked((int)0xC00D275E);

		[Description("Some of the licenses could not be stored.")]
		public const int NS_E_DRM_STORE_NOTALLSTORED = unchecked((int)0xC00D275F);

		[Description("The Digital Rights Management security upgrade component could not be validated. Contact Microsoft product support.")]
		public const int NS_E_DRM_SECURITY_COMPONENT_SIGNATURE_INVALID = unchecked((int)0xC00D2760);

		[Description("Invalid or corrupt data was encountered.")]
		public const int NS_E_DRM_INVALID_DATA = unchecked((int)0xC00D2761);

		[Description("The Windows Media Digital Rights Management system cannot perform the requested action because your computer or network administrator has enabled the group policy Prevent Windows Media DRM Internet Access. For assistance, contact your administrator.")]
		public const int NS_E_DRM_POLICY_DISABLE_ONLINE = unchecked((int)0xC00D2762);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_AUTHENTICATION_OBJECT = unchecked((int)0xC00D2763);

		[Description("Not all of the necessary properties for DRM have been set.")]
		public const int NS_E_DRM_NOT_CONFIGURED = unchecked((int)0xC00D2764);

		[Description("The portable device does not have the security required to copy protected files to it. To obtain the additional security, try to copy the file to your portable device again. When a message appears, click OK.")]
		public const int NS_E_DRM_DEVICE_ACTIVATION_CANCELED = unchecked((int)0xC00D2765);

		[Description("Too many resets in Backup-Restore.")]
		public const int NS_E_BACKUP_RESTORE_TOO_MANY_RESETS = unchecked((int)0xC00D2766);

		[Description("Running this process under a debugger while using DRM content is not allowed.")]
		public const int NS_E_DRM_DEBUGGING_NOT_ALLOWED = unchecked((int)0xC00D2767);

		[Description("The user canceled the DRM operation.")]
		public const int NS_E_DRM_OPERATION_CANCELED = unchecked((int)0xC00D2768);

		[Description("The license you are using has assocaited output restrictions. This license is unusable until these restrictions are queried.")]
		public const int NS_E_DRM_RESTRICTIONS_NOT_RETRIEVED = unchecked((int)0xC00D2769);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_PLAYLIST_OBJECT = unchecked((int)0xC00D276A);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_PLAYLIST_BURN_OBJECT = unchecked((int)0xC00D276B);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_DEVICE_REGISTRATION_OBJECT = unchecked((int)0xC00D276C);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_METERING_OBJECT = unchecked((int)0xC00D276D);

		[Description("The specified track has exceeded it's specified playlist burn limit in this playlist.")]
		public const int NS_E_DRM_TRACK_EXCEEDED_PLAYLIST_RESTICTION = unchecked((int)0xC00D2770);

		[Description("The specified track has exceeded it's track burn limit.")]
		public const int NS_E_DRM_TRACK_EXCEEDED_TRACKBURN_RESTRICTION = unchecked((int)0xC00D2771);

		[Description("A problem has occurred in obtaining the device's certificate. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_GET_DEVICE_CERT = unchecked((int)0xC00D2772);

		[Description("A problem has occurred in obtaining the device's secure clock. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_GET_SECURE_CLOCK = unchecked((int)0xC00D2773);

		[Description("A problem has occurred in setting the device's secure clock. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_SET_SECURE_CLOCK = unchecked((int)0xC00D2774);

		[Description("A problem has occurred in obtaining the secure clock from server. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_GET_SECURE_CLOCK_FROM_SERVER = unchecked((int)0xC00D2775);

		[Description("This content requires the metering policy to be enabled.")]
		public const int NS_E_DRM_POLICY_METERING_DISABLED = unchecked((int)0xC00D2776);

		[Description("Transfer of chained licenses unsupported.")]
		public const int NS_E_DRM_TRANSFER_CHAINED_LICENSES_UNSUPPORTED = unchecked((int)0xC00D2777);

		[Description("The Digital Rights Management component is not installed properly. Reinstall the Player.")]
		public const int NS_E_DRM_SDK_VERSIONMISMATCH = unchecked((int)0xC00D2778);

		[Description("The file could not be transferred because the device clock is not set.")]
		public const int NS_E_DRM_LIC_NEEDS_DEVICE_CLOCK_SET = unchecked((int)0xC00D2779);

		[Description("The content header is missing an acquisition URL.")]
		public const int NS_E_LICENSE_HEADER_MISSING_URL = unchecked((int)0xC00D277A);

		[Description("The current attached device does not support WMDRM.")]
		public const int NS_E_DEVICE_NOT_WMDRM_DEVICE = unchecked((int)0xC00D277B);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_INVALID_APPCERT = unchecked((int)0xC00D277C);

		[Description("The client application has been forcefully terminated during a DRM petition.")]
		public const int NS_E_DRM_PROTOCOL_FORCEFUL_TERMINATION_ON_PETITION = unchecked((int)0xC00D277D);

		[Description("The client application has been forcefully terminated during a DRM challenge.")]
		public const int NS_E_DRM_PROTOCOL_FORCEFUL_TERMINATION_ON_CHALLENGE = unchecked((int)0xC00D277E);

		[Description("Secure storage protection error. Restore your licenses from a previous backup and try again.")]
		public const int NS_E_DRM_CHECKPOINT_FAILED = unchecked((int)0xC00D277F);

		[Description("A problem has occurred in the Digital Rights Management root of trust. Contact Microsoft product support.")]
		public const int NS_E_DRM_BB_UNABLE_TO_INITIALIZE = unchecked((int)0xC00D2780);

		[Description("A problem has occurred in retrieving the Digital Rights Management machine identification. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_LOAD_HARDWARE_ID = unchecked((int)0xC00D2781);

		[Description("A problem has occurred in opening the Digital Rights Management data storage file. Contact Microsoft product.")]
		public const int NS_E_DRM_UNABLE_TO_OPEN_DATA_STORE = unchecked((int)0xC00D2782);

		[Description("The Digital Rights Management data storage is not functioning properly. Contact Microsoft product support.")]
		public const int NS_E_DRM_DATASTORE_CORRUPT = unchecked((int)0xC00D2783);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_INMEMORYSTORE_OBJECT = unchecked((int)0xC00D2784);

		[Description("A secured library is required to access the requested functionality.")]
		public const int NS_E_DRM_STUBLIB_REQUIRED = unchecked((int)0xC00D2785);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_CERTIFICATE_OBJECT = unchecked((int)0xC00D2786);

		[Description("A problem has occurred in the Digital Rights Management component during license migration. Contact Microsoft product support.")]
		public const int NS_E_DRM_MIGRATION_TARGET_NOT_ONLINE = unchecked((int)0xC00D2787);

		[Description("A problem has occurred in the Digital Rights Management component during license migration. Contact Microsoft product support.")]
		public const int NS_E_DRM_INVALID_MIGRATION_IMAGE = unchecked((int)0xC00D2788);

		[Description("A problem has occurred in the Digital Rights Management component during license migration. Contact Microsoft product support.")]
		public const int NS_E_DRM_MIGRATION_TARGET_STATES_CORRUPTED = unchecked((int)0xC00D2789);

		[Description("A problem has occurred in the Digital Rights Management component during license migration. Contact Microsoft product support.")]
		public const int NS_E_DRM_MIGRATION_IMPORTER_NOT_AVAILABLE = unchecked((int)0xC00D278A);

		[Description("A problem has occurred in the Digital Rights Management component during license migration. Contact Microsoft product support.")]
		public const int NS_DRM_E_MIGRATION_UPGRADE_WITH_DIFF_SID = unchecked((int)0xC00D278B);

		[Description("The Digital Rights Management component is in use during license migration. Contact Microsoft product support.")]
		public const int NS_DRM_E_MIGRATION_SOURCE_MACHINE_IN_USE = unchecked((int)0xC00D278C);

		[Description("Licenses are being migrated to a machine running XP or downlevel OS. This operation can only be performed on Windows Vista or a later OS. Contact Microsoft product support.")]
		public const int NS_DRM_E_MIGRATION_TARGET_MACHINE_LESS_THAN_LH = unchecked((int)0xC00D278D);

		[Description("Migration Image already exists. Contact Microsoft product support.")]
		public const int NS_DRM_E_MIGRATION_IMAGE_ALREADY_EXISTS = unchecked((int)0xC00D278E);

		[Description("The requested action cannot be performed because a hardware configuration change has been detected by the Windows Media Digital Rights Management (DRM) components on your computer.")]
		public const int NS_E_DRM_HARDWAREID_MISMATCH = unchecked((int)0xC00D278F);

		[Description("The wrong stublib has been linked to an application or DLL using drmv2clt.dll.")]
		public const int NS_E_INVALID_DRMV2CLT_STUBLIB = unchecked((int)0xC00D2790);

		[Description("The legacy V2 data being imported is invalid.")]
		public const int NS_E_DRM_MIGRATION_INVALID_LEGACYV2_DATA = unchecked((int)0xC00D2791);

		[Description("The license being imported already exists.")]
		public const int NS_E_DRM_MIGRATION_LICENSE_ALREADY_EXISTS = unchecked((int)0xC00D2792);

		[Description("The password of the Legacy V2 SST entry being imported is incorrect.")]
		public const int NS_E_DRM_MIGRATION_INVALID_LEGACYV2_SST_PASSWORD = unchecked((int)0xC00D2793);

		[Description("Migration is not supported by the plugin.")]
		public const int NS_E_DRM_MIGRATION_NOT_SUPPORTED = unchecked((int)0xC00D2794);

		[Description("A migration importer cannot be created for this media file. Reinstall the application.")]
		public const int NS_E_DRM_UNABLE_TO_CREATE_MIGRATION_IMPORTER_OBJECT = unchecked((int)0xC00D2795);

		[Description("The requested action cannot be performed because a problem occurred with the Windows Media Digital Rights Management (DRM) components on your computer.")]
		public const int NS_E_DRM_CHECKPOINT_MISMATCH = unchecked((int)0xC00D2796);

		[Description("The requested action cannot be performed because a problem occurred with the Windows Media Digital Rights Management (DRM) components on your computer.")]
		public const int NS_E_DRM_CHECKPOINT_CORRUPT = unchecked((int)0xC00D2797);

		[Description("The requested action cannot be performed because a problem occurred with the Windows Media Digital Rights Management (DRM) components on your computer.")]
		public const int NS_E_REG_FLUSH_FAILURE = unchecked((int)0xC00D2798);

		[Description("The requested action cannot be performed because a problem occurred with the Windows Media Digital Rights Management (DRM) components on your computer.")]
		public const int NS_E_HDS_KEY_MISMATCH = unchecked((int)0xC00D2799);

		[Description("Migration was canceled by the user.")]
		public const int NS_E_DRM_MIGRATION_OPERATION_CANCELLED = unchecked((int)0xC00D279A);

		[Description("Migration object is already in use and cannot be called until the current operation completes.")]
		public const int NS_E_DRM_MIGRATION_OBJECT_IN_USE = unchecked((int)0xC00D279B);

		[Description("The content header does not comply with DRM requirements and cannot be used.")]
		public const int NS_E_DRM_MALFORMED_CONTENT_HEADER = unchecked((int)0xC00D279C);

		[Description("The license for this file has expired and is no longer valid. Contact your content provider for further assistance.")]
		public const int NS_E_DRM_LICENSE_EXPIRED = unchecked((int)0xC00D27D8);

		[Description("The license for this file is not valid yet, but will be at a future date.")]
		public const int NS_E_DRM_LICENSE_NOTENABLED = unchecked((int)0xC00D27D9);

		[Description("The license for this file requires a higher level of security than the player you are currently using has. Try using a different player or download a newer version of your current player.")]
		public const int NS_E_DRM_LICENSE_APPSECLOW = unchecked((int)0xC00D27DA);

		[Description("The license cannot be stored as it requires security upgrade of Digital Rights Management component.")]
		public const int NS_E_DRM_STORE_NEEDINDI = unchecked((int)0xC00D27DB);

		[Description("Your machine does not meet the requirements for storing the license.")]
		public const int NS_E_DRM_STORE_NOTALLOWED = unchecked((int)0xC00D27DC);

		[Description("The license for this file requires an upgraded version of your player or a different player.")]
		public const int NS_E_DRM_LICENSE_APP_NOTALLOWED = unchecked((int)0xC00D27DD);

		[Description("The license server's certificate expired. Make sure your system clock is set correctly. Contact your content provider for further assistance.")]
		public const int NS_E_DRM_LICENSE_CERT_EXPIRED = unchecked((int)0xC00D27DF);

		[Description("The license for this file requires a higher level of security than the player you are currently using has. Try using a different player or download a newer version of your current player.")]
		public const int NS_E_DRM_LICENSE_SECLOW = unchecked((int)0xC00D27E0);

		[Description("The content owner for the license you just acquired is no longer supporting their content. Contact the content owner for a newer version of the content.")]
		public const int NS_E_DRM_LICENSE_CONTENT_REVOKED = unchecked((int)0xC00D27E1);

		[Description("The content owner for the license you just acquired requires your device to register to the current machine.")]
		public const int NS_E_DRM_DEVICE_NOT_REGISTERED = unchecked((int)0xC00D27E2);

		[Description("The license for this file requires a feature that is not supported in your current player or operating system. You can try with newer version of your current player or contact your content provider for further assistance.")]
		public const int NS_E_DRM_LICENSE_NOSAP = unchecked((int)0xC00D280A);

		[Description("The license for this file requires a feature that is not supported in your current player or operating system. You can try with newer version of your current player or contact your content provider for further assistance.")]
		public const int NS_E_DRM_LICENSE_NOSVP = unchecked((int)0xC00D280B);

		[Description("The license for this file requires Windows Driver Model (WDM) audio drivers. Contact your sound card manufacturer for further assistance.")]
		public const int NS_E_DRM_LICENSE_NOWDM = unchecked((int)0xC00D280C);

		[Description("The license for this file requires a higher level of security than the player you are currently using has. Try using a different player or download a newer version of your current player.")]
		public const int NS_E_DRM_LICENSE_NOTRUSTEDCODEC = unchecked((int)0xC00D280D);

		[Description("The license for this file is not supported by your current player. You can try with newer version of your current player or contact your content provider for further assistance.")]
		public const int NS_E_DRM_SOURCEID_NOT_SUPPORTED = unchecked((int)0xC00D280E);

		[Description("An updated version of your media player is required to play the selected content.")]
		public const int NS_E_DRM_NEEDS_UPGRADE_TEMPFILE = unchecked((int)0xC00D283D);

		[Description("A new version of the Digital Rights Management component is required. Contact product support for this application to get the latest version.")]
		public const int NS_E_DRM_NEED_UPGRADE_PD = unchecked((int)0xC00D283E);

		[Description("Failed to either create or verify the content header.")]
		public const int NS_E_DRM_SIGNATURE_FAILURE = unchecked((int)0xC00D283F);

		[Description("Could not read the necessary information from the system registry.")]
		public const int NS_E_DRM_LICENSE_SERVER_INFO_MISSING = unchecked((int)0xC00D2840);

		[Description("The DRM subsystem is currently locked by another application or user. Try again later.")]
		public const int NS_E_DRM_BUSY = unchecked((int)0xC00D2841);

		[Description("There are too many target devices registered on the portable media.")]
		public const int NS_E_DRM_PD_TOO_MANY_DEVICES = unchecked((int)0xC00D2842);

		[Description("The security upgrade cannot be completed because the allowed number of daily upgrades has been exceeded. Try again tomorrow.")]
		public const int NS_E_DRM_INDIV_FRAUD = unchecked((int)0xC00D2843);

		[Description("The security upgrade cannot be completed because the server is unable to perform the operation. Try again later.")]
		public const int NS_E_DRM_INDIV_NO_CABS = unchecked((int)0xC00D2844);

		[Description("The security upgrade cannot be performed because the server is not available. Try again later.")]
		public const int NS_E_DRM_INDIV_SERVICE_UNAVAILABLE = unchecked((int)0xC00D2845);

		[Description("Windows Media Player cannot restore your licenses because the server is not available. Try again later.")]
		public const int NS_E_DRM_RESTORE_SERVICE_UNAVAILABLE = unchecked((int)0xC00D2846);

		[Description("Windows Media Player cannot play the protected file. Verify that your computer's date is set correctly. If it is correct, on the Help menu, click Check for Player Updates to install the latest version of the Player.")]
		public const int NS_E_DRM_CLIENT_CODE_EXPIRED = unchecked((int)0xC00D2847);

		[Description("The chained license cannot be created because the referenced uplink license does not exist.")]
		public const int NS_E_DRM_NO_UPLINK_LICENSE = unchecked((int)0xC00D2848);

		[Description("The specified KID is invalid.")]
		public const int NS_E_DRM_INVALID_KID = unchecked((int)0xC00D2849);

		[Description("License initialization did not work. Contact Microsoft product support.")]
		public const int NS_E_DRM_LICENSE_INITIALIZATION_ERROR = unchecked((int)0xC00D284A);

		[Description("The uplink license of a chained license cannot itself be a chained license.")]
		public const int NS_E_DRM_CHAIN_TOO_LONG = unchecked((int)0xC00D284C);

		[Description("The specified encryption algorithm is unsupported.")]
		public const int NS_E_DRM_UNSUPPORTED_ALGORITHM = unchecked((int)0xC00D284D);

		[Description("License deletion did not work. Contact Microsoft product support.")]
		public const int NS_E_DRM_LICENSE_DELETION_ERROR = unchecked((int)0xC00D284E);

		[Description("The client's certificate is corrupted or the signature cannot be verified.")]
		public const int NS_E_DRM_INVALID_CERTIFICATE = unchecked((int)0xC00D28A0);

		[Description("The client's certificate has been revoked.")]
		public const int NS_E_DRM_CERTIFICATE_REVOKED = unchecked((int)0xC00D28A1);

		[Description("There is no license available for the requested action.")]
		public const int NS_E_DRM_LICENSE_UNAVAILABLE = unchecked((int)0xC00D28A2);

		[Description("The maximum number of devices in use has been reached. Unable to open additional devices.")]
		public const int NS_E_DRM_DEVICE_LIMIT_REACHED = unchecked((int)0xC00D28A3);

		[Description("The proximity detection procedure could not confirm that the receiver is near the transmitter in the network.")]
		public const int NS_E_DRM_UNABLE_TO_VERIFY_PROXIMITY = unchecked((int)0xC00D28A4);

		[Description("The client must be registered before executing the intended operation.")]
		public const int NS_E_DRM_MUST_REGISTER = unchecked((int)0xC00D28A5);

		[Description("The client must be approved before executing the intended operation.")]
		public const int NS_E_DRM_MUST_APPROVE = unchecked((int)0xC00D28A6);

		[Description("The client must be revalidated before executing the intended operation.")]
		public const int NS_E_DRM_MUST_REVALIDATE = unchecked((int)0xC00D28A7);

		[Description("The response to the proximity detection challenge is invalid.")]
		public const int NS_E_DRM_INVALID_PROXIMITY_RESPONSE = unchecked((int)0xC00D28A8);

		[Description("The requested session is invalid.")]
		public const int NS_E_DRM_INVALID_SESSION = unchecked((int)0xC00D28A9);

		[Description("The device must be opened before it can be used to receive content.")]
		public const int NS_E_DRM_DEVICE_NOT_OPEN = unchecked((int)0xC00D28AA);

		[Description("Device registration failed because the device is already registered.")]
		public const int NS_E_DRM_DEVICE_ALREADY_REGISTERED = unchecked((int)0xC00D28AB);

		[Description("Unsupported WMDRM-ND protocol version.")]
		public const int NS_E_DRM_UNSUPPORTED_PROTOCOL_VERSION = unchecked((int)0xC00D28AC);

		[Description("The requested action is not supported.")]
		public const int NS_E_DRM_UNSUPPORTED_ACTION = unchecked((int)0xC00D28AD);

		[Description("The certificate does not have an adequate security level for the requested action.")]
		public const int NS_E_DRM_CERTIFICATE_SECURITY_LEVEL_INADEQUATE = unchecked((int)0xC00D28AE);

		[Description("Unable to open the specified port for receiving Proximity messages.")]
		public const int NS_E_DRM_UNABLE_TO_OPEN_PORT = unchecked((int)0xC00D28AF);

		[Description("The message format is invalid.")]
		public const int NS_E_DRM_BAD_REQUEST = unchecked((int)0xC00D28B0);

		[Description("The Certificate Revocation List is invalid or corrupted.")]
		public const int NS_E_DRM_INVALID_CRL = unchecked((int)0xC00D28B1);

		[Description("The length of the attribute name or value is too long.")]
		public const int NS_E_DRM_ATTRIBUTE_TOO_LONG = unchecked((int)0xC00D28B2);

		[Description("The license blob passed in the cardea request is expired.")]
		public const int NS_E_DRM_EXPIRED_LICENSEBLOB = unchecked((int)0xC00D28B3);

		[Description("The license blob passed in the cardea request is invalid. Contact Microsoft product support.")]
		public const int NS_E_DRM_INVALID_LICENSEBLOB = unchecked((int)0xC00D28B4);

		[Description("The requested operation cannot be performed because the license does not contain an inclusion list.")]
		public const int NS_E_DRM_INCLUSION_LIST_REQUIRED = unchecked((int)0xC00D28B5);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_DRMV2CLT_REVOKED = unchecked((int)0xC00D28B6);

		[Description("A problem has occurred in the Digital Rights Management component. Contact Microsoft product support.")]
		public const int NS_E_DRM_RIV_TOO_SMALL = unchecked((int)0xC00D28B7);

		[Description("Windows Media Player does not support the level of output protection required by the content.")]
		public const int NS_E_OUTPUT_PROTECTION_LEVEL_UNSUPPORTED = unchecked((int)0xC00D2904);

		[Description("Windows Media Player does not support the level of protection required for compressed digital video.")]
		public const int NS_E_COMPRESSED_DIGITAL_VIDEO_PROTECTION_LEVEL_UNSUPPORTED = unchecked((int)0xC00D2905);

		[Description("Windows Media Player does not support the level of protection required for uncompressed digital video.")]
		public const int NS_E_UNCOMPRESSED_DIGITAL_VIDEO_PROTECTION_LEVEL_UNSUPPORTED = unchecked((int)0xC00D2906);

		[Description("Windows Media Player does not support the level of protection required for analog video.")]
		public const int NS_E_ANALOG_VIDEO_PROTECTION_LEVEL_UNSUPPORTED = unchecked((int)0xC00D2907);

		[Description("Windows Media Player does not support the level of protection required for compressed digital audio.")]
		public const int NS_E_COMPRESSED_DIGITAL_AUDIO_PROTECTION_LEVEL_UNSUPPORTED = unchecked((int)0xC00D2908);

		[Description("Windows Media Player does not support the level of protection required for uncompressed digital audio.")]
		public const int NS_E_UNCOMPRESSED_DIGITAL_AUDIO_PROTECTION_LEVEL_UNSUPPORTED = unchecked((int)0xC00D2909);

		[Description("Windows Media Player does not support the scheme of output protection required by the content.")]
		public const int NS_E_OUTPUT_PROTECTION_SCHEME_UNSUPPORTED = unchecked((int)0xC00D290A);

		[Description("Installation was not successful and some file cleanup is not complete. For best results, restart your computer.")]
		public const int NS_E_REBOOT_RECOMMENDED = unchecked((int)0xC00D2AFA);

		[Description("Installation was not successful. To continue, you must restart your computer.")]
		public const int NS_E_REBOOT_REQUIRED = unchecked((int)0xC00D2AFB);

		[Description("Installation was not successful.")]
		public const int NS_E_SETUP_INCOMPLETE = unchecked((int)0xC00D2AFC);

		[Description("Setup cannot migrate the Windows Media Digital Rights Management (DRM) components.")]
		public const int NS_E_SETUP_DRM_MIGRATION_FAILED = unchecked((int)0xC00D2AFD);

		[Description("Some skin or playlist components cannot be installed.")]
		public const int NS_E_SETUP_IGNORABLE_FAILURE = unchecked((int)0xC00D2AFE);

		[Description("Setup cannot migrate the Windows Media Digital Rights Management (DRM) components. In addition, some skin or playlist components cannot be installed.")]
		public const int NS_E_SETUP_DRM_MIGRATION_FAILED_AND_IGNORABLE_FAILURE = unchecked((int)0xC00D2AFF);

		[Description("Installation is blocked because your computer does not meet one or more of the setup requirements.")]
		public const int NS_E_SETUP_BLOCKED = unchecked((int)0xC00D2B00);

		[Description("The specified protocol is not supported.")]
		public const int NS_E_UNKNOWN_PROTOCOL = unchecked((int)0xC00D2EE0);

		[Description("The client is redirected to a proxy server.")]
		public const int NS_E_REDIRECT_TO_PROXY = unchecked((int)0xC00D2EE1);

		[Description("The server encountered an unexpected condition which prevented it from fulfilling the request.")]
		public const int NS_E_INTERNAL_SERVER_ERROR = unchecked((int)0xC00D2EE2);

		[Description("The request could not be understood by the server.")]
		public const int NS_E_BAD_REQUEST = unchecked((int)0xC00D2EE3);

		[Description("The proxy experienced an error while attempting to contact the media server.")]
		public const int NS_E_ERROR_FROM_PROXY = unchecked((int)0xC00D2EE4);

		[Description("The proxy did not receive a timely response while attempting to contact the media server.")]
		public const int NS_E_PROXY_TIMEOUT = unchecked((int)0xC00D2EE5);

		[Description("The server is currently unable to handle the request due to a temporary overloading or maintenance of the server.")]
		public const int NS_E_SERVER_UNAVAILABLE = unchecked((int)0xC00D2EE6);

		[Description("The server is refusing to fulfill the requested operation.")]
		public const int NS_E_REFUSED_BY_SERVER = unchecked((int)0xC00D2EE7);

		[Description("The server is not a compatible streaming media server.")]
		public const int NS_E_INCOMPATIBLE_SERVER = unchecked((int)0xC00D2EE8);

		[Description("The content cannot be streamed because the Multicast protocol has been disabled.")]
		public const int NS_E_MULTICAST_DISABLED = unchecked((int)0xC00D2EE9);

		[Description("The server redirected the player to an invalid location.")]
		public const int NS_E_INVALID_REDIRECT = unchecked((int)0xC00D2EEA);

		[Description("The content cannot be streamed because all protocols have been disabled.")]
		public const int NS_E_ALL_PROTOCOLS_DISABLED = unchecked((int)0xC00D2EEB);

		[Description("The MSBD protocol is no longer supported. Please use HTTP to connect to the Windows Media stream.")]
		public const int NS_E_MSBD_NO_LONGER_SUPPORTED = unchecked((int)0xC00D2EEC);

		[Description("The proxy server could not be located. Please check your proxy server configuration.")]
		public const int NS_E_PROXY_NOT_FOUND = unchecked((int)0xC00D2EED);

		[Description("Unable to establish a connection to the proxy server. Please check your proxy server configuration.")]
		public const int NS_E_CANNOT_CONNECT_TO_PROXY = unchecked((int)0xC00D2EEE);

		[Description("Unable to locate the media server. The operation timed out.")]
		public const int NS_E_SERVER_DNS_TIMEOUT = unchecked((int)0xC00D2EEF);

		[Description("Unable to locate the proxy server. The operation timed out.")]
		public const int NS_E_PROXY_DNS_TIMEOUT = unchecked((int)0xC00D2EF0);

		[Description("Media closed because Windows was shut down.")]
		public const int NS_E_CLOSED_ON_SUSPEND = unchecked((int)0xC00D2EF1);

		[Description("Unable to read the contents of a playlist file from a media server.")]
		public const int NS_E_CANNOT_READ_PLAYLIST_FROM_MEDIASERVER = unchecked((int)0xC00D2EF2);

		[Description("Session not found.")]
		public const int NS_E_SESSION_NOT_FOUND = unchecked((int)0xC00D2EF3);

		[Description("Content requires a streaming media client.")]
		public const int NS_E_REQUIRE_STREAMING_CLIENT = unchecked((int)0xC00D2EF4);

		[Description("A command applies to a previous playlist entry.")]
		public const int NS_E_PLAYLIST_ENTRY_HAS_CHANGED = unchecked((int)0xC00D2EF5);

		[Description("The proxy server is denying access. The username and/or password might be incorrect.")]
		public const int NS_E_PROXY_ACCESSDENIED = unchecked((int)0xC00D2EF6);

		[Description("The proxy could not provide valid authentication credentials to the media server.")]
		public const int NS_E_PROXY_SOURCE_ACCESSDENIED = unchecked((int)0xC00D2EF7);

		[Description("The network sink failed to write data to the network.")]
		public const int NS_E_NETWORK_SINK_WRITE = unchecked((int)0xC00D2EF8);

		[Description("Packets are not being received from the server. The packets might be blocked by a filtering device, such as a network firewall.")]
		public const int NS_E_FIREWALL = unchecked((int)0xC00D2EF9);

		[Description("The MMS protocol is not supported. Please use HTTP or RTSP to connect to the Windows Media stream.")]
		public const int NS_E_MMS_NOT_SUPPORTED = unchecked((int)0xC00D2EFA);

		[Description("The Windows Media server is denying access. The username and/or password might be incorrect.")]
		public const int NS_E_SERVER_ACCESSDENIED = unchecked((int)0xC00D2EFB);

		[Description("The Publishing Point or file on the Windows Media Server is no longer available.")]
		public const int NS_E_RESOURCE_GONE = unchecked((int)0xC00D2EFC);

		[Description("There is no existing packetizer plugin for a stream.")]
		public const int NS_E_NO_EXISTING_PACKETIZER = unchecked((int)0xC00D2EFD);

		[Description("The response from the media server could not be understood. This might be caused by an incompatible proxy server or media server.")]
		public const int NS_E_BAD_SYNTAX_IN_SERVER_RESPONSE = unchecked((int)0xC00D2EFE);

		[Description("The Windows Media Server reset the network connection.")]
		public const int NS_E_RESET_SOCKET_CONNECTION = unchecked((int)0xC00D2F00);

		[Description("The request could not reach the media server (too many hops).")]
		public const int NS_E_TOO_MANY_HOPS = unchecked((int)0xC00D2F02);

		[Description("The server is sending too much data. The connection has been terminated.")]
		public const int NS_E_TOO_MUCH_DATA_FROM_SERVER = unchecked((int)0xC00D2F05);

		[Description("It was not possible to establish a connection to the media server in a timely manner. The media server might be down for maintenance, or it might be necessary to use a proxy server to access this media server.")]
		public const int NS_E_CONNECT_TIMEOUT = unchecked((int)0xC00D2F06);

		[Description("It was not possible to establish a connection to the proxy server in a timely manner. Please check your proxy server configuration.")]
		public const int NS_E_PROXY_CONNECT_TIMEOUT = unchecked((int)0xC00D2F07);

		[Description("Session not found.")]
		public const int NS_E_SESSION_INVALID = unchecked((int)0xC00D2F08);

		[Description("Unknown packet sink stream.")]
		public const int NS_E_PACKETSINK_UNKNOWN_FEC_STREAM = unchecked((int)0xC00D2F0A);

		[Description("Unable to establish a connection to the server. Ensure Windows Media Services is started and the HTTP Server control protocol is properly enabled.")]
		public const int NS_E_PUSH_CANNOTCONNECT = unchecked((int)0xC00D2F0B);

		[Description("The Server service that received the HTTP push request is not a compatible version of Windows Media Services (WMS). This error might indicate the push request was received by IIS instead of WMS. Ensure WMS is started and has the HTTP Server control protocol properly enabled and try again.")]
		public const int NS_E_INCOMPATIBLE_PUSH_SERVER = unchecked((int)0xC00D2F0C);

		[Description("The playlist has reached its end.")]
		public const int NS_E_END_OF_PLAYLIST = unchecked((int)0xC00D32C8);

		[Description("Use file source.")]
		public const int NS_E_USE_FILE_SOURCE = unchecked((int)0xC00D32C9);

		[Description("The property was not found.")]
		public const int NS_E_PROPERTY_NOT_FOUND = unchecked((int)0xC00D32CA);

		[Description("The property is read only.")]
		public const int NS_E_PROPERTY_READ_ONLY = unchecked((int)0xC00D32CC);

		[Description("The table key was not found.")]
		public const int NS_E_TABLE_KEY_NOT_FOUND = unchecked((int)0xC00D32CD);

		[Description("Invalid query operator.")]
		public const int NS_E_INVALID_QUERY_OPERATOR = unchecked((int)0xC00D32CF);

		[Description("Invalid query property.")]
		public const int NS_E_INVALID_QUERY_PROPERTY = unchecked((int)0xC00D32D0);

		[Description("The property is not supported.")]
		public const int NS_E_PROPERTY_NOT_SUPPORTED = unchecked((int)0xC00D32D2);

		[Description("Schema classification failure.")]
		public const int NS_E_SCHEMA_CLASSIFY_FAILURE = unchecked((int)0xC00D32D4);

		[Description("The metadata format is not supported.")]
		public const int NS_E_METADATA_FORMAT_NOT_SUPPORTED = unchecked((int)0xC00D32D5);

		[Description("Cannot edit the metadata.")]
		public const int NS_E_METADATA_NO_EDITING_CAPABILITY = unchecked((int)0xC00D32D6);

		[Description("Cannot set the locale id.")]
		public const int NS_E_METADATA_CANNOT_SET_LOCALE = unchecked((int)0xC00D32D7);

		[Description("The language is not supported in the format.")]
		public const int NS_E_METADATA_LANGUAGE_NOT_SUPORTED = unchecked((int)0xC00D32D8);

		[Description("There is no RFC1766 name translation for the supplied locale id.")]
		public const int NS_E_METADATA_NO_RFC1766_NAME_FOR_LOCALE = unchecked((int)0xC00D32D9);

		[Description("The metadata (or metadata item) is not available.")]
		public const int NS_E_METADATA_NOT_AVAILABLE = unchecked((int)0xC00D32DA);

		[Description("The cached metadata (or metadata item) is not available.")]
		public const int NS_E_METADATA_CACHE_DATA_NOT_AVAILABLE = unchecked((int)0xC00D32DB);

		[Description("The metadata document is invalid.")]
		public const int NS_E_METADATA_INVALID_DOCUMENT_TYPE = unchecked((int)0xC00D32DC);

		[Description("The metadata content identifier is not available.")]
		public const int NS_E_METADATA_IDENTIFIER_NOT_AVAILABLE = unchecked((int)0xC00D32DD);

		[Description("Cannot retrieve metadata from the offline metadata cache.")]
		public const int NS_E_METADATA_CANNOT_RETRIEVE_FROM_OFFLINE_CACHE = unchecked((int)0xC00D32DE);

		[Description("Checksum of the obtained monitor descriptor is invalid.")]
		public const int ERROR_MONITOR_INVALID_DESCRIPTOR_CHECKSUM = unchecked((int)0xC0261003);

		[Description("Monitor descriptor contains an invalid standard timing block.")]
		public const int ERROR_MONITOR_INVALID_STANDARD_TIMING_BLOCK = unchecked((int)0xC0261004);

		[Description("Windows Management Instrumentation (WMI) data block registration failed for one of the MSMonitorClass WMI subclasses.")]
		public const int ERROR_MONITOR_WMI_DATABLOCK_REGISTRATION_FAILED = unchecked((int)0xC0261005);

		[Description("Provided monitor descriptor block is either corrupted or does not contain the monitor's detailed serial number.")]
		public const int ERROR_MONITOR_INVALID_SERIAL_NUMBER_MONDSC_BLOCK = unchecked((int)0xC0261006);

		[Description("Provided monitor descriptor block is either corrupted or does not contain the monitor's user-friendly name.")]
		public const int ERROR_MONITOR_INVALID_USER_FRIENDLY_MONDSC_BLOCK = unchecked((int)0xC0261007);

		[Description("There is no monitor descriptor data at the specified (offset, size) region.")]
		public const int ERROR_MONITOR_NO_MORE_DESCRIPTOR_DATA = unchecked((int)0xC0261008);

		[Description("Monitor descriptor contains an invalid detailed timing block.")]
		public const int ERROR_MONITOR_INVALID_DETAILED_TIMING_BLOCK = unchecked((int)0xC0261009);

		[Description("Exclusive mode ownership is needed to create unmanaged primary allocation.")]
		public const int ERROR_GRAPHICS_NOT_EXCLUSIVE_MODE_OWNER = unchecked((int)0xC0262000);

		[Description("The driver needs more direct memory access (DMA) buffer space to complete the requested operation.")]
		public const int ERROR_GRAPHICS_INSUFFICIENT_DMA_BUFFER = unchecked((int)0xC0262001);

		[Description("Specified display adapter handle is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_DISPLAY_ADAPTER = unchecked((int)0xC0262002);

		[Description("Specified display adapter and all of its state has been reset.")]
		public const int ERROR_GRAPHICS_ADAPTER_WAS_RESET = unchecked((int)0xC0262003);

		[Description("The driver stack does not match the expected driver model.")]
		public const int ERROR_GRAPHICS_INVALID_DRIVER_MODEL = unchecked((int)0xC0262004);

		[Description("Present happened but ended up into the changed desktop mode.")]
		public const int ERROR_GRAPHICS_PRESENT_MODE_CHANGED = unchecked((int)0xC0262005);

		[Description("Nothing to present due to desktop occlusion.")]
		public const int ERROR_GRAPHICS_PRESENT_OCCLUDED = unchecked((int)0xC0262006);

		[Description("Not able to present due to denial of desktop access.")]
		public const int ERROR_GRAPHICS_PRESENT_DENIED = unchecked((int)0xC0262007);

		[Description("Not able to present with color conversion.")]
		public const int ERROR_GRAPHICS_CANNOTCOLORCONVERT = unchecked((int)0xC0262008);

		[Description("Not enough video memory available to complete the operation.")]
		public const int ERROR_GRAPHICS_NO_VIDEO_MEMORY = unchecked((int)0xC0262100);

		[Description("Could not probe and lock the underlying memory of an allocation.")]
		public const int ERROR_GRAPHICS_CANT_LOCK_MEMORY = unchecked((int)0xC0262101);

		[Description("The allocation is currently busy.")]
		public const int ERROR_GRAPHICS_ALLOCATION_BUSY = unchecked((int)0xC0262102);

		[Description("An object being referenced has reach the maximum reference count already and cannot be referenced further.")]
		public const int ERROR_GRAPHICS_TOO_MANY_REFERENCES = unchecked((int)0xC0262103);

		[Description("A problem could not be solved due to some currently existing condition. The problem should be tried again later.")]
		public const int ERROR_GRAPHICS_TRY_AGAIN_LATER = unchecked((int)0xC0262104);

		[Description("A problem could not be solved due to some currently existing condition. The problem should be tried again immediately.")]
		public const int ERROR_GRAPHICS_TRY_AGAIN_NOW = unchecked((int)0xC0262105);

		[Description("The allocation is invalid.")]
		public const int ERROR_GRAPHICS_ALLOCATION_INVALID = unchecked((int)0xC0262106);

		[Description("No more unswizzling apertures are currently available.")]
		public const int ERROR_GRAPHICS_UNSWIZZLING_APERTURE_UNAVAILABLE = unchecked((int)0xC0262107);

		[Description("The current allocation cannot be unswizzled by an aperture.")]
		public const int ERROR_GRAPHICS_UNSWIZZLING_APERTURE_UNSUPPORTED = unchecked((int)0xC0262108);

		[Description("The request failed because a pinned allocation cannot be evicted.")]
		public const int ERROR_GRAPHICS_CANT_EVICT_PINNED_ALLOCATION = unchecked((int)0xC0262109);

		[Description("The allocation cannot be used from its current segment location for the specified operation.")]
		public const int ERROR_GRAPHICS_INVALID_ALLOCATION_USAGE = unchecked((int)0xC0262110);

		[Description("A locked allocation cannot be used in the current command buffer.")]
		public const int ERROR_GRAPHICS_CANT_RENDER_LOCKED_ALLOCATION = unchecked((int)0xC0262111);

		[Description("The allocation being referenced has been closed permanently.")]
		public const int ERROR_GRAPHICS_ALLOCATION_CLOSED = unchecked((int)0xC0262112);

		[Description("An invalid allocation instance is being referenced.")]
		public const int ERROR_GRAPHICS_INVALID_ALLOCATION_INSTANCE = unchecked((int)0xC0262113);

		[Description("An invalid allocation handle is being referenced.")]
		public const int ERROR_GRAPHICS_INVALID_ALLOCATION_HANDLE = unchecked((int)0xC0262114);

		[Description("The allocation being referenced does not belong to the current device.")]
		public const int ERROR_GRAPHICS_WRONG_ALLOCATION_DEVICE = unchecked((int)0xC0262115);

		[Description("The specified allocation lost its content.")]
		public const int ERROR_GRAPHICS_ALLOCATION_CONTENT_LOST = unchecked((int)0xC0262116);

		[Description("Graphics processing unit (GPU) exception is detected on the given device. The device is not able to be scheduled.")]
		public const int ERROR_GRAPHICS_GPU_EXCEPTION_ON_DEVICE = unchecked((int)0xC0262200);

		[Description("Specified video present network (VidPN) topology is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN_TOPOLOGY = unchecked((int)0xC0262300);

		[Description("Specified VidPN topology is valid but is not supported by this model of the display adapter.")]
		public const int ERROR_GRAPHICS_VIDPN_TOPOLOGY_NOT_SUPPORTED = unchecked((int)0xC0262301);

		[Description("Specified VidPN topology is valid but is not supported by the display adapter at this time, due to current allocation of its resources.")]
		public const int ERROR_GRAPHICS_VIDPN_TOPOLOGY_CURRENTLY_NOT_SUPPORTED = unchecked((int)0xC0262302);

		[Description("Specified VidPN handle is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN = unchecked((int)0xC0262303);

		[Description("Specified video present source is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE = unchecked((int)0xC0262304);

		[Description("Specified video present target is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET = unchecked((int)0xC0262305);

		[Description("Specified VidPN modality is not supported (for example, at least two of the pinned modes are not cofunctional).")]
		public const int ERROR_GRAPHICS_VIDPN_MODALITY_NOT_SUPPORTED = unchecked((int)0xC0262306);

		[Description("Specified VidPN source mode set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN_SOURCEMODESET = unchecked((int)0xC0262308);

		[Description("Specified VidPN target mode set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN_TARGETMODESET = unchecked((int)0xC0262309);

		[Description("Specified video signal frequency is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_FREQUENCY = unchecked((int)0xC026230A);

		[Description("Specified video signal active region is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_ACTIVE_REGION = unchecked((int)0xC026230B);

		[Description("Specified video signal total region is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_TOTAL_REGION = unchecked((int)0xC026230C);

		[Description("Specified video present source mode is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE_MODE = unchecked((int)0xC0262310);

		[Description("Specified video present target mode is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET_MODE = unchecked((int)0xC0262311);

		[Description("Pinned mode must remain in the set on VidPN's cofunctional modality enumeration.")]
		public const int ERROR_GRAPHICS_PINNED_MODE_MUST_REMAIN_IN_SET = unchecked((int)0xC0262312);

		[Description("Specified video present path is already in the VidPN topology.")]
		public const int ERROR_GRAPHICS_PATH_ALREADY_IN_TOPOLOGY = unchecked((int)0xC0262313);

		[Description("Specified mode is already in the mode set.")]
		public const int ERROR_GRAPHICS_MODE_ALREADY_IN_MODESET = unchecked((int)0xC0262314);

		[Description("Specified video present source set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDEOPRESENTSOURCESET = unchecked((int)0xC0262315);

		[Description("Specified video present target set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDEOPRESENTTARGETSET = unchecked((int)0xC0262316);

		[Description("Specified video present source is already in the video present source set.")]
		public const int ERROR_GRAPHICS_SOURCE_ALREADY_IN_SET = unchecked((int)0xC0262317);

		[Description("Specified video present target is already in the video present target set.")]
		public const int ERROR_GRAPHICS_TARGET_ALREADY_IN_SET = unchecked((int)0xC0262318);

		[Description("Specified VidPN present path is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN_PRESENT_PATH = unchecked((int)0xC0262319);

		[Description("Miniport has no recommendation for augmentation of the specified VidPN topology.")]
		public const int ERROR_GRAPHICS_NO_RECOMMENDED_VIDPN_TOPOLOGY = unchecked((int)0xC026231A);

		[Description("Specified monitor frequency range set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGESET = unchecked((int)0xC026231B);

		[Description("Specified monitor frequency range is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE = unchecked((int)0xC026231C);

		[Description("Specified frequency range is not in the specified monitor frequency range set.")]
		public const int ERROR_GRAPHICS_FREQUENCYRANGE_NOT_IN_SET = unchecked((int)0xC026231D);

		[Description("Specified frequency range is already in the specified monitor frequency range set.")]
		public const int ERROR_GRAPHICS_FREQUENCYRANGE_ALREADY_IN_SET = unchecked((int)0xC026231F);

		[Description("Specified mode set is stale. Reacquire the new mode set.")]
		public const int ERROR_GRAPHICS_STALE_MODESET = unchecked((int)0xC0262320);

		[Description("Specified monitor source mode set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MONITOR_SOURCEMODESET = unchecked((int)0xC0262321);

		[Description("Specified monitor source mode is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MONITOR_SOURCE_MODE = unchecked((int)0xC0262322);

		[Description("Miniport does not have any recommendation regarding the request to provide a functional VidPN given the current display adapter configuration.")]
		public const int ERROR_GRAPHICS_NO_RECOMMENDED_FUNCTIONAL_VIDPN = unchecked((int)0xC0262323);

		[Description("ID of the specified mode is already used by another mode in the set.")]
		public const int ERROR_GRAPHICS_MODE_ID_MUST_BE_UNIQUE = unchecked((int)0xC0262324);

		[Description("System failed to determine a mode that is supported by both the display adapter and the monitor connected to it.")]
		public const int ERROR_GRAPHICS_EMPTY_ADAPTER_MONITOR_MODE_SUPPORT_INTERSECTION = unchecked((int)0xC0262325);

		[Description("Number of video present targets must be greater than or equal to the number of video present sources.")]
		public const int ERROR_GRAPHICS_VIDEO_PRESENT_TARGETS_LESS_THAN_SOURCES = unchecked((int)0xC0262326);

		[Description("Specified present path is not in the VidPN topology.")]
		public const int ERROR_GRAPHICS_PATH_NOT_IN_TOPOLOGY = unchecked((int)0xC0262327);

		[Description("Display adapter must have at least one video present source.")]
		public const int ERROR_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_SOURCE = unchecked((int)0xC0262328);

		[Description("Display adapter must have at least one video present target.")]
		public const int ERROR_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_TARGET = unchecked((int)0xC0262329);

		[Description("Specified monitor descriptor set is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MONITORDESCRIPTORSET = unchecked((int)0xC026232A);

		[Description("Specified monitor descriptor is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MONITORDESCRIPTOR = unchecked((int)0xC026232B);

		[Description("Specified descriptor is not in the specified monitor descriptor set.")]
		public const int ERROR_GRAPHICS_MONITORDESCRIPTOR_NOT_IN_SET = unchecked((int)0xC026232C);

		[Description("Specified descriptor is already in the specified monitor descriptor set.")]
		public const int ERROR_GRAPHICS_MONITORDESCRIPTOR_ALREADY_IN_SET = unchecked((int)0xC026232D);

		[Description("ID of the specified monitor descriptor is already used by another descriptor in the set.")]
		public const int ERROR_GRAPHICS_MONITORDESCRIPTOR_ID_MUST_BE_UNIQUE = unchecked((int)0xC026232E);

		[Description("Specified video present target subset type is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN_TARGET_SUBSET_TYPE = unchecked((int)0xC026232F);

		[Description("Two or more of the specified resources are not related to each other, as defined by the interface semantics.")]
		public const int ERROR_GRAPHICS_RESOURCES_NOT_RELATED = unchecked((int)0xC0262330);

		[Description("ID of the specified video present source is already used by another source in the set.")]
		public const int ERROR_GRAPHICS_SOURCE_ID_MUST_BE_UNIQUE = unchecked((int)0xC0262331);

		[Description("ID of the specified video present target is already used by another target in the set.")]
		public const int ERROR_GRAPHICS_TARGET_ID_MUST_BE_UNIQUE = unchecked((int)0xC0262332);

		[Description("Specified VidPN source cannot be used because there is no available VidPN target to connect it to.")]
		public const int ERROR_GRAPHICS_NO_AVAILABLE_VIDPN_TARGET = unchecked((int)0xC0262333);

		[Description("Newly arrived monitor could not be associated with a display adapter.")]
		public const int ERROR_GRAPHICS_MONITOR_COULD_NOT_BE_ASSOCIATED_WITH_ADAPTER = unchecked((int)0xC0262334);

		[Description("Display adapter in question does not have an associated VidPN manager.")]
		public const int ERROR_GRAPHICS_NO_VIDPNMGR = unchecked((int)0xC0262335);

		[Description("VidPN manager of the display adapter in question does not have an active VidPN.")]
		public const int ERROR_GRAPHICS_NO_ACTIVE_VIDPN = unchecked((int)0xC0262336);

		[Description("Specified VidPN topology is stale. Re-acquire the new topology.")]
		public const int ERROR_GRAPHICS_STALE_VIDPN_TOPOLOGY = unchecked((int)0xC0262337);

		[Description("There is no monitor connected on the specified video present target.")]
		public const int ERROR_GRAPHICS_MONITOR_NOT_CONNECTED = unchecked((int)0xC0262338);

		[Description("Specified source is not part of the specified VidPN topology.")]
		public const int ERROR_GRAPHICS_SOURCE_NOT_IN_TOPOLOGY = unchecked((int)0xC0262339);

		[Description("Specified primary surface size is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_PRIMARYSURFACE_SIZE = unchecked((int)0xC026233A);

		[Description("Specified visible region size is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VISIBLEREGION_SIZE = unchecked((int)0xC026233B);

		[Description("Specified stride is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_STRIDE = unchecked((int)0xC026233C);

		[Description("Specified pixel format is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_PIXELFORMAT = unchecked((int)0xC026233D);

		[Description("Specified color basis is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_COLORBASIS = unchecked((int)0xC026233E);

		[Description("Specified pixel value access mode is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_PIXELVALUEACCESSMODE = unchecked((int)0xC026233F);

		[Description("Specified target is not part of the specified VidPN topology.")]
		public const int ERROR_GRAPHICS_TARGET_NOT_IN_TOPOLOGY = unchecked((int)0xC0262340);

		[Description("Failed to acquire display mode management interface.")]
		public const int ERROR_GRAPHICS_NO_DISPLAY_MODE_MANAGEMENT_SUPPORT = unchecked((int)0xC0262341);

		[Description("Specified VidPN source is already owned by a display mode manager (DMM) client and cannot be used until that client releases it.")]
		public const int ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE = unchecked((int)0xC0262342);

		[Description("Specified VidPN is active and cannot be accessed.")]
		public const int ERROR_GRAPHICS_CANT_ACCESS_ACTIVE_VIDPN = unchecked((int)0xC0262343);

		[Description("Specified VidPN present path importance ordinal is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_PATH_IMPORTANCE_ORDINAL = unchecked((int)0xC0262344);

		[Description("Specified VidPN present path content geometry transformation is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_PATH_CONTENT_GEOMETRY_TRANSFORMATION = unchecked((int)0xC0262345);

		[Description("Specified content geometry transformation is not supported on the respective VidPN present path.")]
		public const int ERROR_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_SUPPORTED = unchecked((int)0xC0262346);

		[Description("Specified gamma ramp is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_GAMMA_RAMP = unchecked((int)0xC0262347);

		[Description("Specified gamma ramp is not supported on the respective VidPN present path.")]
		public const int ERROR_GRAPHICS_GAMMA_RAMP_NOT_SUPPORTED = unchecked((int)0xC0262348);

		[Description("Multisampling is not supported on the respective VidPN present path.")]
		public const int ERROR_GRAPHICS_MULTISAMPLING_NOT_SUPPORTED = unchecked((int)0xC0262349);

		[Description("Specified mode is not in the specified mode set.")]
		public const int ERROR_GRAPHICS_MODE_NOT_IN_MODESET = unchecked((int)0xC026234A);

		[Description("Specified VidPN topology recommendation reason is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_VIDPN_TOPOLOGY_RECOMMENDATION_REASON = unchecked((int)0xC026234D);

		[Description("Specified VidPN present path content type is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_PATH_CONTENT_TYPE = unchecked((int)0xC026234E);

		[Description("Specified VidPN present path copy protection type is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_COPYPROTECTION_TYPE = unchecked((int)0xC026234F);

		[Description("No more than one unassigned mode set can exist at any given time for a given VidPN source or target.")]
		public const int ERROR_GRAPHICS_UNASSIGNED_MODESET_ALREADY_EXISTS = unchecked((int)0xC0262350);

		[Description("The specified scan line ordering type is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_SCANLINE_ORDERING = unchecked((int)0xC0262352);

		[Description("Topology changes are not allowed for the specified VidPN.")]
		public const int ERROR_GRAPHICS_TOPOLOGY_CHANGES_NOT_ALLOWED = unchecked((int)0xC0262353);

		[Description("All available importance ordinals are already used in the specified topology.")]
		public const int ERROR_GRAPHICS_NO_AVAILABLE_IMPORTANCE_ORDINALS = unchecked((int)0xC0262354);

		[Description("Specified primary surface has a different private format attribute than the current primary surface.")]
		public const int ERROR_GRAPHICS_INCOMPATIBLE_PRIVATE_FORMAT = unchecked((int)0xC0262355);

		[Description("Specified mode pruning algorithm is invalid.")]
		public const int ERROR_GRAPHICS_INVALID_MODE_PRUNING_ALGORITHM = unchecked((int)0xC0262356);

		[Description("Specified display adapter child device already has an external device connected to it.")]
		public const int ERROR_GRAPHICS_SPECIFIED_CHILD_ALREADY_CONNECTED = unchecked((int)0xC0262400);

		[Description("The display adapter child device does not support reporting a descriptor.")]
		public const int ERROR_GRAPHICS_CHILD_DESCRIPTOR_NOT_SUPPORTED = unchecked((int)0xC0262401);

		[Description("The display adapter is not linked to any other adapters.")]
		public const int ERROR_GRAPHICS_NOT_A_LINKED_ADAPTER = unchecked((int)0xC0262430);

		[Description("Lead adapter in a linked configuration was not enumerated yet.")]
		public const int ERROR_GRAPHICS_LEADLINK_NOT_ENUMERATED = unchecked((int)0xC0262431);

		[Description("Some chain adapters in a linked configuration were not enumerated yet.")]
		public const int ERROR_GRAPHICS_CHAINLINKS_NOT_ENUMERATED = unchecked((int)0xC0262432);

		[Description("The chain of linked adapters is not ready to start because of an unknown failure.")]
		public const int ERROR_GRAPHICS_ADAPTER_CHAIN_NOT_READY = unchecked((int)0xC0262433);

		[Description("An attempt was made to start a lead link display adapter when the chain links were not started yet.")]
		public const int ERROR_GRAPHICS_CHAINLINKS_NOT_STARTED = unchecked((int)0xC0262434);

		[Description("An attempt was made to turn on a lead link display adapter when the chain links were turned off.")]
		public const int ERROR_GRAPHICS_CHAINLINKS_NOT_POWERED_ON = unchecked((int)0xC0262435);

		[Description("The adapter link was found to be in an inconsistent state. Not all adapters are in an expected PNP or power state.")]
		public const int ERROR_GRAPHICS_INCONSISTENT_DEVICE_LINK_STATE = unchecked((int)0xC0262436);

		[Description("The driver trying to start is not the same as the driver for the posted display adapter.")]
		public const int ERROR_GRAPHICS_NOT_POST_DEVICE_DRIVER = unchecked((int)0xC0262438);

		[Description("The driver does not support Output Protection Manager (OPM).")]
		public const int ERROR_GRAPHICS_OPM_NOT_SUPPORTED = unchecked((int)0xC0262500);

		[Description("The driver does not support Certified Output Protection Protocol (COPP).")]
		public const int ERROR_GRAPHICS_COPP_NOT_SUPPORTED = unchecked((int)0xC0262501);

		[Description("The driver does not support a user-accessible bus (UAB).")]
		public const int ERROR_GRAPHICS_UAB_NOT_SUPPORTED = unchecked((int)0xC0262502);

		[Description("The specified encrypted parameters are invalid.")]
		public const int ERROR_GRAPHICS_OPM_INVALID_ENCRYPTED_PARAMETERS = unchecked((int)0xC0262503);

		[Description("An array passed to a function cannot hold all of the data that the function wants to put in it.")]
		public const int ERROR_GRAPHICS_OPM_PARAMETER_ARRAY_TOO_SMALL = unchecked((int)0xC0262504);

		[Description("The GDI display device passed to this function does not have any active video outputs.")]
		public const int ERROR_GRAPHICS_OPM_NO_VIDEO_OUTPUTS_EXIST = unchecked((int)0xC0262505);

		[Description("The protected video path (PVP) cannot find an actual GDI display device that corresponds to the passed-in GDI display device name.")]
		public const int ERROR_GRAPHICS_PVP_NO_DISPLAY_DEVICE_CORRESPONDS_TO_NAME = unchecked((int)0xC0262506);

		[Description("This function failed because the GDI display device passed to it was not attached to the Windows desktop.")]
		public const int ERROR_GRAPHICS_PVP_DISPLAY_DEVICE_NOT_ATTACHED_TO_DESKTOP = unchecked((int)0xC0262507);

		[Description("The PVP does not support mirroring display devices because they do not have video outputs.")]
		public const int ERROR_GRAPHICS_PVP_MIRRORING_DEVICES_NOT_SUPPORTED = unchecked((int)0xC0262508);

		[Description("The function failed because an invalid pointer parameter was passed to it. A pointer parameter is invalid if it is null, it points to an invalid address, it points to a kernel mode address, or it is not correctly aligned.")]
		public const int ERROR_GRAPHICS_OPM_INVALID_POINTER = unchecked((int)0xC026250A);

		[Description("An internal error caused this operation to fail.")]
		public const int ERROR_GRAPHICS_OPM_INTERNAL_ERROR = unchecked((int)0xC026250B);

		[Description("The function failed because the caller passed in an invalid OPM user mode handle.")]
		public const int ERROR_GRAPHICS_OPM_INVALID_HANDLE = unchecked((int)0xC026250C);

		[Description("This function failed because the GDI device passed to it did not have any monitors associated with it.")]
		public const int ERROR_GRAPHICS_PVP_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE = unchecked((int)0xC026250D);

		[Description("A certificate could not be returned because the certificate buffer passed to the function was too small.")]
		public const int ERROR_GRAPHICS_PVP_INVALID_CERTIFICATE_LENGTH = unchecked((int)0xC026250E);

		[Description("A video output could not be created because the frame buffer is in spanning mode.")]
		public const int ERROR_GRAPHICS_OPM_SPANNING_MODE_ENABLED = unchecked((int)0xC026250F);

		[Description("A video output could not be created because the frame buffer is in theater mode.")]
		public const int ERROR_GRAPHICS_OPM_THEATER_MODE_ENABLED = unchecked((int)0xC0262510);

		[Description("The function call failed because the display adapter's hardware functionality scan failed to validate the graphics hardware.")]
		public const int ERROR_GRAPHICS_PVP_HFS_FAILED = unchecked((int)0xC0262511);

		[Description("The High-Bandwidth Digital Content Protection (HDCP) System Renewability Message (SRM) passed to this function did not comply with section 5 of the HDCP 1.1 specification.")]
		public const int ERROR_GRAPHICS_OPM_INVALID_SRM = unchecked((int)0xC0262512);

		[Description("The video output cannot enable the HDCP system because it does not support it.")]
		public const int ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_HDCP = unchecked((int)0xC0262513);

		[Description("The video output cannot enable analog copy protection because it does not support it.")]
		public const int ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_ACP = unchecked((int)0xC0262514);

		[Description("The video output cannot enable the Content Generation Management System Analog (CGMS-A) protection technology because it does not support it.")]
		public const int ERROR_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_CGMSA = unchecked((int)0xC0262515);

		[Description("IOPMVideoOutput's GetInformation() method cannot return the version of the SRM being used because the application never successfully passed an SRM to the video output.")]
		public const int ERROR_GRAPHICS_OPM_HDCP_SRM_NEVER_SET = unchecked((int)0xC0262516);

		[Description("IOPMVideoOutput's Configure() method cannot enable the specified output protection technology because the output's screen resolution is too high.")]
		public const int ERROR_GRAPHICS_OPM_RESOLUTION_TOO_HIGH = unchecked((int)0xC0262517);

		[Description("IOPMVideoOutput's Configure() method cannot enable HDCP because the display adapter's HDCP hardware is already being used by other physical outputs.")]
		public const int ERROR_GRAPHICS_OPM_ALL_HDCP_HARDWARE_ALREADY_IN_USE = unchecked((int)0xC0262518);

		[Description("The operating system asynchronously destroyed this OPM video output because the operating system's state changed. This error typically occurs because the monitor physical device object (PDO) associated with this video output was removed, the monitor PDO associated with this video output was stopped, the video output's session became a nonconsole session or the video output's desktop became an inactive desktop.")]
		public const int ERROR_GRAPHICS_OPM_VIDEO_OUTPUT_NO_LONGER_EXISTS = unchecked((int)0xC0262519);

		[Description("IOPMVideoOutput's methods cannot be called when a session is changing its type. There are currently three types of sessions: console, disconnected and remote (remote desktop protocol [RDP] or Independent Computing Architecture [ICA]).")]
		public const int ERROR_GRAPHICS_OPM_SESSION_TYPE_CHANGE_IN_PROGRESS = unchecked((int)0xC026251A);

		[Description("The monitor connected to the specified video output does not have an I2C bus.")]
		public const int ERROR_GRAPHICS_I2C_NOT_SUPPORTED = unchecked((int)0xC0262580);

		[Description("No device on the I2C bus has the specified address.")]
		public const int ERROR_GRAPHICS_I2C_DEVICE_DOES_NOT_EXIST = unchecked((int)0xC0262581);

		[Description("An error occurred while transmitting data to the device on the I2C bus.")]
		public const int ERROR_GRAPHICS_I2C_ERROR_TRANSMITTING_DATA = unchecked((int)0xC0262582);

		[Description("An error occurred while receiving data from the device on the I2C bus.")]
		public const int ERROR_GRAPHICS_I2C_ERROR_RECEIVING_DATA = unchecked((int)0xC0262583);

		[Description("The monitor does not support the specified Virtual Control Panel (VCP) code.")]
		public const int ERROR_GRAPHICS_DDCCI_VCP_NOT_SUPPORTED = unchecked((int)0xC0262584);

		[Description("The data received from the monitor is invalid.")]
		public const int ERROR_GRAPHICS_DDCCI_INVALID_DATA = unchecked((int)0xC0262585);

		[Description("A function call failed because a monitor returned an invalid Timing Status byte when the operating system used the Display Data Channel Command Interface (DDC/CI) Get Timing Report and Timing Message command to get a timing report from a monitor.")]
		public const int ERROR_GRAPHICS_DDCCI_MONITOR_RETURNED_INVALID_TIMING_STATUS_BYTE = unchecked((int)0xC0262586);

		[Description("The monitor returned a DDC/CI capabilities string that did not comply with the ACCESS.bus 3.0, DDC/CI 1.1 or MCCS 2 Revision 1 specification.")]
		public const int ERROR_GRAPHICS_MCA_INVALID_CAPABILITIES_STRING = unchecked((int)0xC0262587);

		[Description("An internal Monitor Configuration API error occurred.")]
		public const int ERROR_GRAPHICS_MCA_INTERNAL_ERROR = unchecked((int)0xC0262588);

		[Description("An operation failed because a DDC/CI message had an invalid value in its command field.")]
		public const int ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_COMMAND = unchecked((int)0xC0262589);

		[Description("This error occurred because a DDC/CI message length field contained an invalid value.")]
		public const int ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_LENGTH = unchecked((int)0xC026258A);

		[Description("This error occurred because the value in a DDC/CI message checksum field did not match the message's computed checksum value. This error implies that the data was corrupted while it was being transmitted from a monitor to a computer.")]
		public const int ERROR_GRAPHICS_DDCCI_INVALID_MESSAGE_CHECKSUM = unchecked((int)0xC026258B);

		[Description("The HMONITOR no longer exists, is not attached to the desktop, or corresponds to a mirroring device.")]
		public const int ERROR_GRAPHICS_PMEA_INVALID_MONITOR = unchecked((int)0xC02625D6);

		[Description("The Direct3D (D3D) device's GDI display device no longer exists, is not attached to the desktop, or is a mirroring display device.")]
		public const int ERROR_GRAPHICS_PMEA_INVALID_D3D_DEVICE = unchecked((int)0xC02625D7);

		[Description("A continuous VCP code's current value is greater than its maximum value. This error code indicates that a monitor returned an invalid value.")]
		public const int ERROR_GRAPHICS_DDCCI_CURRENT_CURRENT_VALUE_GREATER_THAN_MAXIMUM_VALUE = unchecked((int)0xC02625D8);

		[Description("The monitor's VCP Version (0xDF) VCP code returned an invalid version value.")]
		public const int ERROR_GRAPHICS_MCA_INVALID_VCP_VERSION = unchecked((int)0xC02625D9);

		[Description("The monitor does not comply with the Monitor Control Command Set (MCCS) specification it claims to support.")]
		public const int ERROR_GRAPHICS_MCA_MONITOR_VIOLATES_MCCS_SPECIFICATION = unchecked((int)0xC02625DA);

		[Description("The MCCS version in a monitor's mccs_ver capability does not match the MCCS version the monitor reports when the VCP Version (0xDF) VCP code is used.")]
		public const int ERROR_GRAPHICS_MCA_MCCS_VERSION_MISMATCH = unchecked((int)0xC02625DB);

		[Description("The Monitor Configuration API only works with monitors that support the MCCS 1.0 specification, the MCCS 2.0 specification, or the MCCS 2.0 Revision 1 specification.")]
		public const int ERROR_GRAPHICS_MCA_UNSUPPORTED_MCCS_VERSION = unchecked((int)0xC02625DC);

		[Description("The monitor returned an invalid monitor technology type. CRT, plasma, and LCD (TFT) are examples of monitor technology types. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.")]
		public const int ERROR_GRAPHICS_MCA_INVALID_TECHNOLOGY_TYPE_RETURNED = unchecked((int)0xC02625DE);

		[Description("The SetMonitorColorTemperature() caller passed a color temperature to it that the current monitor did not support. CRT, plasma, and LCD (TFT) are examples of monitor technology types. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.")]
		public const int ERROR_GRAPHICS_MCA_UNSUPPORTED_COLOR_TEMPERATURE = unchecked((int)0xC02625DF);

		[Description("This function can be used only if a program is running in the local console session. It cannot be used if the program is running on a remote desktop session or on a terminal server session.")]
		public const int ERROR_GRAPHICS_ONLY_CONSOLE_SESSION_SUPPORTED = unchecked((int)0xC02625E0);

		[Description("User responded \"Yes\" to the dialog.")]
		public const int COPYENGINE_S_YES = 0x00270001;
		[Description("Undocumented.")]
		public const int COPYENGINE_S_NOT_HANDLED = 0x00270003;
		[Description("User responded to retry the current action.")]
		public const int COPYENGINE_S_USER_RETRY = 0x00270004;
		[Description("User responded \"No\" to the dialog.")]
		public const int COPYENGINE_S_USER_IGNORED = 0x00270005;
		[Description("User responded to merge folders.")]
		public const int COPYENGINE_S_MERGE = 0x00270006;
		[Description("Child items should not be processed.")]
		public const int COPYENGINE_S_DONT_PROCESS_CHILDREN = 0x00270008;
		[Description("Undocumented.")]
		public const int COPYENGINE_S_ALREADY_DONE = 0x0027000A;
		[Description("Error has been queued and will display later.")]
		public const int COPYENGINE_S_PENDING = 0x0027000B;
		[Description("Undocumented.")]
		public const int COPYENGINE_S_KEEP_BOTH = 0x0027000C;
		[Description("Close the program using the current file")]
		public const int COPYENGINE_S_CLOSE_PROGRAM = 0x0027000D;
		[Description("User wants to canceled entire job")]
		public const int COPYENGINE_E_USER_CANCELLED = unchecked((int)0x80270000);
		[Description("Engine wants to canceled entire job, don't set the CANCELLED bit")]
		public const int COPYENGINE_E_CANCELLED = unchecked((int)0x80270001);
		[Description("Need to elevate the process to complete the operation")]
		public const int COPYENGINE_E_REQUIRES_ELEVATION = unchecked((int)0x80270002);
		[Description("Source and destination file are the same")]
		public const int COPYENGINE_E_SAME_FILE = unchecked((int)0x80270003);
		[Description("Trying to rename a file into a different location, use move instead")]
		public const int COPYENGINE_E_DIFF_DIR = unchecked((int)0x80270004);
		[Description("One source specified, multiple destinations")]
		public const int COPYENGINE_E_MANY_SRC_1_DEST = unchecked((int)0x80270005);
		[Description("The destination is a sub-tree of the source")]
		public const int COPYENGINE_E_DEST_SUBTREE = unchecked((int)0x80270009);
		[Description("The destination is the same folder as the source")]
		public const int COPYENGINE_E_DEST_SAME_TREE = unchecked((int)0x8027000A);
		[Description("Existing destination file with same name as folder")]
		public const int COPYENGINE_E_FLD_IS_FILE_DEST = unchecked((int)0x8027000B);
		[Description("Existing destination folder with same name as file")]
		public const int COPYENGINE_E_FILE_IS_FLD_DEST = unchecked((int)0x8027000C);
		[Description("File too large for destination file system")]
		public const int COPYENGINE_E_FILE_TOO_LARGE = unchecked((int)0x8027000D);
		[Description("Destination device is full and happens to be removable")]
		public const int COPYENGINE_E_REMOVABLE_FULL = unchecked((int)0x8027000E);
		[Description("Destination is a Read-Only CDRom, possibly unformatted")]
		public const int COPYENGINE_E_DEST_IS_RO_CD = unchecked((int)0x8027000F);
		[Description("Destination is a Read/Write CDRom, possibly unformatted")]
		public const int COPYENGINE_E_DEST_IS_RW_CD = unchecked((int)0x80270010);
		[Description("Destination is a Recordable (Audio, CDRom, possibly unformatted")]
		public const int COPYENGINE_E_DEST_IS_R_CD = unchecked((int)0x80270011);
		[Description("Destination is a Read-Only DVD, possibly unformatted")]
		public const int COPYENGINE_E_DEST_IS_RO_DVD = unchecked((int)0x80270012);
		[Description("Destination is a Read/Wrote DVD, possibly unformatted")]
		public const int COPYENGINE_E_DEST_IS_RW_DVD = unchecked((int)0x80270013);
		[Description("Destination is a Recordable (Audio, DVD, possibly unformatted")]
		public const int COPYENGINE_E_DEST_IS_R_DVD = unchecked((int)0x80270014);
		[Description("Source is a Read-Only CDRom, possibly unformatted")]
		public const int COPYENGINE_E_SRC_IS_RO_CD = unchecked((int)0x80270015);
		[Description("Source is a Read/Write CDRom, possibly unformatted")]
		public const int COPYENGINE_E_SRC_IS_RW_CD = unchecked((int)0x80270016);
		[Description("Source is a Recordable (Audio, CDRom, possibly unformatted")]
		public const int COPYENGINE_E_SRC_IS_R_CD = unchecked((int)0x80270017);
		[Description("Source is a Read-Only DVD, possibly unformatted")]
		public const int COPYENGINE_E_SRC_IS_RO_DVD = unchecked((int)0x80270018);
		[Description("Source is a Read/Wrote DVD, possibly unformatted")]
		public const int COPYENGINE_E_SRC_IS_RW_DVD = unchecked((int)0x80270019);
		[Description("Source is a Recordable (Audio, DVD, possibly unformatted")]
		public const int COPYENGINE_E_SRC_IS_R_DVD = unchecked((int)0x8027001A);
		[Description("Invalid source path")]
		public const int COPYENGINE_E_INVALID_FILES_SRC = unchecked((int)0x8027001B);
		[Description("Invalid destination path")]
		public const int COPYENGINE_E_INVALID_FILES_DEST = unchecked((int)0x8027001C);
		[Description("Source Files within folders where the overall path is longer than MAX_PATH")]
		public const int COPYENGINE_E_PATH_TOO_DEEP_SRC = unchecked((int)0x8027001D);
		[Description("Destination files would be within folders where the overall path is longer than MAX_PATH")]
		public const int COPYENGINE_E_PATH_TOO_DEEP_DEST = unchecked((int)0x8027001E);
		[Description("Source is a root directory, cannot be moved or renamed")]
		public const int COPYENGINE_E_ROOT_DIR_SRC = unchecked((int)0x8027001F);
		[Description("Destination is a root directory, cannot be renamed")]
		public const int COPYENGINE_E_ROOT_DIR_DEST = unchecked((int)0x80270020);
		[Description("Security problem on source")]
		public const int COPYENGINE_E_ACCESS_DENIED_SRC = unchecked((int)0x80270021);
		[Description("Security problem on destination")]
		public const int COPYENGINE_E_ACCESS_DENIED_DEST = unchecked((int)0x80270022);
		[Description("Source file does not exist, or is unavailable")]
		public const int COPYENGINE_E_PATH_NOT_FOUND_SRC = unchecked((int)0x80270023);
		[Description("Destination file does not exist, or is unavailable")]
		public const int COPYENGINE_E_PATH_NOT_FOUND_DEST = unchecked((int)0x80270024);
		[Description("Source file is on a disconnected network location")]
		public const int COPYENGINE_E_NET_DISCONNECT_SRC = unchecked((int)0x80270025);
		[Description("Destination file is on a disconnected network location")]
		public const int COPYENGINE_E_NET_DISCONNECT_DEST = unchecked((int)0x80270026);
		[Description("Sharing Violation on source")]
		public const int COPYENGINE_E_SHARING_VIOLATION_SRC = unchecked((int)0x80270027);
		[Description("Sharing Violation on destination")]
		public const int COPYENGINE_E_SHARING_VIOLATION_DEST = unchecked((int)0x80270028);
		[Description("Destination exists, cannot replace")]
		public const int COPYENGINE_E_ALREADY_EXISTS_NORMAL = unchecked((int)0x80270029);
		[Description("Destination with read-only attribute exists, cannot replace")]
		public const int COPYENGINE_E_ALREADY_EXISTS_READONLY = unchecked((int)0x8027002A);
		[Description("Destination with system attribute exists, cannot replace")]
		public const int COPYENGINE_E_ALREADY_EXISTS_SYSTEM = unchecked((int)0x8027002B);
		[Description("Destination folder exists, cannot replace")]
		public const int COPYENGINE_E_ALREADY_EXISTS_FOLDER = unchecked((int)0x8027002C);
		[Description("Secondary Stream information would be lost")]
		public const int COPYENGINE_E_STREAM_LOSS = unchecked((int)0x8027002D);
		[Description("Extended Attributes would be lost")]
		public const int COPYENGINE_E_EA_LOSS = unchecked((int)0x8027002E);
		[Description("Property would be lost")]
		public const int COPYENGINE_E_PROPERTY_LOSS = unchecked((int)0x8027002F);
		[Description("Properties would be lost")]
		public const int COPYENGINE_E_PROPERTIES_LOSS = unchecked((int)0x80270030);
		[Description("Encryption would be lost")]
		public const int COPYENGINE_E_ENCRYPTION_LOSS = unchecked((int)0x80270031);
		[Description("Entire operation likely won't fit")]
		public const int COPYENGINE_E_DISK_FULL = unchecked((int)0x80270032);
		[Description("Entire operation likely won't fit, clean-up wizard available")]
		public const int COPYENGINE_E_DISK_FULL_CLEAN = unchecked((int)0x80270033);
		[Description("Can't reach source folder")]
		public const int COPYENGINE_E_CANT_REACH_SOURCE = unchecked((int)0x80270035);
		[Description("???")]
		public const int COPYENGINE_E_RECYCLE_UNKNOWN_ERROR = unchecked((int)0x80270035);
		[Description("Recycling not available (usually turned off,")]
		public const int COPYENGINE_E_RECYCLE_FORCE_NUKE = unchecked((int)0x80270036);
		[Description("Item is too large for the recycle-bin")]
		public const int COPYENGINE_E_RECYCLE_SIZE_TOO_BIG = unchecked((int)0x80270037);
		[Description("Folder is too deep to fit in the recycle-bin")]
		public const int COPYENGINE_E_RECYCLE_PATH_TOO_LONG = unchecked((int)0x80270038);
		[Description("Recycle bin could not be found or is unavailable")]
		public const int COPYENGINE_E_RECYCLE_BIN_NOT_FOUND = unchecked((int)0x8027003A);
		[Description("Name of the new file being created is too long")]
		public const int COPYENGINE_E_NEWFILE_NAME_TOO_LONG = unchecked((int)0x8027003B);
		[Description("Name of the new folder being created is too long")]
		public const int COPYENGINE_E_NEWFOLDER_NAME_TOO_LONG = unchecked((int)0x8027003C);
		[Description("The directory being processed is not empty")]
		public const int COPYENGINE_E_DIR_NOT_EMPTY = unchecked((int)0x8027003D);

		[Description("The IPv6 protocol is not installed.")]
		public const int PEER_E_IPV6_NOT_INSTALLED = unchecked((int)0x80630001);

		[Description("The component has not been initialized.")]
		public const int PEER_E_NOT_INITIALIZED = unchecked((int)0x80630002);

		[Description("The required service cannot be started.")]
		public const int PEER_E_CANNOT_START_SERVICE = unchecked((int)0x80630003);

		[Description("The P2P protocol is not licensed to run on this OS.")]
		public const int PEER_E_NOT_LICENSED = unchecked((int)0x80630004);

		[Description("The graph handle is invalid.")]
		public const int PEER_E_INVALID_GRAPH = unchecked((int)0x80630010);

		[Description("The graph database name has changed.")]
		public const int PEER_E_DBNAME_CHANGED = unchecked((int)0x80630011);

		[Description("A graph with the same ID already exists.")]
		public const int PEER_E_DUPLICATE_GRAPH = unchecked((int)0x80630012);

		[Description("The graph is not ready.")]
		public const int PEER_E_GRAPH_NOT_READY = unchecked((int)0x80630013);

		[Description("The graph is shutting down.")]
		public const int PEER_E_GRAPH_SHUTTING_DOWN = unchecked((int)0x80630014);

		[Description("The graph is still in use.")]
		public const int PEER_E_GRAPH_IN_USE = unchecked((int)0x80630015);

		[Description("The graph database is corrupt.")]
		public const int PEER_E_INVALID_DATABASE = unchecked((int)0x80630016);

		[Description("Too many attributes have been used.")]
		public const int PEER_E_TOO_MANY_ATTRIBUTES = unchecked((int)0x80630017);

		[Description("The connection can not be found.")]
		public const int PEER_E_CONNECTION_NOT_FOUND = unchecked((int)0x80630103);

		[Description("The peer attempted to connect to itself.")]
		public const int PEER_E_CONNECT_SELF = unchecked((int)0x80630106);

		[Description("The peer is already listening for connections.")]
		public const int PEER_E_ALREADY_LISTENING = unchecked((int)0x80630107);

		[Description("The node was not found.")]
		public const int PEER_E_NODE_NOT_FOUND = unchecked((int)0x80630108);

		[Description("The Connection attempt failed.")]
		public const int PEER_E_CONNECTION_FAILED = unchecked((int)0x80630109);

		[Description("The peer connection could not be authenticated.")]
		public const int PEER_E_CONNECTION_NOT_AUTHENTICATED = unchecked((int)0x8063010A);

		[Description("The connection was refused.")]
		public const int PEER_E_CONNECTION_REFUSED = unchecked((int)0x8063010B);

		[Description("The peer name classifier is too long.")]
		public const int PEER_E_CLASSIFIER_TOO_LONG = unchecked((int)0x80630201);

		[Description("The maximum number of identities have been created.")]
		public const int PEER_E_TOO_MANY_IDENTITIES = unchecked((int)0x80630202);

		[Description("Unable to access a key.")]
		public const int PEER_E_NO_KEY_ACCESS = unchecked((int)0x80630203);

		[Description("The group already exists.")]
		public const int PEER_E_GROUPS_EXIST = unchecked((int)0x80630204);

		[Description("The requested record could not be found.")]
		public const int PEER_E_RECORD_NOT_FOUND = unchecked((int)0x80630301);

		[Description("Access to the database was denied.")]
		public const int PEER_E_DATABASE_ACCESSDENIED = unchecked((int)0x80630302);

		[Description("The Database could not be initialized.")]
		public const int PEER_E_DBINITIALIZATION_FAILED = unchecked((int)0x80630303);

		[Description("The record is too big.")]
		public const int PEER_E_MAX_RECORD_SIZE_EXCEEDED = unchecked((int)0x80630304);

		[Description("The database already exists.")]
		public const int PEER_E_DATABASE_ALREADY_PRESENT = unchecked((int)0x80630305);

		[Description("The database could not be found.")]
		public const int PEER_E_DATABASE_NOT_PRESENT = unchecked((int)0x80630306);

		[Description("The identity could not be found.")]
		public const int PEER_E_IDENTITY_NOT_FOUND = unchecked((int)0x80630401);

		[Description("The event handle could not be found.")]
		public const int PEER_E_EVENT_HANDLE_NOT_FOUND = unchecked((int)0x80630501);

		[Description("Invalid search.")]
		public const int PEER_E_INVALID_SEARCH = unchecked((int)0x80630601);

		[Description("The search attributes are invalid.")]
		public const int PEER_E_INVALID_ATTRIBUTES = unchecked((int)0x80630602);

		[Description("The invitation is not trusted.")]
		public const int PEER_E_INVITATION_NOT_TRUSTED = unchecked((int)0x80630701);

		[Description("The certchain is too long.")]
		public const int PEER_E_CHAIN_TOO_LONG = unchecked((int)0x80630703);

		[Description("The time period is invalid.")]
		public const int PEER_E_INVALID_TIME_PERIOD = unchecked((int)0x80630705);

		[Description("A circular cert chain was detected.")]
		public const int PEER_E_CIRCULAR_CHAIN_DETECTED = unchecked((int)0x80630706);

		[Description("The certstore is corrupted.")]
		public const int PEER_E_CERT_STORE_CORRUPTED = unchecked((int)0x80630801);

		[Description("The specified PNRP cloud does not exist.")]
		public const int PEER_E_NO_CLOUD = unchecked((int)0x80631001);

		[Description("The cloud name is ambiguous.")]
		public const int PEER_E_CLOUD_NAME_AMBIGUOUS = unchecked((int)0x80631005);

		[Description("The record is invalid.")]
		public const int PEER_E_INVALID_RECORD = unchecked((int)0x80632010);

		[Description("Not authorized.")]
		public const int PEER_E_NOT_AUTHORIZED = unchecked((int)0x80632020);

		[Description("The password does not meet policy requirements.")]
		public const int PEER_E_PASSWORD_DOES_NOT_MEET_POLICY = unchecked((int)0x80632021);

		[Description("The record validation has been deferred.")]
		public const int PEER_E_DEFERRED_VALIDATION = unchecked((int)0x80632030);

		[Description("The group properties are invalid.")]
		public const int PEER_E_INVALID_GROUP_PROPERTIES = unchecked((int)0x80632040);

		[Description("The peername is invalid.")]
		public const int PEER_E_INVALID_PEER_NAME = unchecked((int)0x80632050);

		[Description("The classifier is invalid.")]
		public const int PEER_E_INVALID_CLASSIFIER = unchecked((int)0x80632060);

		[Description("The friendly name is invalid.")]
		public const int PEER_E_INVALID_FRIENDLY_NAME = unchecked((int)0x80632070);

		[Description("Invalid role property.")]
		public const int PEER_E_INVALID_ROLE_PROPERTY = unchecked((int)0x80632071);

		[Description("Invalid classifier property.")]
		public const int PEER_E_INVALID_CLASSIFIER_PROPERTY = unchecked((int)0x80632072);

		[Description("Invalid record expiration.")]
		public const int PEER_E_INVALID_RECORD_EXPIRATION = unchecked((int)0x80632080);

		[Description("Invalid credential info.")]
		public const int PEER_E_INVALID_CREDENTIAL_INFO = unchecked((int)0x80632081);

		[Description("Invalid credential.")]
		public const int PEER_E_INVALID_CREDENTIAL = unchecked((int)0x80632082);

		[Description("Invalid record size.")]
		public const int PEER_E_INVALID_RECORD_SIZE = unchecked((int)0x80632083);

		[Description("Unsupported version.")]
		public const int PEER_E_UNSUPPORTED_VERSION = unchecked((int)0x80632090);

		[Description("The group is not ready.")]
		public const int PEER_E_GROUP_NOT_READY = unchecked((int)0x80632091);

		[Description("The group is still in use.")]
		public const int PEER_E_GROUP_IN_USE = unchecked((int)0x80632092);

		[Description("The group is invalid.")]
		public const int PEER_E_INVALID_GROUP = unchecked((int)0x80632093);

		[Description("No members were found.")]
		public const int PEER_E_NO_MEMBERS_FOUND = unchecked((int)0x80632094);

		[Description("There are no member connections.")]
		public const int PEER_E_NO_MEMBER_CONNECTIONS = unchecked((int)0x80632095);

		[Description("Unable to listen.")]
		public const int PEER_E_UNABLE_TO_LISTEN = unchecked((int)0x80632096);

		[Description("The identity does not exist.")]
		public const int PEER_E_IDENTITY_DELETED = unchecked((int)0x806320A0);

		[Description("The service is not available.")]
		public const int PEER_E_SERVICE_NOT_AVAILABLE = unchecked((int)0x806320A1);

		[Description("THe contact could not be found.")]
		public const int PEER_E_CONTACT_NOT_FOUND = unchecked((int)0x80636001);

		[Description("The graph data was created.")]
		public const int PEER_S_GRAPH_DATA_CREATED = unchecked((int)0x00630001);

		[Description("There is not more event data.")]
		public const int PEER_S_NO_EVENT_DATA = unchecked((int)0x00630002);

		[Description("The graph is already connect.")]
		public const int PEER_S_ALREADY_CONNECTED = unchecked((int)0x00632000);

		[Description("The subscription already exists.")]
		public const int PEER_S_SUBSCRIPTION_EXISTS = unchecked((int)0x00636000);

		[Description("No connectivity.")]
		public const int PEER_S_NO_CONNECTIVITY = unchecked((int)0x00630005);

		[Description("Already a member.")]
		public const int PEER_S_ALREADY_A_MEMBER = unchecked((int)0x00630006);

		[Description("The peername could not be converted to a DNS pnrp name.")]
		public const int PEER_E_CANNOT_CONVERT_PEER_NAME = unchecked((int)0x80634001);

		[Description("Invalid peer host name.")]
		public const int PEER_E_INVALID_PEER_HOST_NAME = unchecked((int)0x80634002);

		[Description("No more data could be found.")]
		public const int PEER_E_NO_MORE = unchecked((int)0x80634003);

		[Description("The existing peer name is already registered.")]
		public const int PEER_E_PNRP_DUPLICATE_PEER_NAME = unchecked((int)0x80634005);

		[Description("The app invite request was cancelled by the user.")]
		public const int PEER_E_INVITE_CANCELLED = unchecked((int)0x80637000);

		[Description("No response of the invite was received.")]
		public const int PEER_E_INVITE_RESPONSE_NOT_AVAILABLE = unchecked((int)0x80637001);

		[Description("User is not signed into serverless presence.")]
		public const int PEER_E_NOT_SIGNED_IN = unchecked((int)0x80637003);

		[Description("The user declined the privacy policy prompt.")]
		public const int PEER_E_PRIVACY_DECLINED = unchecked((int)0x80637004);

		[Description("A timeout occurred.")]
		public const int PEER_E_TIMEOUT = unchecked((int)0x80637005);

		[Description("The address is invalid.")]
		public const int PEER_E_INVALID_ADDRESS = unchecked((int)0x80637007);

		[Description("A required firewall exception is disabled.")]
		public const int PEER_E_FW_EXCEPTION_DISABLED = unchecked((int)0x80637008);

		[Description("The service is blocked by a firewall policy.")]
		public const int PEER_E_FW_BLOCKED_BY_POLICY = unchecked((int)0x80637009);

		[Description("Firewall exceptions are disabled.")]
		public const int PEER_E_FW_BLOCKED_BY_SHIELDS_UP = unchecked((int)0x8063700A);

		[Description("The user declined to enable the firewall exceptions.")]
		public const int PEER_E_FW_DECLINED = unchecked((int)0x8063700B);

		[Description("The IAudioClient object is already initialized.")]
		public static readonly HRESULT AUDCLNT_E_ALREADY_INITIALIZED = AUDCLNT_ERR(0x002);

		[Description("The AUDCLNT_STREAMFLAGS_EVENTCALLBACK flag is set but parameters hnsBufferDuration and hnsPeriodicity are not equal.")]
		public static readonly HRESULT AUDCLNT_E_BUFDURATION_PERIOD_NOT_EQUAL = AUDCLNT_ERR(0x013);

		[Description("GetBuffer failed to retrieve a data buffer and *ppData points to NULL. For more information, see Remarks.")]
		public static readonly HRESULT AUDCLNT_E_BUFFER_ERROR = AUDCLNT_ERR(0x018);

		[Description("Buffer cannot be accessed because a stream reset is in progress.")]
		public static readonly HRESULT AUDCLNT_E_BUFFER_OPERATION_PENDING = AUDCLNT_ERR(0x00b);

		[Description(
			"Indicates that the buffer duration value requested by an exclusive-mode client is out of range. The requested duration value for "+
			"pull mode must not be greater than 500 milliseconds; for push mode the duration value must not be greater than 2 seconds.")]
		public static readonly HRESULT AUDCLNT_E_BUFFER_SIZE_ERROR = AUDCLNT_ERR(0x016);

		[Description(
			"The requested buffer size is not aligned. This code can be returned for a render or a capture device if the caller specified "+
			"AUDCLNT_SHAREMODE_EXCLUSIVE and the AUDCLNT_STREAMFLAGS_EVENTCALLBACK flags. The caller must call Initialize again with the "+
			"aligned buffer size. For more information, see Remarks.")]
		public static readonly HRESULT AUDCLNT_E_BUFFER_SIZE_NOT_ALIGNED = AUDCLNT_ERR(0x019);

		[Description("The NumFramesRequested value exceeds the available buffer space (buffer size minus padding size).")]
		public static readonly HRESULT AUDCLNT_E_BUFFER_TOO_LARGE = AUDCLNT_ERR(0x006);

		[Description(
			"Indicates that the process-pass duration exceeded the maximum CPU usage. The audio engine keeps track of CPU usage by " +
			"maintaining the number of times the process-pass duration exceeds the maximum CPU usage. The maximum CPU usage is calculated as " +
			"a percent of the engine's periodicity. The percentage value is the system's CPU throttle value (within the range of 10% and " +
			"90%). If this value is not found, then the default value of 40% is used to calculate the maximum CPU usage.")]
		public static readonly HRESULT AUDCLNT_E_CPUUSAGE_EXCEEDED = AUDCLNT_ERR(0x017);

		[Description(
			"The endpoint device is already in use. Either the device is being used in exclusive mode, or the device is being used in shared " +
			"mode and the caller asked to use the device in exclusive mode.")]
		public static readonly HRESULT AUDCLNT_E_DEVICE_IN_USE = AUDCLNT_ERR(0x00a);

		[Description(
			"The audio endpoint device has been unplugged, or the audio hardware or associated hardware resources have been reconfigured, " +
			"disabled, removed, or otherwise made unavailable for use.")]
		public static readonly HRESULT AUDCLNT_E_DEVICE_INVALIDATED = AUDCLNT_ERR(0x004);

		[Description(
			"The method failed to create the audio endpoint for the render or the capture device.This can occur if the audio endpoint device"+
			" has been unplugged, or the audio hardware or associated hardware resources have been reconfigured, disabled, removed, or"+
			"otherwise made unavailable for use.")]
        public static readonly HRESULT AUDCLNT_E_ENDPOINT_CREATE_FAILED = AUDCLNT_ERR(0x00f);

		[Description("The endpoint does not support offloading.")]
		public static readonly HRESULT AUDCLNT_E_ENDPOINT_OFFLOAD_NOT_CAPABLE = AUDCLNT_ERR(0x022);

		[Description(
			"The client specified AUDCLNT_STREAMOPTIONS_MATCH_FORMAT when calling IAudioClient2::SetClientProperties, but the format of the"+
			"audio engine has been locked by another client. In this case, you can call IAudioClient2::SetClientProperties without specifying"+
			"the match format option and then use audio engine's current format.")]
		public static readonly HRESULT AUDCLNT_E_ENGINE_FORMAT_LOCKED = AUDCLNT_ERR(0x029);

		[Description(
			"The client specified AUDCLNT_STREAMOPTIONS_MATCH_FORMAT when calling IAudioClient2::SetClientProperties, but the periodicity of"+
			"the audio engine has been locked by another client. In this case, you can call IAudioClient2::SetClientProperties without"+
			"specifying the match format option and then use audio engine's current periodicity.")]
		public static readonly HRESULT AUDCLNT_E_ENGINE_PERIODICITY_LOCKED = AUDCLNT_ERR(0x028);

		[Description("The audio stream was not initialized for event-driven buffering.")]
		public static readonly HRESULT AUDCLNT_E_EVENTHANDLE_NOT_EXPECTED = AUDCLNT_ERR(0x011);

		[Description(
			"The audio stream is configured to use event-driven buffering, but the caller has not called IAudioClient::SetEventHandle to set"+
			"the event handle on the stream.")]
		public static readonly HRESULT AUDCLNT_E_EVENTHANDLE_NOT_SET = AUDCLNT_ERR(0x014);

		[Description("The caller is requesting exclusive-mode use of the endpoint device, but the user has disabled exclusive-mode use of the device.")]
		public static readonly HRESULT AUDCLNT_E_EXCLUSIVE_MODE_NOT_ALLOWED = AUDCLNT_ERR(0x00e);

		[Description("Exclusive mode only.")]
		public static readonly HRESULT AUDCLNT_E_EXCLUSIVE_MODE_ONLY = AUDCLNT_ERR(0x012);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_HEADTRACKING_ENABLED = AUDCLNT_ERR(0x030);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_HEADTRACKING_UNSUPPORTED = AUDCLNT_ERR(0x040);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_INCORRECT_BUFFER_SIZE = AUDCLNT_ERR(0x015);

		[Description(
			"Indicates that the requested device period specified with the PeriodInFrames is not an integral multiple of the fundamental "+
			"periodicity of the audio engine, is shorter than the engine's minimum period, or is longer than the engine's maximum period. Get "+
			"the supported periodicity values of the engine by calling IAudioClient3::GetSharedModeEnginePeriod.")]
		public static readonly HRESULT AUDCLNT_E_INVALID_DEVICE_PERIOD = AUDCLNT_ERR(0x020);

		[Description("The NumFramesWritten value exceeds the NumFramesRequested value specified in the previous IAudioRenderClient::GetBuffer call.")]
		public static readonly HRESULT AUDCLNT_E_INVALID_SIZE = AUDCLNT_ERR(0x009);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_INVALID_STREAM_FLAG = AUDCLNT_ERR(0x021);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_NONOFFLOAD_MODE_ONLY = AUDCLNT_ERR(0x025);

		[Description("The audio stream has not been successfully initialized.")]
		public static readonly HRESULT AUDCLNT_E_NOT_INITIALIZED = AUDCLNT_ERR(0x001);

		[Description("The audio stream was not stopped at the time of the Start call.")]
		public static readonly HRESULT AUDCLNT_E_NOT_STOPPED = AUDCLNT_ERR(0x005);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_OFFLOAD_MODE_ONLY = AUDCLNT_ERR(0x024);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_OUT_OF_OFFLOAD_RESOURCES = AUDCLNT_ERR(0x023);

		[Description("A previous IAudioRenderClient::GetBuffer call is still in effect.")]
		public static readonly HRESULT AUDCLNT_E_OUT_OF_ORDER = AUDCLNT_ERR(0x007);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_RAW_MODE_UNSUPPORTED = AUDCLNT_ERR(0x027);

		[Description("A resource associated with the spatial audio stream is no longer valid.")]
		public static readonly HRESULT AUDCLNT_E_RESOURCES_INVALIDATED = AUDCLNT_ERR(0x026);

		[Description("The Windows audio service is not running.")]
		public static readonly HRESULT AUDCLNT_E_SERVICE_NOT_RUNNING = AUDCLNT_ERR(0x010);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_E_THREAD_NOT_REGISTERED = AUDCLNT_ERR(0x00c);

		[Description("The audio engine (shared mode) or audio endpoint device (exclusive mode) does not support the specified format.")]
		public static readonly HRESULT AUDCLNT_E_UNSUPPORTED_FORMAT = AUDCLNT_ERR(0x008);

		[Description("The AUDCLNT_STREAMFLAGS_LOOPBACK flag is set but the endpoint device is a capture device, not a rendering device.")]
		public static readonly HRESULT AUDCLNT_E_WRONG_ENDPOINT_TYPE = AUDCLNT_ERR(0x003);

		[Description("The call succeeded and *pNumFramesToRead is 0, indicating that no capture data is available to be read.")]
		public static readonly HRESULT AUDCLNT_S_BUFFER_EMPTY = AUDCLNT_SUCCESS(0x001);

		[Description("The IAudioClient::Start method has not been called for this stream.")]
		public static readonly HRESULT AUDCLNT_S_POSITION_STALLED = AUDCLNT_SUCCESS(0x003);

		/// <summary/>
		public static readonly HRESULT AUDCLNT_S_THREAD_ALREADY_REGISTERED = AUDCLNT_SUCCESS(0x002);

		private static HRESULT AUDCLNT_ERR(uint n) => Make(false, FacilityCode.FACILITY_AUDCLNT, n);

		private static HRESULT AUDCLNT_SUCCESS(uint n) => Make(true, FacilityCode.FACILITY_AUDCLNT, n);

		/// <summary/>
		public static readonly HRESULT DRT_E_TIMEOUT = Make(true, 98U, 0x1001);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_KEY_SIZE = Make(true, 98U, 0x1002);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_CERT_CHAIN = Make(true, 98U, 0x1004);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_MESSAGE = Make(true, 98U, 0x1005);
		/// <summary/>
		public static readonly HRESULT DRT_E_NO_MORE = Make(true, 98U, 0x1006);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_MAX_ADDRESSES = Make(true, 98U, 0x1007);
		/// <summary/>
		public static readonly HRESULT DRT_E_SEARCH_IN_PROGRESS = Make(true, 98U, 0x1008);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_KEY = Make(true, 98U, 0x1009);
		/// <summary/>
		public static readonly HRESULT DRT_S_RETRY = Make(false, 98U, 0x1010);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_MAX_ENDPOINTS = Make(true, 98U, 0x1011);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_SEARCH_RANGE = Make(true, 98U, 0x1012);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_PORT = Make(true, 98U, 0x2000);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_TRANSPORT_PROVIDER = Make(true, 98U, 0x2001);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_SECURITY_PROVIDER = Make(true, 98U, 0x2002);
		/// <summary/>
		public static readonly HRESULT DRT_E_STILL_IN_USE = Make(true, 98U, 0x2003);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_BOOTSTRAP_PROVIDER = Make(true, 98U, 0x2004);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_ADDRESS = Make(true, 98U, 0x2005);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_SCOPE = Make(true, 98U, 0x2006);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_SHUTTING_DOWN = Make(true, 98U, 0x2007);
		/// <summary/>
		public static readonly HRESULT DRT_E_NO_ADDRESSES_AVAILABLE = Make(true, 98U, 0x2008);
		/// <summary/>
		public static readonly HRESULT DRT_E_DUPLICATE_KEY = Make(true, 98U, 0x2009);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORTPROVIDER_IN_USE = Make(true, 98U, 0x200a);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORTPROVIDER_NOT_ATTACHED = Make(true, 98U, 0x200b);
		/// <summary/>
		public static readonly HRESULT DRT_E_SECURITYPROVIDER_IN_USE = Make(true, 98U, 0x200c);
		/// <summary/>
		public static readonly HRESULT DRT_E_SECURITYPROVIDER_NOT_ATTACHED = Make(true, 98U, 0x200d);
		/// <summary/>
		public static readonly HRESULT DRT_E_BOOTSTRAPPROVIDER_IN_USE = Make(true, 98U, 0x200e);
		/// <summary/>
		public static readonly HRESULT DRT_E_BOOTSTRAPPROVIDER_NOT_ATTACHED = Make(true, 98U, 0x200f);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_ALREADY_BOUND = Make(true, 98U, 0x2101);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_NOT_BOUND = Make(true, 98U, 0x2102);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_UNEXPECTED = Make(true, 98U, 0x2103);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_INVALID_ARGUMENT = Make(true, 98U, 0x2104);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_NO_DEST_ADDRESSES = Make(true, 98U, 0x2105);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_EXECUTING_CALLBACK = Make(true, 98U, 0x2106);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_ALREADY_EXISTS_FOR_SCOPE = Make(true, 98U, 0x2107);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_SETTINGS = Make(true, 98U, 0x2108);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_SEARCH_INFO = Make(true, 98U, 0x2109);
		/// <summary/>
		public static readonly HRESULT DRT_E_FAULTED = Make(true, 98U, 0x210a);
		/// <summary/>
		public static readonly HRESULT DRT_E_TRANSPORT_STILL_BOUND = Make(true, 98U, 0x210b);
		/// <summary/>
		public static readonly HRESULT DRT_E_INSUFFICIENT_BUFFER = Make(true, 98U, 0x210c);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_INSTANCE_PREFIX = Make(true, 98U, 0x210d);
		/// <summary/>
		public static readonly HRESULT DRT_E_INVALID_SECURITY_MODE = Make(true, 98U, 0x210e);
		/// <summary/>
		public static readonly HRESULT DRT_E_CAPABILITY_MISMATCH = Make(true, 98U, 0x210f);
	}
}