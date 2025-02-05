﻿public enum ErrorCode
{
    INVALID_REQUEST = 450,
    INVALID_PERMISSION = 451,
    MISSING_EULA_AGREEMENT = 452,
    INVALID_JOB = 453,
    INVALID_TASK = 453,
    UNABLE_TO_ADD_TASK_TO_JOB = 454,
    INVALID_CONTAINER = 455,
    UNABLE_TO_RESIZE = 456,
    UNAUTHORIZED = 457,
    UNABLE_TO_USE_STOPPED_JOB = 458,
    UNABLE_TO_CHANGE_JOB_DURATION = 459,
    UNABLE_TO_CREATE_NETWORK = 460,
    VM_CREATE_FAILED = 461,
    MISSING_NOTIFICATION = 462,
    INVALID_IMAGE = 463,
    UNABLE_TO_CREATE = 464,
    UNABLE_TO_PORT_FORWARD = 465,
    UNABLE_TO_FIND = 467,
    TASK_FAILED = 468,
    INVALID_NODE = 469,
    NOTIFICATION_FAILURE = 470,
    UNABLE_TO_UPDATE = 471,
    PROXY_FAILED = 472,
    INVALID_CONFIGURATION = 473,
}


public enum WebhookMessageState
{
    Queued,
    Retrying,
    Succeeded,
    Failed
}