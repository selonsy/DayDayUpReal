<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
         <title>ThinkPHP示例：模板继承</title>
	<style type="text/css">
	*{ padding: 0; margin: 0;font-size:16px; font-family: "微软雅黑"} 
	div{ padding: 3px 20px;} 
	body{ background: #fff; color: #333;}
	h2{font-size:36px}
	a{text-decoration:none; color:#174B73; border-bottom:1px dashed gray}
	a:hover{color:#F60; border-bottom:1px dashed gray}
	div.result{border:1px solid #d4d4d4; background:#FFC;color:#393939; padding:8px 20px;float:auto; width:350px;margin:2px}
	</style>
    </head>
    <body>
        <div class="main">
            <h2><?php echo ($title); ?></h2>
			<div class="result">这里是父模板定义内容</div>
            
<div class="result" style="background:#6699ff;color:white"><?php echo ($var); ?></div>

			
        </div>
    </body>
</html>