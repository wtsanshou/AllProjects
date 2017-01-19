package com.carpool2.business.dao;


import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.LockMode;
import org.hibernate.Query;
import org.hibernate.criterion.Example;

import com.carpool2.business.model.TcpRecord;

/**
 * A data access object (DAO) providing persistence and search support for
 * TcpRecord entities. Transaction control of the save(), update() and delete()
 * operations can directly support Spring container-managed transactions or they
 * can be augmented to handle user-managed Spring transactions. Each of these
 * methods provides additional information for how to configure it for the
 * desired type of transaction control.
 * 
 * @see com.carpool2.business.model.TcpRecord
 * @author MyEclipse Persistence Tools
 */

public class TcpRecordDAO extends BaseHibernateDAO {
	private static final Log log = LogFactory.getLog(TcpRecordDAO.class);
	// property constants
	public static final String CP_RECORD_USER_TYPE = "cpRecordUserType";
	public static final String CP_RECORD_TIME = "cpRecordTime";
	public static final String CP_RECORD_COMMENT = "cpRecordComment";
	public static final String DEL = "del";
	public static final String TEST2 = "test2";
	public static final String TEST3 = "test3";

	public void save(TcpRecord transientInstance) {
		log.debug("saving TcpRecord instance");
		try {
			getSession().save(transientInstance);
			log.debug("save successful");
		} catch (RuntimeException re) {
			log.error("save failed", re);
			throw re;
		}
	}

	public void delete(TcpRecord persistentInstance) {
		log.debug("deleting TcpRecord instance");
		try {
			getSession().delete(persistentInstance);
			log.debug("delete successful");
		} catch (RuntimeException re) {
			log.error("delete failed", re);
			throw re;
		}
	}

	public TcpRecord findById(java.lang.Integer id) {
		log.debug("getting TcpRecord instance with id: " + id);
		try {
			TcpRecord instance = (TcpRecord) getSession().get(
					"com.carpool2.business.dao.TcpRecord", id);
			return instance;
		} catch (RuntimeException re) {
			log.error("get failed", re);
			throw re;
		}
	}

	public List findByExample(TcpRecord instance) {
		log.debug("finding TcpRecord instance by example");
		try {
			List results = getSession().createCriteria(
					"com.carpool2.business.dao.TcpRecord").add(
					Example.create(instance)).list();
			log.debug("find by example successful, result size: "
					+ results.size());
			return results;
		} catch (RuntimeException re) {
			log.error("find by example failed", re);
			throw re;
		}
	}

	public List findByProperty(String propertyName, Object value) {
		log.debug("finding TcpRecord instance with property: " + propertyName
				+ ", value: " + value);
		try {
			String queryString = "from TcpRecord as model where model."
					+ propertyName + "= ?";
			Query queryObject = getSession().createQuery(queryString);
			queryObject.setParameter(0, value);
			return queryObject.list();
		} catch (RuntimeException re) {
			log.error("find by property name failed", re);
			throw re;
		}
	}

	public List findByCpRecordUserType(Object cpRecordUserType) {
		return findByProperty(CP_RECORD_USER_TYPE, cpRecordUserType);
	}

	public List findByCpRecordTime(Object cpRecordTime) {
		return findByProperty(CP_RECORD_TIME, cpRecordTime);
	}

	public List findByCpRecordComment(Object cpRecordComment) {
		return findByProperty(CP_RECORD_COMMENT, cpRecordComment);
	}

	public List findByDel(Object del) {
		return findByProperty(DEL, del);
	}

	public List findByTest2(Object test2) {
		return findByProperty(TEST2, test2);
	}

	public List findByTest3(Object test3) {
		return findByProperty(TEST3, test3);
	}

	public List findAll() {
		log.debug("finding all TcpRecord instances");
		try {
			String queryString = "from TcpRecord";
			Query queryObject = getSession().createQuery(queryString);
			return queryObject.list();
		} catch (RuntimeException re) {
			log.error("find all failed", re);
			throw re;
		}
	}

	public TcpRecord merge(TcpRecord detachedInstance) {
		log.debug("merging TcpRecord instance");
		try {
			TcpRecord result = (TcpRecord) getSession().merge(detachedInstance);
			log.debug("merge successful");
			return result;
		} catch (RuntimeException re) {
			log.error("merge failed", re);
			throw re;
		}
	}

	public void attachDirty(TcpRecord instance) {
		log.debug("attaching dirty TcpRecord instance");
		try {
			getSession().saveOrUpdate(instance);
			log.debug("attach successful");
		} catch (RuntimeException re) {
			log.error("attach failed", re);
			throw re;
		}
	}

	public void attachClean(TcpRecord instance) {
		log.debug("attaching clean TcpRecord instance");
		try {
			getSession().lock(instance, LockMode.NONE);
			log.debug("attach successful");
		} catch (RuntimeException re) {
			log.error("attach failed", re);
			throw re;
		}
	}
}