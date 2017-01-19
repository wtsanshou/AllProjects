package com.carpool2.web.actions;


import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;



public class LogoutAction extends Action{
	public ActionForward execute(ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse response) {
		boolean flag = false;
		try{
			request.getSession().removeAttribute("userinfo");
			request.getSession().removeAttribute("user");
			request.getSession().removeAttribute("usedInfo");
			request.getSession().removeAttribute("published");
			request.getSession().removeAttribute("applied");
			request.getSession().removeAttribute("message");
			flag = true;
		}catch(Exception e){
			e.printStackTrace();
		}
		if(flag == true){
			return mapping.findForward("success");
		}else {
			request.setAttribute("error", "ÍË³öÊ§°Ü");
			return mapping.findForward("fail");
		}
		
	}
}
