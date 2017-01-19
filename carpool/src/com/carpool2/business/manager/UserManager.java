package com.carpool2.business.manager;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.hibernate.Session;
import org.hibernate.Transaction;

import com.carpool2.business.dao.CPInfoDAO;
import com.carpool2.business.dao.UserDAO;
import com.carpool2.business.model.CPInfo;
import com.carpool2.business.model.TcpRecord;
import com.carpool2.business.model.User;
import com.carpool2.web.forms.LoginActionForm;

public class UserManager {
	/**
	 * 添加用户
	 * @param user
	 * @return
	 */
	public boolean addUser(User user) {
		boolean result = false;
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();

		ud.save(user);
	
		result = true;
		tra.commit();
	
		return result;
	}
	/**
	 * 检查用户名与密码是否合法
	 * @param user
	 * @return
	 */
	public boolean checkUser(LoginActionForm user) {
		
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();
		List list = ud.findByUserName(user.getUsername());
		tra.commit();
		if(list.size()> 0){
			
			User userlist = (User)list.get(0);

			if(userlist.getUserPwd().equals(user.getPassword())) {
				return true;
			}
		}
	return false;
	}
	/**
	 * 检查用户名是否可用
	 * @param userName
	 * @return
	 */
	public boolean isNameUsable(String userName) {
		boolean result = false;
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();

		if(ud.findByUserName(userName).size()== 0) {
			
			result = true;
		}
		
		tra.commit();
	
		return result;
	}
	/**
	 * 删除用户
	 * @param user
	 * @return
	 */
	public boolean deleteUser(User user) {
		
		boolean result = false;
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();
		ud.delete(user);
		tra.commit();
		return result;
	}
	/**
	 * 更新用户
	 * @param user
	 * @return
	 */
	public boolean updateUser(User user) {
		boolean result = false;
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();

		ud.update(user);
		result = true;
		tra.commit();
		return result;
	}
	/**
	 * 报名线路
	 * @param username
	 * @param routeID
	 * @return
	 */
	public boolean applyRoute(String userID, String routeID) {
		
		boolean result = false;
			int limit; 	//人数上限
			int applicantNum;	//已报名人数

			CPInfoDAO cid = new CPInfoDAO();
			CPInfo cpi = new CPInfo();
			Session session = cid.getSession();
			Transaction tra = session.beginTransaction();
			List<CPInfo> list = new ArrayList();
			list = cid.findAll();
			Iterator<CPInfo> it = list.iterator();
			while(it.hasNext()){
				cpi = it.next();
				if(cpi.getCpInfId().toString().equals(routeID.toString())){
					break;
				}
			}
			limit = cpi.getCpInfPeopNumb();
			applicantNum = cpi.getCpInfAvilableNum();
			
			int passengerID = limit - applicantNum;
			
			switch(passengerID) {
				
			case 3:
				cpi.setCpRegisterId1(Integer.parseInt(userID));
				break;
			case 2:
				cpi.setCpRegisterId2(Integer.parseInt(userID));
				break;
			case 1:
				cpi.setCpRegisterId3(Integer.parseInt(userID));
				break;
			}
			applicantNum ++;
			cpi.setCpInfAvilableNum(applicantNum);
			cid.update(cpi);
			//System.out.println("beforcommit:"+applicantNum);
			tra.commit();
			//System.out.println("aftercommit:"+applicantNum);
		result = true;
		return result;
	}
	/**
	 * 按用户名查找用户
	 * @param userName
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public User findUserByName(String userName) {
		User user = new User();
		user = null;
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();
		List<User> list = ud.findAll();
		Iterator it = list.iterator();
		int i;
		for(i = 0; i < list.size(); i++) {
			
			user = (User)it.next();
			if (user.getUserName().toString().equals(userName))
				break;
		}
		if(i == list.size()){
			user = null;
		}
		tra.commit();
		return user;
	}
	
	@SuppressWarnings("unchecked")
	public User findUserByID(String userID) {
		User user = new User();
		UserDAO ud = new UserDAO();
		Session session = ud.getSession();
		Transaction tra = session.beginTransaction();
		List<User> list = ud.findAll();
		Iterator<User> it = list.iterator();
		while(it.hasNext()) {
			
			user = (User)it.next();
			if (user.getUserId().toString().equals(userID))
				break;
		}
		tra.commit();
		return user;
	}
	
}
