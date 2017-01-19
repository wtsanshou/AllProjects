package com.carpool2.web.actions;

import java.util.Date;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.carpool2.business.manager.CPInfoManager;
import com.carpool2.business.manager.UserManager;
import com.carpool2.business.model.CPInfo;
import com.carpool2.business.model.User;
import com.carpool2.web.forms.AddUserActionForm;

public class AddUserAciton extends Action {
/**
 * 添加用户
 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		AddUserActionForm aaf = (AddUserActionForm)form;
		User user = new User();
		UserManager um = new UserManager();
		if (um.findUserByName(aaf.getUn())!= null) {
			request.setAttribute("error", "用户名已存在");
			return mapping.findForward("fail");
		}
		user.setDel(0);
		user.setUserBadTimes(0);
		user.setUserContact(aaf.getPhone());
		user.setUserEmail(aaf.getEmail());
		user.setUserGoodTimes(0);
		user.setUserIndr(null);
		user.setUserLevel(0);
		user.setUserLoginTimes(0);
		user.setUserName(aaf.getUn());
		user.setUserNickname(aaf.getNickname());
		user.setUserProfile("../images/w/hn04.gif");
		user.setUserPwd(aaf.getPw());
		Date date = new Date();
		user.setUserRegTime(new java.sql.Date(date.getTime()));
		user.setUserSex(aaf.getSex());
		user.setUserSuccessTimes(0);
		
		CPInfoManager cim = new CPInfoManager();
		List<CPInfo> list = cim.findLatest(20);
		boolean result = false;
		try{
			result = um.addUser(user);

		}catch(Exception e){
			e.printStackTrace();
		}
		if(result == true){
			request.getSession().setAttribute("latest", list);
			request.getSession().setAttribute("userinfo", user);
			request.getSession().setAttribute("user", aaf.getUn());
			return mapping.findForward("success");	
		}
		request.setAttribute("error", "注册失败");
		return mapping.findForward("fail");
		//return super.execute(mapping, form, request, response);
	}	

}
