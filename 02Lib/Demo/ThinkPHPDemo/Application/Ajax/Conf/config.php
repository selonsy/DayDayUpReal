<?php
return array(
    'URL_MODEL'		=>	2, // 如果你的环境不支持PATHINFO 请设置为3
    'DB_TYPE'		=>	'mysql',
    'DB_HOST'		=>	'localhost',
    'DB_NAME'		=>	'think',
    'DB_USER'		=>	'root',
    'DB_PWD'		=>	'howard',
    'DB_PORT'		=>	'3306',
    'DB_PREFIX'		=>	'think_',
    
    
    /* 模板相关配置 */
    'TMPL_PARSE_STRING' => array(
        '__STATIC__' => __ROOT__ . '/Public/static',
        '__ADDONS__' => __ROOT__ . '/Public/' . MODULE_NAME . '/Addons',
        '__IMG__'    => __ROOT__ . '/Public/' . MODULE_NAME . '/images',
        '__CSS__'    => __ROOT__ . '/Public/' . MODULE_NAME . '/css',
        '__JS__'     => __ROOT__ . '/Public/' . MODULE_NAME . '/js',
    ),
    
    //应用类库不再需要使用命名空间
    //'APP_USE_NAMESPACE'    =>    false,
    
);