<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html lang="zh-cn">
<head>
<title>2014年辛星简易BBS登录页面</title>
<link href="/thinkbbs/Public/CSS/xinxing.css" type="text/css" rel="stylesheet"/>
<link href = "/thinkbbs/Public/CSS/board.css" rel = "stylesheet">
</head><body>
<div class="page-header">
   <h1>辛星简易BBS
      <small>给您更多的选择</small>
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
<?php if(is_array($board)): foreach($board as $key=>$vo): ?><tr><td>第<?php echo ($key); ?>个</td>
<td><a href= "/thinkbbs/index.php/Home/Board/detail/id/<?php echo ($vo["id"]); ?>"><?php echo ($vo["name"]); ?></a></td></tr><?php endforeach; endif; ?>

</table>
</body></html>