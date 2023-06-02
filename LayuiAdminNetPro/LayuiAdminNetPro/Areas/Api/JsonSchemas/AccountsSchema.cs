namespace LayuiAdminNetCore.Schemas
{
    public static class AccountsSchema
    {
        #region  创建账号

        public const string Create = @"{
            'type': 'object',
            'properties': {
                'name': {
                    'type': 'string',
                    'title': '账号名称',
                },
                'phone': {
                    'type': 'number',
                    'title': '手机号'
                },
                'password': {
                    'type': 'string',
                    'title': '密码',
                    'minLength': 6,
                    'maxLength': 12,
                },
                'sex': {
                    'type': 'number',
                    'minimum': 0,
                    'maximum': 3,
                    'enum': [
                        0,
                        1,
                        2
                    ],
                    'title': '性别'
                },
                'jobStatus': {
                    'type': 'number',
                    'enum': [
                        0,
                        1,
                        2,
                        3
                    ],
                    'title': '工作状态'
                }
            },
            'required': [
                'name',
                'phone',
                'password',
                'sex',
                'jobStatus'
            ]
        }";

        #endregion 创建账号接口 Schema
    }
}