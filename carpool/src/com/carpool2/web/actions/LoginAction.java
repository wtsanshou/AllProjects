/**
 * 
 */
package com.carpool2.web.actions;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.carpool2.business.manager.CPInfoManager;
import com.carpool2.business.manager.TcpRecordManager;
import com.carpool2.business.manager.UserManager;
import com.carpool2.business.model.CPInfo;
import com.carpool2.business.model.TcpRecord;
import com.carpool2.business.model.User;
import com.carpool2.web.forms.LoginActionForm;


/**
 * 用户登录Action
 *
 */
public class LoginAction extends Action {

/**
 * 用户登录
 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		boolean flag = false;
		LoginActionForm laf = (LoginActionForm)form;

		UserManager um = new UserManager();
		try{
			if(um.checkUser(laf)) {	
				CPInfoManager cim = new CPInfoManager();
				User userinfo = new User();
				CPInfo published = new CPInfo();
				CPInfo applied = new CPInfo();
				TcpRecordManager trm = new TcpRecordManager();
				TcpRecord td = new TcpRecord();
	
				
				List<CPInfo> usedInfoList = cim.findUsedInfoByUserName(laf.getUsername());
				List<CPInfo> list = cim.findLatest(20);
				int lenOfList = list.size();
				String len[] = new String[lenOfList];
			
				for(int i = 0; i<(lenOfList/5); i++){
				
					len[i] = ""+(i+1);
				}
				List<CPInfo> sublist1 = list.subList(0, 5);
				List<CPInfo> sublist2 = list.subList(5, 10);
				List<CPInfo> sublist3 = list.subList(10, 15);
				List<CPInfo> sublist4 = list.subList(15,lenOfList);
				
				applied = cim.findAppliedByUserName(laf.getUsername());
				published = cim.findPublishedByUserName(laf.getUsername());
				
				userinfo = um.findUserByName(laf.getUsername());
				td = trm.getLatestByUserID(userinfo.getUserId());
		
				userinfo = um.findUserByName(laf.getUsername());
				if(td != null){
					request.getSession().setAttribute("news", "您有新的消息！");
				}
			
				request.getSession().setAttribute("message", td);
				request.getSession().setAttribute("user", laf.getUsername());
				request.getSession().setAttribute("t1", sublist1);
				request.getSession().setAttribute("t2", sublist2);
				request.getSession().setAttribute("t3", sublist3);
				request.getSession().setAttribute("t4", sublist4);
				request.getSession().setAttribute("len", len);
				request.getSession().setAttribute("userinfo", userinfo);
				request.getSession().setAttribute("usedInfo", usedInfoList);
				request.getSession().setAttribute("published", published);
				request.getSession().setAttribute("applied", applied);
				flag = true;
			}
		}catch(Exception e){
			e.printStackTrace();
		}
		if(flag == true){
			return mapping.findForward("success");	
		}else {
			request.setAttribute("error", "密码错误，请重新登录！");
			return mapping.findForward("fail");
		}
		
		//return super.execute(mapping, form, request, response);
	}
}
