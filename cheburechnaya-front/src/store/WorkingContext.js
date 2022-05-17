import AuthService from '@/services/auth.service'

const state = {
    userId: localStorage.getItem('work-context.userId'),
    organizationId: localStorage.getItem('work-context.organizationId'),
    moduleId: localStorage.getItem('work-context.moduleId'),
    actionId: localStorage.getItem('work-context.actionId'),
}

const mutations = {
    updateUserId(state, userId) {
        state.userId = userId
        if (userId)
            localStorage.setItem('work-context.userId', userId)
        else
            localStorage.removeItem('work-context.userId')
    },
    updateOrganizationId(state, organizationId) {
        state.organizationId = organizationId
        if (organizationId != null)
            localStorage.setItem('work-context.organizationId', organizationId)
        else
            localStorage.removeItem('work-context.organizationId')
    },
    updateModuleId(state, moduleId) {
        state.moduleId = moduleId
        if (moduleId != null)
            localStorage.setItem('work-context.moduleId', moduleId)
        else
            localStorage.removeItem('work-context.moduleId')
    },

    updateActionId(state, actionId) {
        state.actionId = actionId
        if (actionId != null)
            localStorage.setItem('work-context.actionId', actionId)
        else
            localStorage.removeItem('work-context.actionId')
    },
}

const actions = {

    setUserContext(context, userId) {
        context.commit('updateUserId', userId);
    },
    setOrganizationContext(context, organizationId) {
        context.commit('updateOrganizationId', organizationId);
    },
    setModuleContext(context, moduleId) {
        context.commit('updateModuleId', moduleId);
    },

    setActionContext(context, actionId) {
        context.commit('updateActionId', actionId);
    },

    initUserContextFromAuth(context) {
        const userId = AuthService.getUserId()
        if (userId)
            context.commit('updateUserId', userId);
    },

    clearWorkingContext(context) {
        context.commit('updateUserId', null);
        context.commit('updateOrganizationId', null);
        context.commit('updateModuleId', null);
    }

}

const getters = {
    getUserId: (state) => state.userId,
}

export default {
    state,
    getters,
    actions,
    mutations,

    getContext() {
        let result = {}

        const userId = localStorage.getItem('work-context.userId')
        if (userId && userId !== "null") result.userId = userId

        return result
    }

};


