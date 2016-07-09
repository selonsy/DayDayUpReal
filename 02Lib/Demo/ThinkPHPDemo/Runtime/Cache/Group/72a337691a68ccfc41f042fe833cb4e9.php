<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
  <title>ThinkPHP示例：独立分组</title>
	<style type="text/css">
	*{ padding: 0; margin: 0;font-size:16px; font-family: "微软雅黑"} 
	div{ padding: 3px 20px;} 
	body{ background: #fff; color: #333;}
	h2{font-size:36px}
	h3{font-size:36px;font-weight:normal}
	a{text-decoration:none; color:#174B73; border-bottom:1px dashed gray}
	a:hover{color:#F60; border-bottom:1px dashed gray}
	div.result{border:1px solid #d4d4d4; background:#FFC;color:#393939; padding:8px 20px;float:auto;margin:2px}
	</style>
 </head>
 <body>
 <div class="main">
 <h2>ThinkPHP示例：独立分组</h2>
 <table cellpadding=2 cellspacing=2>
  <tr>
  <td></td>
	<td>
        <div id="list" >
            <?php if(is_array($list)): $i = 0; $__LIST__ = $list;if( count($__LIST__)==0 ) : echo "" ;else: foreach($__LIST__ as $key=>$vo): $mod = ($i % 2 );++$i;?><div id="div_<?php echo ($vo["id"]); ?>" class="result" style='font-weight:normal;<?php if(($key%2) == "1"): ?>background:#ECECFF<?php endif; ?>'>
            <A href="<?php echo U('read?id='.$vo['id']);?>"><?php echo ($vo["title"]); ?></A>  [<?php echo (date('Y-m-d H:i:s',$vo["create_time"])); ?>]
            </div><?php endforeach; endif; else: echo "" ;endif; ?>
        </div>
		<H3>[ <A href="<?php echo U('Rbac/Public/login');?>">后台管理</a> ]</H3> 
	</td>
  </tr>
 </table>
</div>
 </body>
</html>