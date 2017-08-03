package com.yaitza.pro.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.Map;

/**
 * Created by yangzhijie on 2017/8/3.
 */
@Controller
public class HomeController {
    @RequestMapping("/index")
    public String index(Map<String, Object> map){
        map.put("model", "Fuck this world!");
        return "/index";
    }
}
