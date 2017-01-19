package com.carpool2.business.manager;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.hibernate.Session;
import org.hibernate.Transaction;

import com.carpool2.business.dao.CPInfoDAO;
import com.carpool2.business.model.CPInfo;

public class CPInfoManager {
	/**
	 * 搜索线路
	 * @param coordinateX
	 * @param coordinateY
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public List<CPInfo> findByCoordinate(String startCoordinate, String endCoordinate) {
		
		Dist dist = new Dist();
		CPInfoDAO cid = new CPInfoDAO();
		Session session = cid.getSession();
		Transaction tra = session.beginTransaction();
		
		Double startX = Double.parseDouble(startCoordinate.split(",")[0]);	//起点X坐标
		Double startY = Double.parseDouble(startCoordinate.split(",")[1]);	//起点Y坐标
		Double endX = Double.parseDouble(endCoordinate.split(",")[0]);	//终点X坐标
		Double endY = Double.parseDouble(endCoordinate.split(",")[1]);	//终点Y坐标
		CPInfo cpi = new CPInfo();
		List<CPInfo> resultList = new ArrayList();
		List<CPInfo> list = cid.findAll();
		Iterator<CPInfo> iterator = list.iterator();
		int range = 1;
		while(iterator.hasNext()) {
			cpi = (CPInfo)iterator.next();
			Double routeStartX = Double.parseDouble(cpi.getCpInfScoordinate().split(",")[0]);
			Double routeStartY = Double.parseDouble(cpi.getCpInfScoordinate().split(",")[1]);
			Double routeEndX = Double.parseDouble(cpi.getCpInfEcoordinate().split(",")[0]);
			Double routeEndY = Double.parseDouble(cpi.getCpInfEcoordinate().split(",")[1]);
			
			if(((dist.getDistance(startX, routeStartX, startY, routeStartY)) < range)
				&&((dist.getDistance(endX, routeEndX, endY, routeEndY)) < range)){
				
				try{
					resultList.add(cpi);
				}catch(Exception e){
					e.printStackTrace();
				}
			}
		}
		tra.commit();
	
		return resultList;
	}
	/**
	 * 添加拼车信息
	 * @param info
	 * @return
	 */
	public boolean addInfo(CPInfo info) {
		boolean result = false;
		CPInfoDAO cid = new CPInfoDAO();
		Session session = cid.getSession();
		Transaction tra = session.beginTransaction();

		cid.save(info);
		result = true;
		tra.commit();
		return result;
	}
	/**
	 * 更新拼车信息
	 * @param cpi
	 * @return
	 */
	public boolean updateInfo(CPInfo cpi) {
		boolean result = false;
		CPInfoDAO cid = new CPInfoDAO();
		Session session = cid.getSession();
		Transaction tra = session.beginTransaction();

		cid.update(cpi);
		result = true;
		tra.commit();
		return result;
	}

	/**
	 * 
	 * @param startCoordinate
	 * @param endCoordinate
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public CPInfo findByID(String routeID) {
		
		CPInfoDAO cid = new CPInfoDAO();
		Session session = cid.getSession();
		Transaction tra = session.beginTransaction();
		CPInfo cpi = new CPInfo();
		List<CPInfo> list = new ArrayList();
		list = cid.findAll();
		Iterator it = list.iterator();
		while(it.hasNext()){
			cpi = (CPInfo) it.next();
			if(cpi.getCpInfId().toString().equals(routeID.toString())){
				break;
			}
		}
		
		
		tra.commit();
	
		return cpi;
	}
	/**
	 * 获取最新n条信息
	 * @param n
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public List<CPInfo> findLatest(int n) {
		List<CPInfo> resultlist = new ArrayList<CPInfo>();
		List<CPInfo> templist = new ArrayList<CPInfo>();
		CPInfoDAO cid = new CPInfoDAO();
		Session session = cid.getSession();
		Transaction tra = session.beginTransaction();
		List<CPInfo> list = cid.findAll();
		CPInfo cpi = new CPInfo();
		
		int loop = n<(list.size())?n:(list.size());
		Iterator it = list.iterator();
		while((it.hasNext())&&(loop > 0)) {
			cpi = (CPInfo) it.next();
			if(cpi.getDel().equals(0)){
				templist.add(cpi);
			}else{
				continue;
			}
			loop --;
		}

		loop = templist.size();
		for(int i = 0; i < loop; i++){
			resultlist.add((CPInfo)templist.toArray()[loop-i-1]);
		}
		tra.commit();
		return resultlist;
	}
	/**
	 * 按用户名查找已发布的信息
	 * @param userName
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public CPInfo findPublishedByUserName(String userName) {
		boolean findFlag = false;
		CPInfoDAO cid = new CPInfoDAO();
		CPInfo cpi = new CPInfo();
		UserManager um = new UserManager();
		String userID = um.findUserByName(userName).getUserId().toString();
		
		List<CPInfo> list = cid.findAll();
		Iterator<CPInfo> it = list.iterator();
		while (it.hasNext()) {
			cpi = it.next();
			if((cpi.getCpRegisterMainId().toString().equals(userID))
					&&(cpi.getDel().equals(0))){							//查找未失效的记录
				findFlag = true;
				break;
			}
		}
		if(findFlag == false) {
			cpi = null;
		}
		return cpi;
	}
	/**
	 * 按用户名查找已报名的信息
	 * @param userName
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public CPInfo findAppliedByUserName(String userName) {
		boolean findFlag = false;
		CPInfoDAO cid = new CPInfoDAO();
		CPInfo cpi = new CPInfo();
		UserManager um = new UserManager();
		String userID = um.findUserByName(userName).getUserId().toString();
		
		List<CPInfo> list = cid.findAll();
		Iterator<CPInfo> it = list.iterator();
		while (it.hasNext()) {
			cpi = it.next();
			if(((cpi.getCpRegisterId1().toString().equals(userID))
					||(cpi.getCpRegisterId2().toString().equals(userID))
					||(cpi.getCpRegisterId3().toString().equals(userID)))
					&&(cpi.getDel().equals(0))){							//查找未失效的记录
				findFlag = true;
				break;
			}
		}
		if(findFlag == false) {
			cpi = null;
		}
		return cpi;
	}
	
	@SuppressWarnings("unchecked")
	public List<CPInfo> findUsedInfoByUserName(String userName) {
		boolean findFlag = false;
		CPInfoDAO cid = new CPInfoDAO();
		CPInfo cpi = new CPInfo();
		UserManager um = new UserManager();
		List<CPInfo> resultList = new ArrayList<CPInfo>();
		String userID = um.findUserByName(userName).getUserId().toString();
		
		List<CPInfo> list = cid.findAll();
		Iterator<CPInfo> it = list.iterator();
		while (it.hasNext()) {
			cpi = it.next();
			if(((cpi.getCpRegisterId1().toString().equals(userID))
					||(cpi.getCpRegisterId2().toString().equals(userID))
					||(cpi.getCpRegisterId3().toString().equals(userID))
					||(cpi.getCpRegisterMainId().toString().equals(userID)))
					&&(cpi.getDel().equals(1))){								//查找已失效的记录
				findFlag = true;
				resultList.add(cpi);
			}
		}
		if(findFlag == false) {
			resultList = null;
		}
		return resultList;
	}
	
	@SuppressWarnings("unchecked")
	public boolean deleteInfoByID(Integer id) {
		boolean flag = false;
		CPInfoDAO cid = new CPInfoDAO();
		Session session = cid.getSession();
		Transaction tra = session.beginTransaction();
		
		try{
			List<CPInfo> list = new ArrayList<CPInfo>();
			CPInfo cpi = new CPInfo();
			list = cid.findAll();
			Iterator<CPInfo> it = list.iterator();
			while(it.hasNext()){
				cpi = it.next();
				if (cpi.getCpInfId().equals(id)) {
					cpi.setDel(1);
					cid.update(cpi);
					break;
				}
			}
			flag = true;
		}catch(Exception e){
			flag = false;
			e.printStackTrace();
		}
		tra.commit();
		return flag;
	}
	
}
/**
 * 
 * @author Administrator
 *
 */
class Dist{
	
	/**
	 * 由经纬度获得距离
	 * @param lat1
	 * @param lat2
	 * @param lng1
	 * @param lng2
	 * @return
	 */
	public Double getDistance(Double lat1, Double lat2, Double lng1, Double lng2){
		
		Double a = rad(lat1) - rad(lat2);
		Double b = rad(lng1) - rad(lng2);
		Double dist = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a/2),2) +
		Math.cos(rad(lat1))*Math.cos(rad(lat2))*Math.pow(Math.sin(b/2),2)));
		dist = dist *6378.137 ;// EARTH_RADIUS;
		
		return dist;
	}
	/**
	 * 角度转弧度
	 * @param d
	 * @return
	 */
	public Double rad(Double d){
	
	return d * Math.PI / 180.0;
	} 
}