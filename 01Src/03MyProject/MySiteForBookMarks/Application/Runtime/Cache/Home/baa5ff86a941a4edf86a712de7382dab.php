<?php if (!defined('THINK_PATH')) exit();?>﻿<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>晨敛清泉</title>
	<link rel="stylesheet" href="/Public/css/bookmark.css"/>
</head>	
<body oncontextmenu='return false' ondragstart='return false'>
<div id="wrapper">
	<div class="skinBg" style="background-image: url('/Public/img/bookmark/65.jpg');"></div>
	<header id="header">
		<div class="weather fl">
			<span class="cityW">
				<span>广州：</span>
				<span>
					<span class="weatherIcon wI1"></span>
					<span>23 ~ 31℃</span>
				</span>
			</span>
			<span class="sp">|</span>
			<span class="pollution">
				<span>空气质量：83</span>
				<span class="polutionLevel">良</span>
			</span>
			<div class="cityWeather">
				
			</div>
		</div>
		<nav class="headNavs fr tr">
			<a href="#">
				<!--<span class="s-icon s-icon-treasure"></span>-->
				<span>注册</span>
			</a>
			<a href="#">
				<!--<span class="s-icon s-icon-skin"></span>-->
				<span>登录</span>
			</a>
			<!--<a href="#"><span class="s-icon s-icon-treasure"></span><span>宝箱</span></a>-->
			<!--<a href="#"><span class="s-icon s-icon-skin"></span><span>换肤</span></a>-->
			<!--<a href="#"><span class="s-icon s-icon-msg"></span><span>消息</span></a>-->
			<!--<a><span class="s-icon s-icon-line"></span></a>-->
			<!--<a href="#"><span>设为首页</span></a>-->
			<!--<a href="javascript:;" class="uname"><span>模板网</span><span class="user-arrow"></span></a>-->
			<!--<div class="topMenus dn">-->
				<!--<span class="arrowTop"></span>-->
				<!--<a href="#">个人中心</a>-->
				<!--<a href="#">帐号设置</a>-->
				<!--<a href="#">搜索设置</a>-->
				<!--<a href="#">意见反馈</a>-->
				<!--<a href="#">首页教程</a>-->
				<!--<a href="#">安全退出</a>-->
			<!--</div>-->
		</nav>
	</header>
	<div class="content tc">
		<p class="logo"><img width="270" height="129" src="/Public/img/bookmark/logo_white.png" alt="logo" /></p>
		<!--<nav class="mainNavs">-->
			<!--<a href="#">新闻</a>-->
			<!--<a href="#">网页</a>-->
			<!--<a href="#">贴吧</a>-->
			<!--<a href="#">知道</a>-->
			<!--<a href="#">音乐</a>-->
			<!--<a href="#">图片</a>-->
			<!--<a href="#">视频</a>-->
			<!--<a href="#">地图</a>-->
			<!--<a href="#">百科</a>-->
			<!--<a href="#">文库</a>-->
			<!--<a href="#">更多&gt;&gt;</a>-->
		<!--</nav>-->
		<div class="searchBox">
			<form action="">
				<input type="text" class="searchIpt f14" name="wd" maxlength="100" autocomplete="off"/>
				<input type="button" class="btn cp searchBTN" for="baidu" id="btnSearchBD" value="{{txtBaidu}}" />
				<input type="button" class="btn cp searchBTN" for="google" value="谷歌一下" />
			</form>
		</div>
		<div class="mainContents oh">
			<div class="menusWrapper fl">
				<a class="active" href="javascript:;">导航</a>
				<a href="javascript:;">新闻</a>
				<a href="javascript:;">音乐</a>
				<a href="javascript:;">游戏</a>
			</div>
			<div class="ctnerWrapper">
				<div class="ctnerBox">
					<div id="cbox-1" class="cbox tl dn">
						<div class="ctnerTab pr tc">
							<a href="#" class="on">常用类</a>
							<a href="#">工作类</a>
							<a href="#">开发类</a>
							<a href="#">工具类</a>
							<a href="#">娱乐类</a>
						</div>
						<div class="Navs rtNavs dn pt15 db">
							<div class="navTitle fl">常用类导航</div>
							<div class="navArea oh">
								<a href="http://www.taobao.com" target="_blank" title="淘宝网"><img width="115" height="70" src="/Public/img/bookmark/101.png" alt="" /></a>
								<a href="http://www.jd.com" target="_blank" title="京东商城"><img width="115" height="70" src="/Public/img/bookmark/102.png" alt="" /></a>
								<a href="http://www.vip.com" target="_blank" title="唯品会"><img width="115" height="70" src="/Public/img/bookmark/115.png" alt="" /></a>
								<a href="http://www.ctrip.com" target="_blank" title="携程"><img width="115" height="70" src="/Public/img/bookmark/116.png" alt="" /></a>
								<a href="http://www.youku.com" target="_blank" title="优酷"><img width="115" height="70" src="/Public/img/bookmark/106.png" alt="" /></a>
							</div>
						</div>
						<div class="Navs rtNavs dn pt15">
							<div class="navTitle fl">工作类导航</div>
							<div class="navArea oh">
								<a href="http://www.taobao.com" target="_blank" title="淘宝网"><img width="115" height="70" src="/Public/img/bookmark/101.png" alt="" /></a>
								<a href="http://www.jd.com" target="_blank" title="京东商城"><img width="115" height="70" src="/Public/img/bookmark/102.png" alt="" /></a>
							</div>
						</div>
						<div class="Navs rtNavs dn pt15">
							<div class="navTitle fl">开发类导航</div>
							<div class="navArea oh">
								<a href="http://www.taobao.com" target="_blank" title="淘宝网"><img width="115" height="70" src="/Public/img/bookmark/101.png" alt="" /></a>
								<a href="http://www.jd.com" target="_blank" title="京东商城"><img width="115" height="70" src="/Public/img/bookmark/102.png" alt="" /></a>
							</div>
						</div>
						<div class="Navs rtNavs dn pt15">
							<div class="navTitle fl">工具类导航</div>
							<div class="navArea oh">
								<a href="http://www.taobao.com" target="_blank" title="淘宝网"><img width="115" height="70" src="/Public/img/bookmark/101.png" alt="" /></a>
								<a href="http://www.jd.com" target="_blank" title="京东商城"><img width="115" height="70" src="/Public/img/bookmark/102.png" alt="" /></a>
							</div>
						</div>
						<div class="Navs rtNavs dn pt15">
							<div class="navTitle fl">娱乐类导航</div>
							<div class="navArea oh">
								<a href="http://www.taobao.com" target="_blank" title="淘宝网"><img width="115" height="70" src="/Public/img/bookmark/101.png" alt="" /></a>
								<a href="http://www.jd.com" target="_blank" title="京东商城"><img width="115" height="70" src="/Public/img/bookmark/102.png" alt="" /></a>
							</div>
						</div>
					</div>
					<div id="cbox-2" class="cbox tl dn">
						<div class="newsBox oh">
							<div class="sliderBox fl">
								<div class="slider">
									<a href="#" target="_blank" class="picLink active">
										<img width="425" height="260" src="/Public/img/bookmark/news2.jpg" alt="" />
										<span href="#" target="_blank" class="picTitle">颤抖吧骚年</span>
									</a>
								</div>
								<div class="smallPics">
									<a class="active"><img width="68" height="40" src="/Public/img/bookmark/news2.jpg" alt="" /></a>
								</div>
							</div>
							<div class="topic">
								<div class="topicTop">
									<span class="titleWords titleT on"><a href="javascript:;">搜索风云榜</a></span>
									<span class="titleTopic titleT"><a href="javascript:;">热门话题</a></span>
									<a class="changeWords" href="javascript:;">换一换</a>
								</div>
								<div class="topicList">
									<div class="wBox topicB dn">
										<ul>
											<li><a target="_blank" href="#">风云11111</a></li>
											<li><a target="_blank" href="#">风云22222</a></li>
											<li><a target="_blank" href="#">风云33333</a></li>
											<li><a target="_blank" href="#">风云44444</a></li>
											<li><a target="_blank" href="#">风云55555</a></li>
											<li><a target="_blank" href="#">风云66666</a></li>
											<li><a target="_blank" href="#">风云77777</a></li>
											<li><a target="_blank" href="#">风云88888</a></li>
											<li><a target="_blank" href="#">风云99999</a></li>
											<li><a target="_blank" href="#">风云11111</a></li>
											<li><a target="_blank" href="#">风云22222</a></li>
											<li><a target="_blank" href="#">风云33333</a></li>
											<li><a target="_blank" href="#">风云44444</a></li>
											<li><a target="_blank" href="#">风云55555</a></li>
											<li><a target="_blank" href="#">风云66666</a></li>
											<li><a target="_blank" href="#">风云77777</a></li>
										</ul>
									</div>
									<div class="tBox topicB dn">
										<ul>
											<li><a target="_blank" href="#">话题11111</a></li>
											<li><a target="_blank" href="#">话题22222</a></li>
											<li><a target="_blank" href="#">话题33333</a></li>
											<li><a target="_blank" href="#">话题44444</a></li>
											<li><a target="_blank" href="#">话题55555</a></li>
											<li><a target="_blank" href="#">话题66666</a></li>
											<li><a target="_blank" href="#">话题77777</a></li>
											<li><a target="_blank" href="#">话题88888</a></li>
											<li><a target="_blank" href="#">话题99999</a></li>
											<li><a target="_blank" href="#">话题11111</a></li>
											<li><a target="_blank" href="#">话题22222</a></li>
											<li><a target="_blank" href="#">话题33333</a></li>
											<li><a target="_blank" href="#">话题44444</a></li>
											<li><a target="_blank" href="#">话题55555</a></li>
											<li><a target="_blank" href="#">话题66666</a></li>
											<li><a target="_blank" href="#">话题77777</a></li>
										</ul>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div id="cbox-3" class="cbox tl dn">
						<p class="pt15 tc f14">不要急骚年，在开发中了~~</p>
					</div>
					<div id="cbox-4" class="cbox tl dn">
						<p class="pt15 tc f14">说了不要急，还点~~</p>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<footer id="footer" class="footer">©2016 selonsy 使用百度前必读 粤ICP证9527号</footer>
<script src="/Public/js/seajs/sea.js"></script>
<script src="/Public/js/seajs/sea.config.js"></script>
<script type="text/javascript">
    seajs.use('self/bookmark', function (bookmark) {
        bookmark.init();
    });
</script>
</body>
</html>