<?php
namespace Rbac\Controller;
// 后台用户模块
class UserController extends CommonController {
    function _filter(&$map){
        $map['id'] = array('egt',2);
        if(!empty($_POST['account'])) {
            $map['account'] = array('like',"%".$_POST['account']."%");
        }
    }
    
    public function index() {
        $name = 'User' ;
        $this->_index($name); 
    }

    // 检查帐号
    public function checkAccount() {
        if(!preg_match('/^[a-z]\w{4,}$/i',$_POST['account'])) {
            $this->error( '用户名必须是字母，且5位以上！');
        }
        $User = M("User");
        // 检测用户名是否冲突
        $name  =  $_REQUEST['account'];
        $result  =  $User->getByAccount($name);
        if($result) {
            $this->error('该用户名已经存在！');
        }else {
            $this->success('该用户名可以使用！');
        }
    }

    // 插入数据
    public function insert() {
        // 创建数据对象
        $User	 =	 D("User");
        if(!$User->create()) {
            $this->error($User->getError());
        }else{
            // 写入帐号数据
            if($result	 =	 $User->add()) {
                $this->addRole($result);
                $this->success('用户添加成功！');
            }else{
                $this->error('用户添加失败！');
            }
        }
    }

    protected function addRole($userId) {
        //新增用户自动加入相应权限组
        $RoleUser = M("RoleUser");
        $RoleUser->user_id	=	$userId;
        // 默认加入网站编辑组
        $RoleUser->role_id	=	3;
        $RoleUser->add();
    }

    //重置密码
    public function resetPwd() {
        $id  =  $_POST['id'];
        $password = $_POST['password'];
        if(''== trim($password)) {
            $this->error('密码不能为空！');
        }
        $User = M('User');
        $User->password	=	md5($password);
        $User->id			=	$id;
        $result	=	$User->save();
        if(false !== $result) {
            $this->success("密码修改为$password");
        }else {
            $this->error('重置密码失败！');
        }
    }
      
     
    public function edit() { 
        $name = 'User' ;
        $this->_edit($name); 
    }
    
    public function update() { 
        $name = 'User' ;
        $this->_update($name); 
    }
    
    public function delete() { 
        $name = 'User' ;
        $this->_delete($name); 
    }  
    
    public function foreverdelete() { 
        $name = 'User' ;
        $this->_foreverdelete($name); 
    }  
    
    public function clear() { 
        $name = 'User' ;
        $this->_clear($name); 
    }  
    
    public function forbid() { 
        $name = 'User' ;
        $this->_forbid($name); 
    }  
    
    public function checkPass() { 
        $name = 'User' ;
        $this->_checkPass($name); 
    } 
    
    public function recycle() { 
        $name = 'User' ;
        $this->_recycle($name); 
    }  
    
    public function recycleBin() { 
        $name = 'User' ;
        $this->_recycleBin($name); 
    } 
    
    public function resume() { 
        $name = 'User' ;
        $this->_resume($name); 
    } 
    
    public function saveSort() { 
        $name = 'User' ;
        $this->_saveSort($name); 
    } 
    
}