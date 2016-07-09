<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
  <title>ThinkPHP示例：多语言</title>
	<style type="text/css">
	*{ padding: 0; margin: 0;font-size:24px; font-family: "微软雅黑"} 
	div{ padding: 3px 20px;} 
	body{ background: #fff; color: #333;}
	h2{font-size:36px}
	a{text-decoration:none; color:#174B73; border-bottom:1px dashed gray}
	a:hover{color:#F60; border-bottom:1px dashed gray}
	</style>
 </head>
 <body>
 <div class="main">
 <h2>ThinkPHP示例之：多语言支持</h2>
<div>切换语言：<a href="?l=zh-cn">简体中文</a> | <a href="?l=zh-tw">繁体中文</a> | <a href="?l=en-us">英文</a></div>
 <div class="result"><?php echo (L("welcome")); ?> <?php echo (L("remark")); ?></div>
</div>
 </body>
</html>