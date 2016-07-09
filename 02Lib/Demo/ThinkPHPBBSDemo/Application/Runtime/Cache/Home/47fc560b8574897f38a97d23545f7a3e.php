<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html lang="zh-cn">
<head>
<title>2014年辛星简易BBS登录页面</title>
<link href="/thinkbbs/Public/CSS/xinxing.css" type="text/css" rel="stylesheet"/>
<link href = "/thinkbbs/Public/CSS/board.css" rel = "stylesheet">
</head><body>
<div class="page-header">
   <h1>辛星简易BBS
      <small> <?php echo ($board["name"]); ?> 版块</small>
   </h1>
</div>
<div id = "user">
<?php if(cookie('username')) { ?>
<a href = "#"><?php echo cookie('username'); ?></a>
<a href = "/thinkbbs/index.php/Home/User/logout">注销</a>
<?php  }else{ ?>
<a href = "/thinkbbs/index.php/Home/User/login">登录</a><br />
<a href = "/thinkbbs/index.php/Home/User/reg">注册</a>
<?php } ?>
</div>
<table class = "table">
<?php if(is_array($post)): foreach($post as $key=>$vo): ?><tr><td>第<?php echo ($key); ?>帖：</td>
<td><a href= "/thinkbbs/index.php/Home/Post/index/id/<?php echo ($vo["id"]); ?>"><?php echo ($vo["text"]); ?></a></td>
<td>楼主：<?php echo ($vo["author"]); ?></td></tr><?php endforeach; endif; ?>

</table>
 
 <?php echo ($page); ?>
 
 
</body></html>