package com.carpool2.business.dao;
// default package

import com.carpool2.business.dao.BaseHibernateDAO;
import com.carpool2.business.model.User;

import java.util.Date;
import java.util.List;
import java.util.Set;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.LockMode;
import org.hibernate.Query;
import org.hibernate.criterion.Example;

/**
 * A data access object (DAO) providing persistence and search support for User
 * entities. Transaction control of the save(), update() and delete() operations
 * can directly support Spring container-managed transactions or they can be
 * augmented to handle user-managed Spring transactions. Each of these methods
 * provides additional information for how to configure it for the desired type
 * of transaction control.
 * 
 * @see .User
 * @author MyEclipse Persistence Tools
 */

public class UserDAO extends BaseHibernateDAO {
	private static final Log log = LogFactory.getLog(UserDAO.class);
	// property constants
	public static final String USER_NAME = "userName";
	public static final String USER_NICKNAME = "userNickname";
	public static final String USER_PWD = "userPwd";
	public static final String USER_SEX = "userSex";
	public static final String USER_LOGIN_TIMES = "userLoginTimes";
	public static final String USER_GOOD_TIMES = "userGoodTimes";
	public static final String USER_BAD_TIMES = "userBadTimes";
	public static final String USER_LEVEL = "userLevel";
	public static final String USER_EMAIL = "userEmail";
	public static final String USER_SUCCESS_TIMES = "userSuccessTimes";
	public static final String USER_CONTACT = "userContact";
	public static final String USER_PROFILE = "userProfile";
	public static final String USER_INDR = "userIndr";
	public static final String DEL = "del";
	public static final String TEST = "test";

	public void save(User transientInstance) {
		log.debug("saving User instance");
		try {
			getSession().save(transientInstance);
			log.debug("save successful");
		} catch (RuntimeException re) {
			log.error("save failed", re);
			throw re;
		}
	}

	public void delete(User persistentInstance) {
		log.debug("deleting User instance");
		try {
			getSession().delete(persistentInstance);
			log.debug("delete successful");
		} catch (RuntimeException re) {
			log.error("delete failed", re);
			throw re;
		}
	}

	public User findById(java.lang.Integer id) {
		log.debug("getting User instance with id: " + id);
		try {
			User instance = (User) getSession().get("User", id);
			return instance;
		} catch (RuntimeException re) {
			log.error("get failed", re);
			throw re;
		}
	}

	public List findByExample(User instance) {
		log.debug("finding User instance by example");
		try {
			List results = getSession().createCriteria("User").add(
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
		log.debug("finding User instance with property: " + propertyName
				+ ", value: " + value);
		try {
			String queryString = "from User as model where model."
					+ propertyName + "= ?";
			Query queryObject = getSession().createQuery(queryString);
			queryObject.setParameter(0, value);
			return queryObject.list();
		} catch (RuntimeException re) {
			log.error("find by property name failed", re);
			throw re;
		}
	}

	public List findByUserName(Object userName) {
		return findByProperty(USER_NAME, userName);
	}

	public List findByUserNickname(Object userNickname) {
		return findByProperty(USER_NICKNAME, userNickname);
	}

	public List findByUserPwd(Object userPwd) {
		return findByProperty(USER_PWD, userPwd);
	}

	public List findByUserSex(Object userSex) {
		return findByProperty(USER_SEX, userSex);
	}

	public List findByUserLoginTimes(Object userLoginTimes) {
		return findByProperty(USER_LOGIN_TIMES, userLoginTimes);
	}

	public List findByUserGoodTimes(Object userGoodTimes) {
		return findByProperty(USER_GOOD_TIMES, userGoodTimes);
	}

	public List findByUserBadTimes(Object userBadTimes) {
		return findByProperty(USER_BAD_TIMES, userBadTimes);
	}

	public List findByUserLevel(Object userLevel) {
		return findByProperty(USER_LEVEL, userLevel);
	}

	public List findByUserEmail(Object userEmail) {
		return findByProperty(USER_EMAIL, userEmail);
	}

	public List findByUserSuccessTimes(Object userSuccessTimes) {
		return findByProperty(USER_SUCCESS_TIMES, userSuccessTimes);
	}

	public List findByUserContact(Object userContact) {
		return findByProperty(USER_CONTACT, userContact);
	}

	public List findByUserProfile(Object userProfile) {
		return findByProperty(USER_PROFILE, userProfile);
	}

	public List findByUserIndr(Object userIndr) {
		return findByProperty(USER_INDR, userIndr);
	}

	public List findByDel(Object del) {
		return findByProperty(DEL, del);
	}

	public List findByTest(Object test) {
		return findByProperty(TEST, test);
	}

	public List findAll() {
		log.debug("finding all User instances");
		try {
			String queryString = "from User";
			Query queryObject = getSession().createQuery(queryString);
			return queryObject.list();
		} catch (RuntimeException re) {
			log.error("find all failed", re);
			throw re;
		}
	}

	public User merge(User detachedInstance) {
		log.debug("merging User instance");
		try {
			User result = (User) getSession().merge(detachedInstance);
			log.debug("merge successful");
			return result;
		} catch (RuntimeException re) {
			log.error("merge failed", re);
			throw re;
		}
	}

	public void attachDirty(User instance) {
		log.debug("attaching dirty User instance");
		try {
			getSession().saveOrUpdate(instance);
			log.debug("attach successful");
		} catch (RuntimeException re) {
			log.error("attach failed", re);
			throw re;
		}
	}

	public void attachClean(User instance) {
		log.debug("attaching clean User instance");
		try {
			getSession().lock(instance, LockMode.NONE);
			log.debug("attach successful");
		} catch (RuntimeException re) {
			log.error("attach failed", re);
			throw re;
		}
	}
	 public void update(User transientInstance) {
	        log.debug("updating CPInfo instance");
	        try {
	            getSession().update(transientInstance);
	            log.debug("update successful");
	        } catch (RuntimeException re) {
	            log.error("update failed", re);
	            throw re;
	        }
	    }
}