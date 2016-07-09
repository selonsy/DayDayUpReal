<?php
namespace Home\Controller;
use Think\Controller;
class UserController extends Controller {
    public function index(){
    }
	public function login(){
	$this->display();
	}
	public function checklog(){
		if(isset($_POST['email'])){ 
			$name = $_POST['email'];
		}
		$pwd = md5($_POST['pwd']);
		$m = M("user");
		$msg = $m->where("name = '$name'")->find();
		if($msg['pwd'] == $pwd){
			cookie("username",$name);
			$this->success("登录成功",__APP__."/Home/Board/index");
			//redirect(__APP__."/Home/Board/index",2,'登录成功');
		}else{
			$this->error("用户名或密码错误");
		}
				
	}
	
	public function reg(){
		$this->display();
	}
	public function checkreg(){
			$data['name'] = $_POST['email'];
			$data['pwd'] = md5($_POST['pwd']);
			$m = M("user");
			$msg = $m->create($data);
			$result = $m->add();
			if($result == true){
				cookie("username",$data['name']);
				$this->success("注册成功",__APP__."/Home/Board/index");
			}else{
				$this->error("注册失败,用户名已被占用");
			}
	}
	
	
	public function logout(){
		cookie("username",null);
		$this->success("注销成功");
	}
	
	
	
	
}