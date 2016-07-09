<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <title>ThinkPHP示例：URL路由</title>
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
            <h2>ThinkPHP示例之：URL路由</h2>
            <table  cellpadding=3 cellspacing=3>
                <tr>
                    <td>
                        <div class="result">
                            当前URL路由到控制器 <span style="color:red"><?php echo (CONTROLLER_NAME); ?></span> 的<span style="color:red"><?php echo (ACTION_NAME); ?></span> 操作<br/>
                            参数:<br/>
                            <?php if(is_array($vars)): $i = 0; $__LIST__ = $vars;if( count($__LIST__)==0 ) : echo "" ;else: foreach($__LIST__ as $key=>$v): $mod = ($i % 2 );++$i; echo ($key); ?>=><?php echo ($v); ?><br/><?php endforeach; endif; else: echo "" ;endif; ?>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tLeft" >
                        路由1：<a href="<?php echo U('/Route/blog/curd');?>">blog/curd</a><br/>
                        路由2：<a href="<?php echo U('/Route/blog/5');?>">blog/5</a><br/>
                        路由3：<a href="<?php echo U('/Route/blog/2012/09');?>">blog/2012/09</a><br/>
                        路由4：<a href="<?php echo U('/Route/100');?>">100</a><br/>
                    </td>
                </tr>
            </table>
        </div>
    </body>
</html>