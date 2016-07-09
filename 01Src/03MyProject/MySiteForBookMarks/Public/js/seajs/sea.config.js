seajs.config({

    // 路径配置
    paths: {
        'js': '/Public/js',
        'self':'/Public/js/self'
    },

    // 别名配置
    alias: {
        'jquery': 'js/jquery-1.12.0.min.js',
        'autocomplete':'js/jquery.autocomplete.min.js',
        'vue':'js/vuejs/vue.js'
    },

    // 变量配置
    vars: {
        'locale': 'zh-cn'
    },

    // 映射配置
    map: [
        ['', '']
    ],

    // 预加载项
    preload: [

    ],

    // 调试模式
    debug: true,

    // Sea.js 的基础路径
    base: 'js/seajs/',

    // 文件编码
    charset: 'utf-8'
});