<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html lang="zh-cn">
<head>
<title>2014年辛星简易BBS登录页面</title>
<link href="/thinkbbs/Public/CSS/xinxing.css" type="text/css" rel="stylesheet"/>
<link href = "/thinkbbs/Public/CSS/signin.css" rel = "stylesheet">
</head><body>
<form   action = "/thinkbbs/Home/User/checkreg"  method="post"  class="form-signin" role="form">
<h2 class="form-signin-heading">辛星简易BBS</h2>
<input name = "email"type="email" class="form-control" placeholder="账号"  autofocus>
<input name = "xin" type = "hidden" />
<input name = "pwd"  type="password" class="form-control" placeholder="密码" >
<button class="btn btn-lg btn-primary btn-block" type="submit">注册</button></form>
</body></html>