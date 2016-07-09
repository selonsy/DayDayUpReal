<?php
// 系统默认的核心行为扩展列表文件
return array(

	//因为项目中也可能用到语言行为,最好放在项目开始的地方, //检测语言
	// 如果是3.2.1版本 需要改成
    'app_begin' => array('Behavior\CheckLangBehavior'),
	
);