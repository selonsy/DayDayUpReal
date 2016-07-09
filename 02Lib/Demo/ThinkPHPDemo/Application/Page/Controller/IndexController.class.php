<?php
namespace Page\Controller;
use Think\Controller; 
class IndexController extends Controller { 

    public function index() {
        $Form   =   M('Form');
        $count  = $Form->count();    //计算总数 
        $Page = new \Think\Page($count, 5); 
        //列表值
        $list   = $Form->order('id desc')->limit($Page->firstRow. ',' . $Page->listRows)->select(); 
        $show       = $Page->show();// 分页显示输出     
        //输出显示        
        $this->assign('list',$list);// 赋值数据集
        $this->assign('page',$show);// 赋值分页输出
        $this->display(); // 输出模板       
        
    }
}