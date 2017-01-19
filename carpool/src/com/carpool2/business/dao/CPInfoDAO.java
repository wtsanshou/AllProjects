package com.carpool2.business.dao;
// default package

import com.carpool2.business.dao.BaseHibernateDAO;
import com.carpool2.business.model.CPInfo;

import java.util.Date;
import java.util.List;
import java.util.Set;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.LockMode;
import org.hibernate.Query;
import org.hibernate.criterion.Example;

/**
 	* A data access object (DAO) providing persistence and search support for CPInfo entities.
 			* Transaction control of the save(), update() and delete() operations 
		can directly support Spring container-managed transactions or they can be augmented	to handle user-managed Spring transactions. 
		Each of these methods provides additional information for how to configure it for the desired type of transaction control. 	
	 * @see .CPInfo
  * @author MyEclipse Persistence Tools 
 */

public class CPInfoDAO extends BaseHibernateDAO  {
    private static final Log log = LogFactory.getLog(CPInfoDAO.class);
	//property constants
	public static final String CP_INF_USER_TYPE = "cpInfUserType";
	public static final String CP_INF_PEOP_NUMB = "cpInfPeopNumb";
	public static final String CP_INF_AVILABLE_NUM = "cpInfAvilableNum";
	public static final String CP_INF_SEX = "cpInfSex";
	public static final String CP_INF_START = "cpInfStart";
	public static final String CP_INF_SCOORDINATE = "cpInfScoordinate";
	public static final String CP_INF_END = "cpInfEnd";
	public static final String CP_INF_ECOORDINATE = "cpInfEcoordinate";
	public static final String CP_INF_STATUS = "cpInfStatus";
	public static final String CP_INF_REMARK = "cpInfRemark";
	public static final String DEL = "del";
	public static final String CP_INF_FREQUENCY = "cpInfFrequency";
	public static final String TEST3 = "test3";
	public static final String CP_REGISTER_MAIN_ID = "cpRegisterMainId";
	public static final String CP_REGISTER_ID1 = "cpRegisterId1";
	public static final String CP_REGISTER_ID2 = "cpRegisterId2";
	public static final String CP_REGISTER_ID3 = "cpRegisterId3";



    
    public void save(CPInfo transientInstance) {
        log.debug("saving CPInfo instance");
        try {
            getSession().save(transientInstance);
            log.debug("save successful");
        } catch (RuntimeException re) {
            log.error("save failed", re);
            throw re;
        }
    }
    
	public void delete(CPInfo persistentInstance) {
        log.debug("deleting CPInfo instance");
        try {
            getSession().delete(persistentInstance);
            log.debug("delete successful");
        } catch (RuntimeException re) {
            log.error("delete failed", re);
            throw re;
        }
    }
    
    public CPInfo findById( java.lang.Integer id) {
        log.debug("getting CPInfo instance with id: " + id);
        try {
            CPInfo instance = (CPInfo) getSession()
                    .get("CPInfo", id);
            return instance;
        } catch (RuntimeException re) {
            log.error("get failed", re);
            throw re;
        }
    }
    
    
    public List findByExample(CPInfo instance) {
        log.debug("finding CPInfo instance by example");
        try {
            List results = getSession()
                    .createCriteria("CPInfo")
                    .add(Example.create(instance))
            .list();
            log.debug("find by example successful, result size: " + results.size());
            return results;
        } catch (RuntimeException re) {
            log.error("find by example failed", re);
            throw re;
        }
    }    
    
    public List findByProperty(String propertyName, Object value) {
      log.debug("finding CPInfo instance with property: " + propertyName
            + ", value: " + value);
      try {
         String queryString = "from CPInfo as model where model." 
         						+ propertyName + "= ?";
         Query queryObject = getSession().createQuery(queryString);
		 queryObject.setParameter(0, value);
		 return queryObject.list();
      } catch (RuntimeException re) {
         log.error("find by property name failed", re);
         throw re;
      }
	}

	public List findByCpInfUserType(Object cpInfUserType
	) {
		return findByProperty(CP_INF_USER_TYPE, cpInfUserType
		);
	}
	
	public List findByCpInfPeopNumb(Object cpInfPeopNumb
	) {
		return findByProperty(CP_INF_PEOP_NUMB, cpInfPeopNumb
		);
	}
	
	public List findByCpInfAvilableNum(Object cpInfAvilableNum
	) {
		return findByProperty(CP_INF_AVILABLE_NUM, cpInfAvilableNum
		);
	}
	
	public List findByCpInfSex(Object cpInfSex
	) {
		return findByProperty(CP_INF_SEX, cpInfSex
		);
	}
	
	public List findByCpInfStart(Object cpInfStart
	) {
		return findByProperty(CP_INF_START, cpInfStart
		);
	}
	
	public List findByCpInfScoordinate(Object cpInfScoordinate
	) {
		return findByProperty(CP_INF_SCOORDINATE, cpInfScoordinate
		);
	}
	
	public List findByCpInfEnd(Object cpInfEnd
	) {
		return findByProperty(CP_INF_END, cpInfEnd
		);
	}
	
	public List findByCpInfEcoordinate(Object cpInfEcoordinate
	) {
		return findByProperty(CP_INF_ECOORDINATE, cpInfEcoordinate
		);
	}
	
	public List findByCpInfStatus(Object cpInfStatus
	) {
		return findByProperty(CP_INF_STATUS, cpInfStatus
		);
	}
	
	public List findByCpInfRemark(Object cpInfRemark
	) {
		return findByProperty(CP_INF_REMARK, cpInfRemark
		);
	}
	
	public List findByDel(Object del
	) {
		return findByProperty(DEL, del
		);
	}
	
	public List findByCpInfFrequency(Object cpInfFrequency
	) {
		return findByProperty(CP_INF_FREQUENCY, cpInfFrequency
		);
	}
	
	public List findByTest3(Object test3
	) {
		return findByProperty(TEST3, test3
		);
	}
	
	public List findByCpRegisterMainId(Object cpRegisterMainId
	) {
		return findByProperty(CP_REGISTER_MAIN_ID, cpRegisterMainId
		);
	}
	
	public List findByCpRegisterId1(Object cpRegisterId1
	) {
		return findByProperty(CP_REGISTER_ID1, cpRegisterId1
		);
	}
	
	public List findByCpRegisterId2(Object cpRegisterId2
	) {
		return findByProperty(CP_REGISTER_ID2, cpRegisterId2
		);
	}
	
	public List findByCpRegisterId3(Object cpRegisterId3
	) {
		return findByProperty(CP_REGISTER_ID3, cpRegisterId3
		);
	}
	

	public List findAll() {
		log.debug("finding all CPInfo instances");
		try {
			String queryString = "from CPInfo";
	         Query queryObject = getSession().createQuery(queryString);
			 return queryObject.list();
		} catch (RuntimeException re) {
			log.error("find all failed", re);
			throw re;
		}
	}
	
    public CPInfo merge(CPInfo detachedInstance) {
        log.debug("merging CPInfo instance");
        try {
            CPInfo result = (CPInfo) getSession()
                    .merge(detachedInstance);
            log.debug("merge successful");
            return result;
        } catch (RuntimeException re) {
            log.error("merge failed", re);
            throw re;
        }
    }

    public void attachDirty(CPInfo instance) {
        log.debug("attaching dirty CPInfo instance");
        try {
            getSession().saveOrUpdate(instance);
            log.debug("attach successful");
        } catch (RuntimeException re) {
            log.error("attach failed", re);
            throw re;
        }
    }
    
    public void attachClean(CPInfo instance) {
        log.debug("attaching clean CPInfo instance");
        try {
            getSession().lock(instance, LockMode.NONE);
            log.debug("attach successful");
        } catch (RuntimeException re) {
            log.error("attach failed", re);
            throw re;
        }
    }

    public void update(CPInfo transientInstance) {
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