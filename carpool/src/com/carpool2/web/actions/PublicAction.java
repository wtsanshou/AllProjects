package com.carpool2.web.actions;

import java.util.Date;

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
import com.carpool2.web.forms.PublicActionForm;

public class PublicAction extends Action {

	/**
	 * 发布拼车线路
	 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		boolean flag = false;
		PublicActionForm paf = (PublicActionForm)form;
		CPInfo info = new CPInfo();
		UserManager um = new UserManager();
		User user = new User();
		try{
			user = um.findUserByName(paf.getUserName());
			info.setCpInfAvilableNum(0);
			info.setCpInfDepartTime((paf.getDate()+ " " + paf.getHour() + ":" + paf.getMinute()).toString());
			String scord = paf.getScord().substring(1, paf.getScord().length()-1);
			String ecord = paf.getEcord().substring(1, paf.getEcord().length()-1);
			info.setCpInfEcoordinate(ecord);
			info.setCpInfEnd(paf.getEnd());
			info.setCpInfPeopNumb(paf.getLimit());
			Date date = new Date();
			info.setCpInfPublishTime(new java.sql.Date(date.getTime()));
			info.setCpInfRemark(paf.getRemark());
			info.setCpInfScoordinate(scord);
			info.setCpInfSex(paf.getSex());
			info.setCpInfStart(paf.getStart());
			info.setCpInfStatus("等待报名");
			info.setCpInfFrequency(paf.getFrequency());
			info.setDel(0);
			info.setCpRegisterMainId(user.getUserId());
			info.setCpRegisterId1(0);
			info.setCpRegisterId2(0);
			info.setCpRegisterId3(0);
			CPInfoManager cim = new CPInfoManager();
			
			if(cim.addInfo(info)) {
				flag = true;
				
			}
		}catch(Exception e){
			e.printStackTrace();
		}
		if(flag == true){
			request.getSession().setAttribute("published",info);
			return mapping.findForward("success");	
		}else {
			request.setAttribute("error", "线路发布失败！");
			return mapping.findForward("fail");
		}
		
		//return super.execute(mapping, form, request, response);
	}
	
}
