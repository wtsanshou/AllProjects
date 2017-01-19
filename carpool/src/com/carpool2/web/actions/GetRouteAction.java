package com.carpool2.web.actions;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.carpool2.business.manager.CPInfoManager;
import com.carpool2.business.model.CPInfo;

public class GetRouteAction extends Action {

/**
 * ����ƴ����·
 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		boolean flag = false;
		try{	
			//System.out.println(request.getParameter("scord").toString());
			
			String startCoordinate = request.getParameter("scord").toString().substring(1, request.getParameter("scord").toString().length()-2);
			String endCoordinate = request.getParameter("ecord").toString().substring(1, request.getParameter("ecord").toString().length()-2);

			CPInfoManager cim = new CPInfoManager();

			List<CPInfo> list = cim.findByCoordinate(startCoordinate, endCoordinate);
			if(list.size() < 1){
				request.setAttribute("emptyList", "δ�����������Ϣ.");
			}
			request.setAttribute("infoList", list);
			flag = true;
			
		}catch(Exception e){
			e.printStackTrace();
		}
		if(flag == true){
		
			return mapping.findForward("success");
		}else {
			request.setAttribute("error", "����ʧ��");
			return mapping.findForward("fail");
		}
	}
}
