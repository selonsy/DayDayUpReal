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
<tr><td>楼主</td><td><?php echo ($owner["text"]); ?></td><td><?php echo ($owner["author"]); ?></td></tr>
<?php if(is_array($post)): foreach($post as $key=>$vo): ?><tr><td>第<?php echo ($key); ?>楼</td>
<td><?php echo ($vo["text"]); ?></td><td><?php echo ($vo["author"]); ?></td></tr><?php endforeach; endif; ?>
</table>
<?php echo ($page); ?>

<form method = "post" action ="/thinkbbs/Home/Post/add" role="form">
      <input type="text" class="form-control" id="name" 
	  name = "text"  placeholder="请输入内容">
	  <input type = "hidden" name = "board" 
	  value = "<?php echo $owner['board'] ?>" />
	  <input type = "hidden" name = "own" 
	  value = "<?php echo $owner['id'] ?>" />
   <div class="checkbox">
      <label>
      <input name = "alone" type="checkbox"> 是否作为主题帖
      </label>
   </div>
   <button type="submit" class="btn btn-default">发表</button>
</form>

</body></html>