<?php
namespace Home\Controller;
use Think\Controller;
class PostController extends Controller {
    public function index($id){
		$m = M("post");
		$owner = $m->where("own = 0 and id = $id")->find();
		$this->assign('owner',$owner);
		$count = $m->where("own = $id")->count();
		$page = new \Think\Page($count,8);
		$show = $page->show();
		$post = $m->where("own = $id")->limit(
			$page->firstRow.','.$page->listRows)->select();
		$this->assign('post',$post);
		$this->assign('page',$show);
		$this->display();
	}
	
	
	
	
	
	
	public function add(){
		$m = M("post");
		$data = array();
	    $data['own']  = isset($_POST['alone'])?0:$_POST['own'];
		$data['board'] = $_POST['board'];
		$data['text'] = $_POST['text'];
		if(cookie('username')){ 
			$data['author'] = cookie('username');
		}else{
			$data['author'] = '匿名';
		}
		$m->create($data);
		$re = $m->add();
		if($re){
			$this->success("发表成功");
		}else{
			$this->error("发表错误");
		}
		
		
	}
	
	public function addpost($id){
		$m = M("post");
		$data = array();
		$data[] = array('board' => 1,'text'=>'辛星PHP，值得关注',
		'own'=>$id,'author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'小倩说过，我是好人',
		'own'=>$id,'author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'不轻易相信',
		'own'=>$id,'author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'不管前途多么艰险',
		'own'=>$id,'author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'上单蛮王，打爆一切',
		'own'=>$id,'author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'鳄鱼打爆一切',
		'own'=>$id,'author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'诺手不服，求solo',
		'own'=>$id,'author'=>'xin@xin.com');
		$m->addAll($data);
		echo "导入完毕";
	}
	
	
}