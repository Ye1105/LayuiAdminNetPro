using CodeHelper.Common;

namespace LayuiAdminNetCore.Enums
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 在职
        /// </summary>
        [EnumDescription("在职")]
        ON_JOB = 0,

        /// <summary>
        /// 离职
        /// </summary>
        [EnumDescription("离职")]
        DIMISSION = 1,

        /// <summary>
        /// 出差
        /// </summary>
        [EnumDescription("出差")]
        Evection = 2,

        /// <summary>
        /// 休假
        /// </summary>
        [EnumDescription("休假")]
        VOCATION = 3,
    }

    /// <summary>
    /// 行为状态
    /// </summary>
    public enum ActionStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        [EnumDescription("启用")]
        ENABLE = 0,

        /// <summary>
        /// 禁用
        /// </summary>
        [EnumDescription("禁用")]
        DISABLE = 1,

        /// <summary>
        /// 审核中
        /// </summary>
        [EnumDescription("审核中")]
        UNDER_REVIEW = 2,

        /// <summary>
        /// 审核失败
        /// </summary>
        [EnumDescription("审核失败")]
        AUDIT_FAILURE = 3,
    }

    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum SexStatus
    {
        /// <summary>
        /// 男性
        /// </summary>
        [EnumDescription("男性")]
        MAlE = 0,

        /// <summary>
        /// 女性
        /// </summary>
        [EnumDescription("女性")]
        FEMALE = 1,

        /// <summary>
        /// 女性
        /// </summary>
        [EnumDescription("中性")]
        NEUTRAL = 2
    }

    /// <summary>
    /// 增删改查
    /// </summary>
    public enum CrudType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [EnumDescription("添加")]
        CREATE = 0,

        /// <summary>
        /// 读取
        /// </summary>
        [EnumDescription("读取")]
        READ = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [EnumDescription("修改")]
        UPDATE = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [EnumDescription("删除")]
        DELETE = 3,

        /// <summary>
        /// 未知
        /// </summary>
        [EnumDescription("未知")]
        UNKNOWN = 4,
    }
}