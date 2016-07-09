<?php
namespace Rbac\Controller;
class NodeController extends CommonController {
    public function _filter(&$map){
        if(!empty($_GET['group_id'])) {
            $map['group_id'] =  $_GET['group_id'];
            $this->assign('nodeName','分组');
        }elseif(empty($_POST['search']) && !isset($map['pid']) ) {
            $map['pid']	=	0;
        }
        if($_GET['pid']!=''){
            $map['pid']=$_GET['pid'];
        }
        $_SESSION['currentNodeId']	=	$map['pid'];
        //获取上级节点
        $node  = M("Node");
        if(isset($map['pid'])) {
            if($node->getById($map['pid'])) {
                $this->assign('level',$node->level+1);
                $this->assign('nodeName',$node->name);
            }else {
                $this->assign('level',1);
            }
        }
    }

    public function index() {
        $name = 'Node' ;
        $this->_index($name); 
    }
    
    public function _before_index() {
        $model	=	M("Group");
        $list	=	$model->where('status=1')->getField('id,title');
        $this->assign('groupList',$list);
    }

    // 获取配置类型
    public function _before_add() {
        $model	=	M("Group");
        $list	=	$model->where('status=1')->select();
        $this->assign('list',$list);
        $node	=	M("Node");
        $node->getById($_SESSION['currentNodeId']);
        $this->assign('pid',$node->id);
        $this->assign('level',$node->level+1);
    }

    public function _before_patch() {
        $model	=	M("Group");
        $list	=	$model->where('status=1')->select();
        $this->assign('list',$list);
        $node	=	M("Node");
        $node->getById($_SESSION['currentNodeId']);
        $this->assign('pid',$node->id);
        $this->assign('level',$node->level+1);
    }
    public function _before_edit() {
        $model	=	M("Group");
        $list	=	$model->where('status=1')->select();
        $this->assign('list',$list);
    }

    /**
     +----------------------------------------------------------
     * 默认排序操作
     +----------------------------------------------------------
     * @access public
     +----------------------------------------------------------
     * @return void
     +----------------------------------------------------------
     */
    public function sort()
    {
        $node = M('Node');
        if(!empty($_GET['sortId'])) {
            $map = array();
            $map['status'] = 1;
            $map['id']   = array('in',$_GET['sortId']);
            $sortList   =   $node->where($map)->order('sort asc')->select();
        }else{
            if(!empty($_GET['pid'])) {
                $pid  = $_GET['pid'];
            }else {
                $pid  = $_SESSION['currentNodeId'];
            }
            if($node->getById($pid)) {
                $level   =  $node->level+1;
            }else {
                $level   =  1;
            }
            $this->assign('level',$level);
            $sortList   =   $node->where('status=1 and pid='.$pid.' and level='.$level)->order('sort asc')->select();
        }
        $this->assign("sortList",$sortList);
        $this->display();
        return ;
    }
    
        
    public function insert() {
        $name = 'Node' ;
        $this->_insert($name); 
    }
    
    public function edit() { 
        $name = 'Node' ;
        $this->_edit($name); 
    }
    
    public function update() { 
        $name = 'Node' ;
        $this->_update($name); 
    }
    
    public function delete() { 
        $name = 'Node' ;
        $this->_delete($name); 
    }  
    
    public function foreverdelete() { 
        $name = 'Node' ;
        $this->_foreverdelete($name); 
    }  
    
    public function clear() { 
        $name = 'Node' ;
        $this->_clear($name); 
    }  
    
    public function forbid() { 
        $name = 'Node' ;
        $this->_forbid($name); 
    }  
    
    public function checkPass() { 
        $name = 'Node' ;
        $this->_checkPass($name); 
    } 
    
    public function recycle() { 
        $name = 'Node' ;
        $this->_recycle($name); 
    }  
    
    public function recycleBin() { 
        $name = 'Node' ;
        $this->_recycleBin($name); 
    } 
    
    public function resume() { 
        $name = 'Node' ;
        $this->_resume($name); 
    } 
    
    public function saveSort() { 
        $name = 'Node' ;
        $this->_saveSort($name); 
    } 
    
}