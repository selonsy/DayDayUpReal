<?php
namespace Home\Controller;
use Think\Controller;
class BoardController extends Controller {

	public function index(){
		  $m = M("board");
		  $msg = $m->where()->select();
		  $this->assign('board',$msg);
		  $this->display();
		}

		
	public function detail($id){
		$m= M("board");
		$id = intval($id);
		$board = $m->where("id = $id")->select();
		$this->assign('board',$board[0]);
		$n = M("post");
		$count = $n->where("board = $id")->count();
		$page = new \Think\Page($count,8);
		$show = $page->show();
		$post = $n->where("board = $id")->limit(
			$page->firstRow.','.$page->listRows)->select();
		$this->assign('post',$post);
		$this->assign('page',$show);
		$this->display();
		
	}	
		
		
		
		
		
		
	public function xinpost(){
	    $m = M("post");
		$data = array();
		$data[] = array('board' => 1,'text'=>'辛星PHP，值得关注','author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'小倩说过，我是好人','author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'不轻易相信','author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'不管前途多么艰险','author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'上单蛮王，打爆一切','author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'鳄鱼打爆一切','author'=>'xin@xin.com');
		$data[] = array('board' => 1,'text'=>'诺手不服，求solo','author'=>'xin@xin.com');
		$m->addAll($data);
		echo "导入完毕";
	
	}	
		
		

    public function xinadd(){
		$m = M("board");
		$data = array();
		$data[] = array('name' => 'PHP');
		$data[] = array('name' => 'Python');
		$data[] = array('name' => 'Java');
		$data[] = array('name' => 'C');
		$data[] = array('name' => 'C++');
		$data[] = array('name' => 'Perl');
		$data[] = array('name' => 'Ruby');
		$data[] = array('name' => 'HTML');
		$m->addAll($data);
		echo "导入完毕";
	}
}