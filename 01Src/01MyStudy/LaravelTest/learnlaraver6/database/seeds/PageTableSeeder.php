<?php
/**
 * Created by PhpStorm.
 * User: Administrator
 * Date: 2016/3/5
 * Time: 14:45
 */


use Illuminate\Database\seeder;
use App\Page;

class PageTableSeeder extends Seeder
{
    public function run()
    {
        DB::table('pages')->delete();

        for($i=0;$i<10;$i++)
        {
            Page::create([
                'title'=>'Title'.$i,
                'slug'=>'first-page',
                'body'=>'Body'.$i,
                'user_id'=>1,
            ]);
        }
    }
}