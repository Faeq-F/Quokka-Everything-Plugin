using System.Runtime.InteropServices;
using System.Text;

namespace Plugin_Everything {
  public partial class Everything {

    #region magic values

    const int EVERYTHING_OK = 0;
    const int EVERYTHING_ERROR_MEMORY = 1;
    const int EVERYTHING_ERROR_IPC = 2;
    const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
    const int EVERYTHING_ERROR_CREATEWINDOW = 4;
    const int EVERYTHING_ERROR_CREATETHREAD = 5;
    const int EVERYTHING_ERROR_INVALIDINDEX = 6;
    const int EVERYTHING_ERROR_INVALIDCALL = 7;

    const int EVERYTHING_REQUEST_FILE_NAME = 0x00000001;
    const int EVERYTHING_REQUEST_PATH = 0x00000002;
    const int EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME = 0x00000004;
    const int EVERYTHING_REQUEST_EXTENSION = 0x00000008;
    const int EVERYTHING_REQUEST_SIZE = 0x00000010;
    const int EVERYTHING_REQUEST_DATE_CREATED = 0x00000020;
    const int EVERYTHING_REQUEST_DATE_MODIFIED = 0x00000040;
    const int EVERYTHING_REQUEST_DATE_ACCESSED = 0x00000080;
    const int EVERYTHING_REQUEST_ATTRIBUTES = 0x00000100;
    const int EVERYTHING_REQUEST_FILE_LIST_FILE_NAME = 0x00000200;
    const int EVERYTHING_REQUEST_RUN_COUNT = 0x00000400;
    const int EVERYTHING_REQUEST_DATE_RUN = 0x00000800;
    const int EVERYTHING_REQUEST_DATE_RECENTLY_CHANGED = 0x00001000;
    const int EVERYTHING_REQUEST_HIGHLIGHTED_FILE_NAME = 0x00002000;
    const int EVERYTHING_REQUEST_HIGHLIGHTED_PATH = 0x00004000;
    const int EVERYTHING_REQUEST_HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000;

    const int EVERYTHING_SORT_NAME_ASCENDING = 1;
    const int EVERYTHING_SORT_NAME_DESCENDING = 2;
    const int EVERYTHING_SORT_PATH_ASCENDING = 3;
    const int EVERYTHING_SORT_PATH_DESCENDING = 4;
    const int EVERYTHING_SORT_SIZE_ASCENDING = 5;
    const int EVERYTHING_SORT_SIZE_DESCENDING = 6;
    const int EVERYTHING_SORT_EXTENSION_ASCENDING = 7;
    const int EVERYTHING_SORT_EXTENSION_DESCENDING = 8;
    const int EVERYTHING_SORT_TYPE_NAME_ASCENDING = 9;
    const int EVERYTHING_SORT_TYPE_NAME_DESCENDING = 10;
    const int EVERYTHING_SORT_DATE_CREATED_ASCENDING = 11;
    const int EVERYTHING_SORT_DATE_CREATED_DESCENDING = 12;
    const int EVERYTHING_SORT_DATE_MODIFIED_ASCENDING = 13;
    const int EVERYTHING_SORT_DATE_MODIFIED_DESCENDING = 14;
    const int EVERYTHING_SORT_ATTRIBUTES_ASCENDING = 15;
    const int EVERYTHING_SORT_ATTRIBUTES_DESCENDING = 16;
    const int EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING = 17;
    const int EVERYTHING_SORT_FILE_LIST_FILENAME_DESCENDING = 18;
    const int EVERYTHING_SORT_RUN_COUNT_ASCENDING = 19;
    const int EVERYTHING_SORT_RUN_COUNT_DESCENDING = 20;
    const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_ASCENDING = 21;
    const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_DESCENDING = 22;
    const int EVERYTHING_SORT_DATE_ACCESSED_ASCENDING = 23;
    const int EVERYTHING_SORT_DATE_ACCESSED_DESCENDING = 24;
    const int EVERYTHING_SORT_DATE_RUN_ASCENDING = 25;
    const int EVERYTHING_SORT_DATE_RUN_DESCENDING = 26;

    const int EVERYTHING_TARGET_MACHINE_X86 = 1;
    const int EVERYTHING_TARGET_MACHINE_X64 = 2;
    const int EVERYTHING_TARGET_MACHINE_ARM = 3;

    #endregion

    /// <summary>
    /// The Everything_SetSearch function sets the search string for the IPC Query.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setsearch/
    /// </summary>
    /// <param name="lpSearchString">A string to be used as the new search text.</param>
    /// <returns>This function has no return value.</returns>
    [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
    public static extern UInt32 Everything_SetSearchW(string lpSearchString);

    /// <summary>
    /// The Everything_SetMatchPath function enables or disables full path matching for the next call to Everything_Query. <br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setmatchpath/
    /// </summary>
    /// <param name="bEnable">Specifies whether to enable or disable full path matching.</param>
    [DllImport("Everything64.dll")]
    public static extern void Everything_SetMatchPath(bool bEnable);

    /// <summary>
    /// The Everything_SetMatchCase function enables or disables case sensitivity for the next call to Everything_Query. <br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setmatchcase/
    /// </summary>
    /// <param name="bEnable">Specifies whether the search is case sensitive or insensitive.</param>
    [DllImport("Everything64.dll")]
    public static extern void Everything_SetMatchCase(bool bEnable);

    /// <summary>
    /// The Everything_SetMatchWholeWord function enables or disables matching whole words for the next call to Everything_Query.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setmatchwholeword/
    /// </summary>
    /// <param name="bEnable">Specifies whether the search matches whole words, or can match anywhere.</param>
    [DllImport("Everything64.dll")]
    public static extern void Everything_SetMatchWholeWord(bool bEnable);

    /// <summary>
    /// The Everything_SetRegex function enables or disables Regular Expression searching. <br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setregex/
    /// </summary>
    /// <param name="bEnable">Set to non-zero to enable regex, set to zero to disable regex.</param>
    [DllImport("Everything64.dll")]
    public static extern void Everything_SetRegex(bool bEnable);

    /// <summary>
    /// The Everything_SetMax function sets the maximum number of results to return from Everything_Query.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setmax/
    /// </summary>
    /// <param name="dwMax">Specifies the maximum number of results to return.</param>
    [DllImport("Everything64.dll")]
    public static extern void Everything_SetMax(UInt32 dwMax);

    /// <summary>
    /// The Everything_Query function executes an Everything IPC query with the current search state.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_query/
    /// </summary>
    /// <param name="bWait">Should the function wait for the results or return immediately.</param>
    /// <returns>If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE.</returns>
    [DllImport("Everything64.dll")]
    public static extern bool Everything_QueryW(bool bWait);

    /// <summary>
    /// The Everything_GetNumResults function returns the number of visible file and folder results.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_getnumresults/
    /// </summary>
    /// <returns>Returns the number of visible file and folder results. If the function fails the return value is 0.</returns>
    [DllImport("Everything64.dll")]
    public static extern UInt32 Everything_GetNumResults();

    /// <summary>
    /// The Everything_IsVolumeResult function determines if the visible result is the root folder of a volume.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_isvolumeresult/
    /// </summary>
    /// <param name="nIndex">Zero based index of the visible result.</param>
    /// <returns>The function returns TRUE, if the visible result is a volume (For example: C:). The function returns FALSE, if the visible result is a folder or file (For example: C:\ABC.123). If the function fails the return value is FALSE.</returns>
    [DllImport("Everything64.dll")]
    public static extern bool Everything_IsVolumeResult(UInt32 nIndex);

    /// <summary>
    /// The Everything_IsFolderResult function determines if the visible result is a folder.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_isfolderresult/
    /// </summary>
    /// <param name="nIndex">Zero based index of the visible result.</param>
    /// <returns>The function returns TRUE, if the visible result is a folder or volume (For example: C: or c:\WINDOWS). The function returns FALSE, if the visible result is a file (For example: C:\ABC.123). If the function fails the return value is FALSE.</returns>
    [DllImport("Everything64.dll")]
    public static extern bool Everything_IsFolderResult(UInt32 nIndex);

    /// <summary>
    /// The Everything_GetResultFullPathName function retrieves the full path and file name of the visible result.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_getresultfullpathname/
    /// </summary>
    /// <param name="nIndex">Zero based index of the visible result.</param>
    /// <param name="lpString">The buffer that will receive the text. If the string is as long or longer than the buffer, the string is truncated and terminated with a NULL character.</param>
    /// <param name="nMaxCount">Specifies the maximum number of characters to copy to the buffer, including the NULL character. If the text exceeds this limit, it is truncated.</param>
    [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
    public static extern void Everything_GetResultFullPathName(UInt32 nIndex, StringBuilder lpString, UInt32 nMaxCount);

    /// <summary>
    /// The Everything_GetResultFileName function retrieves the file name part only of the visible result.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_getresultfilename/
    /// </summary>
    /// <param name="nIndex">Zero based index of the visible result.</param>
    /// <returns>The function returns a pointer to a null terminated string of TCHARs. If the function fails the return value is NULL.</returns>
    [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr Everything_GetResultFileName(UInt32 nIndex);

    /// <summary>
    /// The Everything_SetRequestFlags function sets the desired result data.<br />
    /// https://www.voidtools.com/support/everything/sdk/everything_setrequestflags/
    /// </summary>
    /// <param name="dwRequestFlags">The request flags, can be zero or more of the following flags:<br />
    /// EVERYTHING_REQUEST_FILE_NAME  (0x00000001)<br />EVERYTHING_REQUEST_PATH       (0x00000002)<br />EVERYTHING_REQUEST_FULL_PATH_A(0x00000004)<br />EVERYTHING_REQUEST_EXTENSION  (0x00000008)<br />EVERYTHING_REQUEST_SIZE       (0x00000010)<br />EVERYTHING_REQUEST_DATE_CREATE(0x00000020)<br />EVERYTHING_REQUEST_DATE_MODIFI(0x00000040)<br />EVERYTHING_REQUEST_DATE_ACCESS(0x00000080)<br />EVERYTHING_REQUEST_ATTRIBUTES (0x00000100)<br />EVERYTHING_REQUEST_FILE_LIST_F(0x00000200)<br />EVERYTHING_REQUEST_RUN_COUNT  (0x00000400)<br />EVERYTHING_REQUEST_DATE_RUN   (0x00000800)<br />EVERYTHING_REQUEST_DATE_RECENT(0x00001000)<br />EVERYTHING_REQUEST_HIGHLIGHTED(0x00002000)<br />EVERYTHING_REQUEST_HIGHLIGHTED(0x00004000)<br />EVERYTHING_REQUEST_HIGHLIGHTED(0x00008000)</param>
    [DllImport("Everything64.dll")]
    public static extern void Everything_SetRequestFlags(UInt32 dwRequestFlags);

    /// <summary>
    /// The Everything_GetResultSize function retrieves the size of a visible result.<br/>
    /// https://www.voidtools.com/support/everything/sdk/everything_getresultsize/
    /// </summary>
    /// <param name="nIndex">Zero based index of the visible result.</param>
    /// <param name="lpFileSize">Pointer to a LARGE_INTEGER to hold the size of the result.</param>
    /// <returns>The function returns non-zero if successful. The function returns 0 if size information is unavailable. </returns>
    [DllImport("Everything64.dll")]
    public static extern bool Everything_GetResultSize(UInt32 nIndex, out long lpFileSize);

    /// <summary>
    /// The Everything_GetResultDateModified function retrieves the modified date of a visible result. <br/>
    /// https://www.voidtools.com/support/everything/sdk/everything_getresultdatemodified/
    /// </summary>
    /// <param name="nIndex">Zero based index of the visible result.</param>
    /// <param name="lpFileTime">A FILETIME to hold the modified date of the result.</param>
    /// <returns>The function returns non-zero if successful. The function returns 0 if the modified date information is unavailable.</returns>
    [DllImport("Everything64.dll")]
    public static extern bool Everything_GetResultDateModified(UInt32 nIndex, out long lpFileTime);
  }
}
