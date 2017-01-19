package com.carpool2.web.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.carpool2.business.manager.UserManager;
import com.carpool2.business.model.User;
import com.carpool2.web.forms.UpdateUserActionForm;

public class UpdateUserAction extends Action {

	/* (non-Javadoc)
	 * @see org.apache.struts.action.Action#execute(org.apache.struts.action.ActionMapping, org.apache.struts.action.ActionForm, javax.servlet.http.HttpServletRequest, javax.servlet.http.HttpServletResponse)
	 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		UpdateUserActionForm uaf = (UpdateUserActionForm)form;
		User user = new User();
		UserManager um = new UserManager();
		user = um.findUserByName(uaf.getUsername());
		if(uaf.getNickname().length()> 1) {
			user.setUserNickname(uaf.getNickname());
		}
		if(uaf.getEmail().length()> 1){
			user.setUserEmail(uaf.getEmail());
		}
		if(uaf.getIntroduction().length() > 1) {
			user.setUserIndr(uaf.getIntroduction());
		}
		if(uaf.getPassword().length()> 1){
			user.setUserPwd(uaf.getPassword());
		}
		if(uaf.getPhone().length() > 1){
			user.setUserContact(uaf.getPhone());
		}
		if(uaf.getProfile().length() > 1) {
			user.setUserProfile(uaf.getProfile());
		}
		user.setUserSex(uaf.getSex());

		boolean result = false;
		try{
			result = um.updateUser(user);
			user = um.findUserByName(uaf.getUsername());
		}catch(Exception e){
			e.printStackTrace();
		}
		if(result == true){
			request.getSession().setAttribute("userinfo", user);
			return mapping.findForward("success");	
		}
		request.setAttribute("error", "用户信息修改失败！");
		return mapping.findForward("fail");
		//return super.execute(mapping, form, request, response);
	}
}
