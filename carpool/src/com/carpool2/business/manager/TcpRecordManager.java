package com.carpool2.business.manager;


import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.hibernate.Session;
import org.hibernate.Transaction;
import com.carpool2.business.dao.TcpRecordDAO;
import com.carpool2.business.model.CPInfo;
import com.carpool2.business.model.TcpRecord;


public class TcpRecordManager {
	/**
	 * 添加记录
	 * @param tr
	 */
	public void addRecord(TcpRecord tr){
		TcpRecordDAO trd = new TcpRecordDAO();
		Session session = trd.getSession();
		Transaction tra = session.beginTransaction();
		trd.save(tr);
		tra.commit();
	}
	/**
	 * 按照用户和线路的ID删除记录
	 * @param userid
	 * @param routeid
	 */
	@SuppressWarnings("unchecked")
	public void deleteRecordByID(Integer userid, Integer routeid) {
		TcpRecordDAO trd = new TcpRecordDAO();
		TcpRecord tr = new TcpRecord();
		Session session = trd.getSession();
		Transaction tra = session.beginTransaction();
		List<TcpRecord> list = trd.findAll();
		Iterator<TcpRecord> it = list.iterator();
		while (it.hasNext()){
			tr = it.next();
			if((tr.getTuser().equals(userid))
					&&(tr.getTcpInf().equals(routeid))
					&&(tr.getDel().equals(0))
					){
				tr.setDel(1);
			}
		}
		tra.commit();
	}
	/**
	 * 根据用户id查找所有历史消息
	 * @param userid
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public List<TcpRecord> findHistoryByUserID(Integer userid) {
		TcpRecordDAO trd = new TcpRecordDAO();
		TcpRecord tr = new TcpRecord();
		Session session = trd.getSession();
		Transaction tra = session.beginTransaction();
		List<TcpRecord> resultlist = new ArrayList();
		resultlist = null;
		List<TcpRecord> list = trd.findAll();
		Iterator<TcpRecord> it = list.iterator();
		while (it.hasNext()) {
			tr = it.next();
			if((tr.getTuser().equals(userid))
					&&(tr.getDel().equals(1))) {
				resultlist.add(tr);
			}
		}
		
		tra.commit();
		return resultlist;
	}
	
	@SuppressWarnings("unchecked")
	public TcpRecord getLatestByUserID(Integer userid) {
		TcpRecordDAO trd = new TcpRecordDAO();
		TcpRecord tr = new TcpRecord();
		Session session = trd.getSession();
		Transaction tra = session.beginTransaction();
		List<TcpRecord> list = new ArrayList();
		list = trd.findAll();
		Iterator<TcpRecord> it = list.iterator();
		int i;
		for (i = 0; i < list.size(); i++) {
			tr = it.next();
			if((tr.getTuser().equals(userid))
					&&(tr.getDel().equals(0))) {
				break;
			}
		}
		if(i == list.size()) {
			tr = null;
		}
		tra.commit();
		return tr;
	}
	public boolean dispatchInfo(CPInfo cpi){
		TcpRecordManager trm = new TcpRecordManager();
		TcpRecord tr = new TcpRecord();
		String message = "您所参与的线路：从"+cpi.getCpInfStart()+"出发,至"+cpi.getCpInfEnd()
							+",出发时间："+cpi.getCpInfDepartTime()+" 已经拼车成功. 请及时与您的拼友联系！";
		tr.setDel(0);
		tr.setTest2(message);
		tr.setTuser(cpi.getCpRegisterMainId());
		trm.addRecord(tr);
		
		tr.setTuser(cpi.getCpRegisterId1());
		trm.addRecord(tr);
		
		tr.setTuser(cpi.getCpRegisterId2());
		trm.addRecord(tr);
		
		tr.setTuser(cpi.getCpRegisterId3());
		trm.addRecord(tr);
		return true;
	}
}
